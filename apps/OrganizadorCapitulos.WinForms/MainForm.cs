using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using organizadorCapitulos.Application.Commands;
using organizadorCapitulos.Application.Comparers;
using organizadorCapitulos.Application.Services;
using organizadorCapitulos.Application.Strategies;
using organizadorCapitulos.Core.Entities;
using organizadorCapitulos.Core.Enums;
using organizadorCapitulos.Core.Interfaces.Observers;
using organizadorCapitulos.Core.Interfaces.Repositories;
using organizadorCapitulos.Core.Interfaces.Services;
using organizadorCapitulos.Core.Interfaces.Strategies;
using organizadorCapitulos.Infrastructure.Repositories;
using organizadorCapitulos.Infrastructure.Services;
using SortOrder = System.Windows.Forms.SortOrder;

namespace OrganizadorCapitulos.WinForms.UI.Forms
{
    public partial class MainForm : Form, IProgressObserver
    {
        private readonly IFileRepository _fileRepository;
        private readonly FileOrganizerService _fileOrganizerService;
        private readonly RenameStrategyFactory _strategyFactory;
        private readonly CommandManager _commandManager;
        private readonly ProgressNotifier _progressNotifier;
        private readonly IMetadataService _metadataService;
        private readonly IAIService _aiService;

        private IRenameStrategy _currentStrategy;
        private SortOrder _lastSortOrder = SortOrder.None;
        private int _lastColumnSorted = -1;

        public MainForm()
        {
            InitializeComponent();

            // Ajuste inicial de layout (centra el groupBox y ajusta anchos de columnas)
            MainForm_Resize(this, EventArgs.Empty);

            // Inicialización de dependencias
            _fileRepository = new FileRepository();
            _progressNotifier = new ProgressNotifier();
            _fileOrganizerService = new FileOrganizerService(_fileRepository, this);
            _strategyFactory = new RenameStrategyFactory();
            _commandManager = new CommandManager();
            _metadataService = new TmdbMetadataService();
            _aiService = new PythonAIService();

            _progressNotifier.Subscribe(this);
            _currentStrategy = _strategyFactory.CreateStrategy(RenameMode.Maintain);

            ConfigureValidation();
            UpdateUIState();
            UpdateStatus("Listo para trabajar...");
        }

        private void ConfigureValidation()
        {
            txtTemporada.Validating += TxtTemporada_Validating;
            txtCapitulo.Validating += TxtCapitulo_Validating;
            txtTitulo.Validating += TxtTitulo_Validating;
            txtTituloEpisodio.Validating += TxtTituloEpisodio_Validating;
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

        public void UpdateStatus(string message)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => UpdateStatus(message)));
                return;
            }
            lblStatus.Text = message;
        }

        

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

            var chapterInfo = new organizadorCapitulos.Core.Entities.ChapterInfo
            {
                Season = temporada,
                Chapter = capitulo,
                Title = txtTitulo.Text.Trim(),
                EpisodeTitle = txtTituloEpisodio.Text.Trim()
            };

            try
            {
                string? directory = Path.GetDirectoryName(originalFilePath);
                if (string.IsNullOrEmpty(directory)) return;

                string extension = Path.GetExtension(originalFilePath);
                string originalNameNoExt = Path.GetFileNameWithoutExtension(originalFilePath);

                string nuevoNombreBase = _currentStrategy.GetNewFileName(originalNameNoExt, chapterInfo);

                string nuevoFilePath = Path.Combine(directory, nuevoNombreBase + extension);

                var renameCommand = new organizadorCapitulos.Application.Commands.RenameFileCommand(_fileRepository, originalFilePath, nuevoFilePath);
                await _commandManager.ExecuteCommandAsync(renameCommand);

                _currentStrategy.UpdateAfterRename(chapterInfo);

                selectedItem.Text = nuevoNombreBase + extension;
                selectedItem.SubItems[1].Text = nuevoFilePath;

                UpdateStatus($"Renombrado a: {nuevoNombreBase}");

                SeleccionarSiguienteItem(selectedItem.Index);

                capitulo++;
                txtCapitulo.Text = capitulo.ToString();

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

            string? carpetaDestino = SeleccionarCarpeta();
            if (string.IsNullOrEmpty(carpetaDestino)) return;

            var sourcePaths = listViewSeries.Items.Cast<ListViewItem>()
                .Select(item => item.SubItems[1].Text)
                .ToList();

            using var progressForm = new ProgressForm();
            progressForm.Show();

            try
            {
                var moveCommand = new organizadorCapitulos.Application.Commands.MoveFilesCommand(_fileRepository, progressForm, sourcePaths, carpetaDestino);
                await _commandManager.ExecuteCommandAsync(moveCommand);

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

            UpdateUIState();
        }

        private async Task CargarCarpetasAsync()
        {
            using var folderBrowser = new FolderBrowserForm();
            if (folderBrowser.ShowDialog(this) != DialogResult.OK || folderBrowser.SelectedFolders.Count == 0)
            {
                return;
            }

            listViewSeries.Items.Clear();
            UpdateStatus("Cargando archivos de video...");

            using var progressForm = new ProgressForm();
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
                    item.SubItems.Add(string.Empty);

                    listViewSeries.Items.Add(item);
                }

                UpdateStatus($"Carga completada: {listViewSeries.Items.Count} archivos encontrados");
                MessageBox.Show($"Se cargaron {listViewSeries.Items.Count} archivos de video.",
                    "Carga completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar archivos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // Ajustes visuales responsivos
        private void MainForm_Resize(object? sender, EventArgs e)
        {
            try
            {
                // Centrar el groupBoxOpciones dentro de panel1
                if (panel1 != null && groupBoxOpciones != null)
                {
                    int x = Math.Max(10, (panel1.ClientSize.Width - groupBoxOpciones.Width) / 2);
                    groupBoxOpciones.Location = new Point(x, groupBoxOpciones.Location.Y);
                }

                // Ajustar anchos de columnas del listView proporcionalmente
                if (listViewSeries != null && listViewSeries.Columns.Count >= 4)
                {
                    int w = listViewSeries.ClientSize.Width;
                    if (w <= 0) return;

                    int col1 = Math.Max(200, (int)(w * 0.30)); // Nombre
                    int col2 = Math.Max(400, (int)(w * 0.45)); // Ruta completa
                    int col3 = Math.Max(150, (int)(w * 0.12)); // Título Episodio
                    int col4 = Math.Max(150, w - (col1 + col2 + col3)); // Vista previa restante

                    listViewSeries.Columns[0].Width = col1;
                    listViewSeries.Columns[1].Width = col2;
                    listViewSeries.Columns[2].Width = col3;
                    listViewSeries.Columns[3].Width = col4;
                }
            }
            catch
            {
                // No interrumpir la UI por errores de layout
            }
        }

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
                        ? $"Nombre del archivo {(_lastSortOrder == SortOrder.Ascending ? "↑" : "↓") }"
                        : $"Ruta completa {(_lastSortOrder == SortOrder.Ascending ? "↑" : "↓") }";
                }
                else
                {
                    column.Text = column.Index == 0 ? "Nombre del archivo" : "Ruta completa";
                }
            }
        }

        private string? SeleccionarCarpeta()
        {
            using var folderBrowser = new FolderBrowserForm();
            folderBrowser.Text = "Selecciona Carpeta Destino";
            folderBrowser.IsSingleSelectionMode = true;

            if (folderBrowser.ShowDialog(this) == DialogResult.OK && folderBrowser.SelectedFolders.Count > 0)
            {
                return folderBrowser.SelectedFolders[0];
            }
            return null;
        }

        private bool TryExtractSeasonEpisode(string filename, out int season, out int episode)
        {
            var match = System.Text.RegularExpressions.Regex.Match(filename, @"S(\d+)E(\d+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (match.Success)
            {
                season = int.Parse(match.Groups[1].Value);
                episode = int.Parse(match.Groups[2].Value);
                return true;
            }

            season = 0;
            episode = 0;
            return false;
        }

        private void UpdatePreview(ListViewItem item)
        {
            while (item.SubItems.Count < 4)
            {
                item.SubItems.Add(string.Empty);
            }

            string originalFilePath = item.SubItems[1].Text;
            string extension = Path.GetExtension(originalFilePath);
            string originalNameNoExt = Path.GetFileNameWithoutExtension(originalFilePath);

            int season = 1;
            int episode = 1;
            string title = txtTitulo.Text;
            string episodeTitle = item.SubItems[2].Text;

            if (item.Selected)
            {
                int.TryParse(txtTemporada.Text, out season);
                int.TryParse(txtCapitulo.Text, out episode);
                title = txtTitulo.Text;
                episodeTitle = txtTituloEpisodio.Text;
            }
            else
            {
                if (TryExtractSeasonEpisode(item.Text, out int s, out int e))
                {
                    season = s;
                    episode = e;
                }
            }

            var chapterInfo = new organizadorCapitulos.Core.Entities.ChapterInfo
            {
                Season = season,
                Chapter = episode,
                Title = title,
                EpisodeTitle = episodeTitle
            };

            try
            {
                string nuevoNombreBase = _currentStrategy.GetNewFileName(originalNameNoExt, chapterInfo);
                item.SubItems[3].Text = nuevoNombreBase + extension;
            }
            catch
            {
                item.SubItems[3].Text = "Error en vista previa";
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
        private void TxtNumerico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtTemporada_Validating(object? sender, CancelEventArgs e)
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

        private void TxtCapitulo_Validating(object? sender, CancelEventArgs e)
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

        private void TxtTitulo_Validating(object? sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                errorProvider.SetError(txtTitulo, "El nombre de la serie no puede estar vacío");
                e.Cancel = true;
            }
            else if (txtTitulo.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                errorProvider.SetError(txtTitulo, "El nombre contiene caracteres no válidos para un nombre de archivo");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtTitulo, "");
            }
        }

        private void TxtTituloEpisodio_Validating(object? sender, CancelEventArgs e)
        {
            if (!txtTituloEpisodio.ReadOnly && !string.IsNullOrWhiteSpace(txtTituloEpisodio.Text))
            {
                if (txtTituloEpisodio.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    errorProvider.SetError(txtTituloEpisodio, "El título contiene caracteres no válidos para un nombre de archivo");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(txtTituloEpisodio, "");
                }
            }
            else
            {
                errorProvider.SetError(txtTituloEpisodio, "");
            }

            if (listViewSeries.SelectedItems.Count > 0)
            {
                UpdatePreview(listViewSeries.SelectedItems[0]);
            }
        }

        void organizadorCapitulos.Core.Interfaces.Observers.IProgressObserver.UpdateStatus(string status)
        {
            UpdateStatus(status);
        }
        #endregion

        #region Event Handlers
        private void ListViewSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSeries.SelectedItems.Count > 0)
            {
                var selectedItem = listViewSeries.SelectedItems[0];

                // Mostrar título del episodio si está disponible en Tag
                if (selectedItem.Tag is string episodeTitle && !string.IsNullOrWhiteSpace(episodeTitle))
                {
                    txtTituloEpisodio.Text = episodeTitle;
                    txtTituloEpisodio.ReadOnly = true;
                    txtTituloEpisodio.BackColor = Color.FromArgb(243, 244, 246); // Gris claro
                }
                else
                {
                    txtTituloEpisodio.Text = string.Empty;
                    txtTituloEpisodio.ReadOnly = false;
                    txtTituloEpisodio.BackColor = Color.White;
                }

                UpdatePreview(selectedItem);
            }
            UpdateUIState();
        }

        private void ListViewSeries_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void RadioMantener_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMantener.Checked)
            {
                _currentStrategy = _strategyFactory.CreateStrategy(RenameMode.Maintain);
                UpdateStatus("Modo: Mantener estructura - El número de capítulo se incrementará automáticamente");
            }
        }

        private void RadioCambiar_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCambiar.Checked)
            {
                _currentStrategy = _strategyFactory.CreateStrategy(RenameMode.Change);
                UpdateStatus("Modo: Cambiar estructura - El número de capítulo se mantendrá igual");
            }
        }

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            await RenombrarCapituloAsync();
        }

        private async void OnClickEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await RenombrarCapituloAsync();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private async void BtnGuardarTodo_Click(object sender, EventArgs e)
        {
            await GuardarTodoAsync();
        }

        private async void BtnCargarCarpetas_Click(object sender, EventArgs e)
        {
            await CargarCarpetasAsync();
        }

        private async void BtnUndo_Click(object sender, EventArgs e)
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

        private async void BtnRedo_Click(object sender, EventArgs e)
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
    }
}
