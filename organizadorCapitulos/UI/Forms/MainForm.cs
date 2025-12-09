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
using organizadorCapitulos.Core.Interfaces.Services;
using organizadorCapitulos.Core.Interfaces.Strategies;
using organizadorCapitulos.Infrastructure.Repositories;
using organizadorCapitulos.Infrastructure.Services;
using organizadorCapitulos.UI.Forms;
using SortOrder = System.Windows.Forms.SortOrder;


namespace organizadorCapitulos.UI.Forms
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

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            // We'll use a property or method to get/set the key. For now let's assume we pass empty string if not set.
            // In a real app we would store this in Properties.Settings.Default
            string currentKey = _metadataService.IsConfigured() ? "********" : "";

            using var settingsForm = new SettingsForm(currentKey);
            if (settingsForm.ShowDialog(this) == DialogResult.OK)
            {
                _metadataService.Configure(settingsForm.ApiKey ?? string.Empty);
                UpdateStatus("Configuración actualizada.");
            }
        }

        private async void BtnMetadata_Click(object sender, EventArgs e)
        {
            if (!_metadataService.IsConfigured())
            {
                MessageBox.Show("Por favor configura la API Key de TMDB primero.", "Configuración requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BtnSettings_Click(sender, e);
                if (!_metadataService.IsConfigured()) return;
            }

            if (listViewSeries.Items.Count == 0)
            {
                MessageBox.Show("Carga archivos primero.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 1. Guess Series Name from the first file (simplified for now)
            string firstFile = listViewSeries.Items[0].Text;
            // Simple heuristic: take first part before "S01" or similar, or just ask user
            string query = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre de la serie para buscar:", "Buscar Serie", Path.GetFileNameWithoutExtension(firstFile));

            if (string.IsNullOrWhiteSpace(query)) return;

            UpdateStatus("Buscando serie...");
            var results = await _metadataService.SearchSeriesAsync(query);

            if (results.Count == 0)
            {
                MessageBox.Show("No se encontraron series.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateStatus("Búsqueda sin resultados.");
                return;
            }

            // 2. Select Series (Take first for now, or implement selection dialog later)
            var selectedSeries = results[0];
            txtTitulo.Text = selectedSeries.Name; // Guardar nombre de la serie
            UpdateStatus($"Serie seleccionada: {selectedSeries.Name} ({selectedSeries.FirstAirDate})");

            // 3. Fetch Episodes
            using (var progressForm = new ProgressForm())
            {
                progressForm.Show();
                int total = listViewSeries.Items.Count;
                int current = 0;

                foreach (ListViewItem item in listViewSeries.Items)
                {
                    current++;
                    string filename = item.Text;
                    progressForm.UpdateProgress(current, total, filename);

                    // Try to parse S and E from filename
                    if (TryExtractSeasonEpisode(filename, out int season, out int episode))
                    {
                        string? title = await _metadataService.GetEpisodeTitleAsync(selectedSeries.Id, season, episode);
                        if (!string.IsNullOrEmpty(title))
                        {
                            // Store the found title in the Tag (for later use)
                            item.Tag = title;

                            // Ensure we have enough subitems
                            while (item.SubItems.Count < 4)
                            {
                                item.SubItems.Add(string.Empty);
                            }

                            item.SubItems[2].Text = title;
                        }
                    }
                }

            }

            // Refresh to make sure UI updates
            listViewSeries.Refresh();

            UpdateStatus("Metadatos descargados. Selecciona un archivo para ver el título sugerido.");
            MessageBox.Show("Búsqueda completada. Los títulos se han guardado en memoria y se muestran en la lista.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Update preview for all items
            foreach (ListViewItem item in listViewSeries.Items)
            {
                UpdatePreview(item);
            }
        }

        private async void BtnAIAnalyze_Click(object sender, EventArgs e)
        {
            if (!_aiService.IsAvailable())
            {
                MessageBox.Show("No se encontró el servicio de IA (Python o ejecutable). Por favor verifica la instalación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listViewSeries.Items.Count == 0)
            {
                MessageBox.Show("Carga archivos primero.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UpdateStatus("Iniciando análisis con IA...");

            using (var progressForm = new ProgressForm())
            {
                progressForm.Show();
                int total = listViewSeries.Items.Count;
                int current = 0;

                foreach (ListViewItem item in listViewSeries.Items)
                {
                    current++;
                    string filename = item.Text;
                    progressForm.UpdateProgress(current, total, $"Analizando: {filename}");

                    var info = await _aiService.AnalyzeFilenameAsync(filename);
                    if (info != null)
                    {
                        // Update UI with AI results
                        // Temporada
                        if (info.Season > 0)
                        {
                            // Store in a temporary way or update the textboxes if selected?
                            // For now, let's update the item's Tag or SubItems if we want to persist it
                        }

                        // Titulo Episodio
                        if (!string.IsNullOrEmpty(info.EpisodeTitle))
                        {
                            item.Tag = info.EpisodeTitle;
                            while (item.SubItems.Count < 3) item.SubItems.Add("");
                            item.SubItems[2].Text = info.EpisodeTitle;
                        }

                        // If we found season/episode, we might want to override the default parsing
                        // But the current architecture parses on the fly in UpdatePreview using TryExtractSeasonEpisode
                        // We should probably update that method to check for AI data attached to the item

                        // Let's attach the full info object to the item for UpdatePreview to use
                        // But Tag is currently string (EpisodeTitle). Let's change Tag to be more flexible or use a dictionary?
                        // For simplicity, let's just update the text if it's selected, or rely on the user seeing the preview.

                        // Actually, UpdatePreview calls TryExtractSeasonEpisode(item.Text...)
                        // We can't easily inject the AI result into that logic without refactoring.
                        // So let's do a trick: We will rename the file in the ListView to a "normalized" standard format that our regex understands perfectly?
                        // No, that changes the source filename visually which is confusing.

                        // Better approach: Store the AI result in a Dictionary<ListViewItem, ChapterInfo> in the form?
                        // Or just use the Tag. Currently Tag is `string episodeTitle`.
                        // Let's make Tag a `ChapterInfo` object?
                        // Existing code: `if (selectedItem.Tag is string episodeTitle ...)`
                        // I should verify if I can change Tag usage safely.

                        // Let's check `listViewSeries_SelectedIndexChanged`:
                        // if (selectedItem.Tag is string episodeTitle && !string.IsNullOrWhiteSpace(episodeTitle))

                        // I will change Tag to hold ChapterInfo, but I need to update the other usage.
                        // OR, I can just keep Tag as string for Episode Title (which is what AI gives mostly)
                        // AND maybe encode Season/Episode in the string? No that's hacky.

                        // Let's just update the Episode Title for now, which is the main value add.
                        // And if the AI finds a Series Name, we can update txtTitulo if it's the first item.

                        if (current == 1 && !string.IsNullOrEmpty(info.Title))
                        {
                            txtTitulo.Text = info.Title;
                        }
                    }
                }
            }

            UpdateStatus("Análisis de IA completado.");
            foreach (ListViewItem item in listViewSeries.Items) UpdatePreview(item);
        }

        private bool TryExtractSeasonEpisode(string filename, out int season, out int episode)
        {
            // Simple Regex for SxxExx
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
                Title = txtTitulo.Text.Trim(),
                EpisodeTitle = txtTituloEpisodio.Text.Trim()
            };

            try
            {
                // 3. CALCULAR (Sin mover nada)
                // Obtenemos el directorio y la extensión del archivo original
                string? directory = Path.GetDirectoryName(originalFilePath);
                if (string.IsNullOrEmpty(directory)) return;

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

            string? carpetaDestino = SeleccionarCarpeta();
            if (string.IsNullOrEmpty(carpetaDestino)) return;

            var sourcePaths = listViewSeries.Items.Cast<ListViewItem>()
                .Select(item => item.SubItems[1].Text)
                .ToList();

            using ProgressForm progressForm = new();
            progressForm.Show();

            try
            {
                var moveCommand = new MoveFilesCommand(_fileRepository, progressForm, sourcePaths, carpetaDestino);
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

            using ProgressForm progressForm = new();
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

                    // Ensure we have a third (title) column available from the start so later metadata can be placed at SubItems[2]
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

        private void UpdatePreview(ListViewItem item)
        {
            // Ensure we have enough subitems
            while (item.SubItems.Count < 4)
            {
                item.SubItems.Add(string.Empty);
            }

            string originalFilePath = item.SubItems[1].Text;
            string extension = Path.GetExtension(originalFilePath);
            string originalNameNoExt = Path.GetFileNameWithoutExtension(originalFilePath);

            // Try to extract info from the item or use defaults
            int season = 1;
            int episode = 1;
            string title = txtTitulo.Text;
            string episodeTitle = item.SubItems[2].Text; // Get from column 2

            // If this is the selected item, use the textboxes (which might be edited)
            if (item.Selected)
            {
                int.TryParse(txtTemporada.Text, out season);
                int.TryParse(txtCapitulo.Text, out episode);
                title = txtTitulo.Text;
                episodeTitle = txtTituloEpisodio.Text;
            }
            else
            {
                // Fallback to parsing filename if not selected/edited
                if (TryExtractSeasonEpisode(item.Text, out int s, out int e))
                {
                    season = s;
                    episode = e;
                }
            }

            var chapterInfo = new ChapterInfo
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
            // Solo validar si no es ReadOnly (es decir, entrada manual)
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

        void IProgressObserver.UpdateStatus(string status)
        {
            UpdateStatus(status);
        }
        #endregion
    }
}
