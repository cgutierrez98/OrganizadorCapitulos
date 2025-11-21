using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using organizadorCapitulos.Application.Commands;
using organizadorCapitulos.Application.Comparers;
using organizadorCapitulos.Application.Services;
using organizadorCapitulos.Application.Strategies;
using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Enums;
using organizadorCapitulos.Core.Interfaces.Observers;
using organizadorCapitulos.Core.Interfaces.Repositories;
using organizadorCapitulos.Core.Interfaces.Strategies;
using organizadorCapitulos.Infrastructure.Repositories;
using SortOrder = System.Windows.Forms.SortOrder;


namespace organizadorCapitulos
{
    public partial class MainForm : Form, IProgressObserver
    {
        private readonly IFileRepository _fileRepository;
        private readonly FileOrganizerService _fileOrganizerService;
        private readonly RenameStrategyFactory _strategyFactory;
        private readonly CommandManager _commandManager;
        private readonly ProgressNotifier _progressNotifier;

        private IRenameStrategy _currentStrategy;
        private SortOrder _lastSortOrder = SortOrder.None;
        private int _lastColumnSorted = -1;

        public MainForm()
        {
            InitializeComponent();

            // Inicialización de dependencias
            _fileRepository = new FileRepository();
            _progressNotifier = new ProgressNotifier();
            _fileOrganizerService = new FileOrganizerService(_fileRepository, this);
            _strategyFactory = new RenameStrategyFactory();
            _commandManager = new CommandManager();

            _progressNotifier.Subscribe(this);
            _currentStrategy = _strategyFactory.CreateStrategy(RenameMode.Maintain);

            ConfigureValidation();
            UpdateUIState();
            UpdateStatus("Listo para trabajar...");
        }

        private void ConfigureValidation()
        {
            txtTemporada.Validating += txtTemporada_Validating;
            txtCapitulo.Validating += txtCapitulo_Validating;
            txtTitulo.Validating += txtTitulo_Validating;
        }

        private void UpdateUIState()
        {
            btnGuardar.Enabled = listViewSeries.SelectedItems.Count > 0;
            btnUndo.Enabled = _commandManager.CanUndo;
            btnRedo.Enabled = _commandManager.CanRedo;

            // Actualizar tooltips o textos de botones
            btnUndo.Text = _commandManager.CanUndo ? $"Deshacer ({_commandManager.GetUndoDescription()})" : "Deshacer";
            btnRedo.Text = _commandManager.CanRedo ? $"Rehacer ({_commandManager.GetRedoDescription()})" : "Rehacer";
        }

        private void UpdateStatus(string message)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => UpdateStatus(message)));
                return;
            }
            lblStatus.Text = message;
        }

        #region Event Handlers
        private void listViewSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSeries.SelectedItems.Count > 0)
            {
                if (radioCambiar.Checked)
                {
                    string fileName = listViewSeries.SelectedItems[0].Text;
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    txtTitulo.Text = fileNameWithoutExtension;
                }
            }
            UpdateUIState();
        }

        private void listViewSeries_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _lastColumnSorted)
            {
                _lastSortOrder = _lastSortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                _lastSortOrder = SortOrder.Ascending;
                _lastColumnSorted = e.Column;
            }

            listViewSeries.ListViewItemSorter = new ListViewItemComparer(e.Column, _lastSortOrder);
            listViewSeries.Sort();
            UpdateColumnHeaders(e.Column);
        }

        private void radioMantener_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMantener.Checked)
            {
                _currentStrategy = _strategyFactory.CreateStrategy(RenameMode.Maintain);
                UpdateStatus("Modo: Mantener estructura - El número de capítulo se incrementará automáticamente");
            }
        }

        private void radioCambiar_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCambiar.Checked)
            {
                _currentStrategy = _strategyFactory.CreateStrategy(RenameMode.Change);
                UpdateStatus("Modo: Cambiar estructura - El número de capítulo se mantendrá igual");
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await RenombrarCapituloAsync();
        }

        private async void onClickEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await RenombrarCapituloAsync();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private async void btnGuardarTodo_Click(object sender, EventArgs e)
        {
            await GuardarTodoAsync();
        }

        private async void btnCargarCarpetas_Click(object sender, EventArgs e)
        {
            await CargarCarpetasAsync();
        }

        private async void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                await _commandManager.UndoAsync();
                UpdateStatus("Operación deshecha correctamente");
                UpdateUIState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al deshacer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRedo_Click(object sender, EventArgs e)
        {
            try
            {
                await _commandManager.RedoAsync();
                UpdateStatus("Operación rehecha correctamente");
                UpdateUIState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al rehacer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Business Logic Methods
        private async Task RenombrarCapituloAsync()
        {
            // 1. Validaciones
            if (listViewSeries.SelectedItems.Count == 0) return; // O mostrar mensaje
            if (!ValidateChildren(ValidationConstraints.Enabled)) return;

            if (!int.TryParse(txtTemporada.Text, out int temporada) ||
                !int.TryParse(txtCapitulo.Text, out int capitulo))
            {
                MessageBox.Show("Datos numéricos inválidos.");
                return;
            }

            // 2. Obtener Datos
            ListViewItem selectedItem = listViewSeries.SelectedItems[0];
            string originalFilePath = selectedItem.SubItems[1].Text;

            if (!File.Exists(originalFilePath))
            {
                MessageBox.Show("El archivo ya no existe.");
                return;
            }

            var chapterInfo = new ChapterInfo
            {
                Season = temporada,
                Chapter = capitulo,
                Title = txtTitulo.Text.Trim()
            };

            try
            {
                // 3. CALCULAR (Sin mover nada)
                // Obtenemos el directorio y la extensión del archivo original
                string directory = Path.GetDirectoryName(originalFilePath);
                string extension = Path.GetExtension(originalFilePath);
                string originalNameNoExt = Path.GetFileNameWithoutExtension(originalFilePath);

                // USAMOS LA ESTRATEGIA PARA OBTENER EL NOMBRE (Aquí es donde fallaba antes)
                string nuevoNombreBase = _currentStrategy.GetNewFileName(originalNameNoExt, chapterInfo);

                // Construimos la ruta final completa
                string nuevoFilePath = Path.Combine(directory, nuevoNombreBase + extension);

                // 4. EJECUTAR COMANDO (Mover físicamente)
                var renameCommand = new RenameFileCommand(_fileRepository, originalFilePath, nuevoFilePath);
                await _commandManager.ExecuteCommandAsync(renameCommand);

                // 5. ACTUALIZAR ESTRATEGIA (Si tiene contadores internos)
                _currentStrategy.UpdateAfterRename(chapterInfo);

                // 6. ACTUALIZAR UI
                selectedItem.Text = nuevoNombreBase + extension; // Actualizar visualización
                selectedItem.SubItems[1].Text = nuevoFilePath;   // Actualizar ruta oculta

                UpdateStatus($"Renombrado a: {nuevoNombreBase}");

                // UX: Preparar siguiente
                SeleccionarSiguienteItem(selectedItem.Index);

                // Aumentar capítulo en la caja de texto para el siguiente
                capitulo++;
                txtCapitulo.Text = capitulo.ToString();

                // Foco al título
                txtTitulo.Focus();
                txtTitulo.SelectAll();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Conflicto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task GuardarTodoAsync()
        {
            if (listViewSeries.Items.Count == 0)
            {
                MessageBox.Show("No hay archivos para guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string carpetaDestino = SeleccionarCarpeta();
            if (string.IsNullOrEmpty(carpetaDestino)) return;

            var sourcePaths = listViewSeries.Items.Cast<ListViewItem>()
                .Select(item => item.SubItems[1].Text)
                .ToList();

            using (ProgressForm progressForm = new ProgressForm())
            {
                progressForm.Show();

                try
                {
                    var moveCommand = new MoveFilesCommand(_fileRepository, this, sourcePaths, carpetaDestino);
                    await _commandManager.ExecuteCommandAsync(moveCommand);

                    // Actualizar ListView con nuevas rutas
                    for (int i = 0; i < listViewSeries.Items.Count; i++)
                    {
                        string newPath = Path.Combine(carpetaDestino, Path.GetFileName(sourcePaths[i]));
                        listViewSeries.Items[i].SubItems[1].Text = newPath;
                    }

                    UpdateStatus($"Todos los archivos movidos a: {carpetaDestino}");
                    MessageBox.Show("Todos los archivos fueron movidos exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatus($"Error al mover archivos: {ex.Message}");
                }
            }

            UpdateUIState();
        }

        private async Task CargarCarpetasAsync()
        {
            using (var folderBrowser = new FolderBrowserForm())
            {
                if (folderBrowser.ShowDialog(this) != DialogResult.OK || folderBrowser.SelectedFolders.Count == 0)
                {
                    return;
                }

                listViewSeries.Items.Clear();
                UpdateStatus("Cargando archivos de video...");

                using (ProgressForm progressForm = new ProgressForm())
                {
                    progressForm.Show();

                    try
                    {
                        var files = await _fileOrganizerService.LoadVideoFilesAsync(folderBrowser.SelectedFolders);

                        int totalFiles = files.Count;
                        int processedFiles = 0;

                        foreach (string file in files)
                        {
                            processedFiles++;
                            UpdateProgress(processedFiles, totalFiles, Path.GetFileName(file));

                            ListViewItem item = new ListViewItem(Path.GetFileName(file));
                            item.SubItems.Add(file);
                            listViewSeries.Items.Add(item);
                        }

                        UpdateStatus($"Carga completada: {listViewSeries.Items.Count} archivos encontrados");
                        MessageBox.Show($"Se cargaron {listViewSeries.Items.Count} archivos de video.",
                            "Carga completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar archivos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatus($"Error en carga: {ex.Message}");
                    }
                }
            }
        }
        #endregion

        #region Helper Methods
        private void SeleccionarSiguienteItem(int currentIndex)
        {
            listViewSeries.SelectedItems.Clear();

            if (currentIndex < listViewSeries.Items.Count - 1)
            {
                ListViewItem nextItem = listViewSeries.Items[currentIndex + 1];
                nextItem.Selected = true;
                nextItem.Focused = true;
                listViewSeries.EnsureVisible(currentIndex + 1);

                if (radioCambiar.Checked)
                {
                    string nextFileName = Path.GetFileNameWithoutExtension(nextItem.Text);
                    txtTitulo.Text = nextFileName;
                }
            }
            else
            {
                // Si era el último item, limpiar selección
                btnGuardar.Enabled = false;
            }
        }

        private void UpdateColumnHeaders(int sortedColumn)
        {
            foreach (ColumnHeader column in listViewSeries.Columns)
            {
                if (column.Index == sortedColumn)
                {
                    column.Text = column.Index == 0
                        ? $"Nombre del archivo {(_lastSortOrder == SortOrder.Ascending ? "↑" : "↓")}"
                        : $"Ruta completa {(_lastSortOrder == SortOrder.Ascending ? "↑" : "↓")}";
                }
                else
                {
                    column.Text = column.Index == 0 ? "Nombre del archivo" : "Ruta completa";
                }
            }
        }

        private string SeleccionarCarpeta()
        {
            using (var folderBrowser = new FolderBrowserForm())
            {
                folderBrowser.Text = "Selecciona Carpeta Destino";
                folderBrowser.IsSingleSelectionMode = true;

                if (folderBrowser.ShowDialog(this) == DialogResult.OK && folderBrowser.SelectedFolders.Count > 0)
                {
                    return folderBrowser.SelectedFolders[0];
                }
                return null;
            }
        }
        #endregion

        #region IProgressObserver Implementation
        public void UpdateProgress(int current, int total, string filename)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(current, total, filename)));
                return;
            }

            UpdateStatus($"Procesando {current} de {total} archivos: {filename}");
        }


        #endregion

        #region Validation Methods
        private void txtNumerico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTemporada_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTemporada.Text) || !int.TryParse(txtTemporada.Text, out int temp) || temp <= 0)
            {
                errorProvider.SetError(txtTemporada, "Ingrese un número de temporada válido (mayor que 0)");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtTemporada, "");
            }
        }

        private void txtCapitulo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCapitulo.Text) || !int.TryParse(txtCapitulo.Text, out int cap) || cap <= 0)
            {
                errorProvider.SetError(txtCapitulo, "Ingrese un número de capítulo válido (mayor que 0)");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtCapitulo, "");
            }
        }

        private void txtTitulo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                errorProvider.SetError(txtTitulo, "El título no puede estar vacío");
                e.Cancel = true;
            }
            else if (txtTitulo.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                errorProvider.SetError(txtTitulo, "El título contiene caracteres no válidos para un nombre de archivo");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtTitulo, "");
            }
        }

        void IProgressObserver.UpdateStatus(string status)
        {
            UpdateStatus(status);
        }
        #endregion
    }
}