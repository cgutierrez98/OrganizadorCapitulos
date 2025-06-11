using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace organizadorCapitulos
{
    public partial class MainForm : Form
    {
        private string ruta_carpeta = "D:\\Series_movidas";
        private string[] videoExtensions = { ".mp4", ".avi", ".mkv", ".mov", ".wmv", ".flv", ".mpeg", ".webm" };
        private SortOrder lastSortOrder = SortOrder.None;
        private int lastColumnSorted = -1;
        public MainForm()
        {
            InitializeComponent();
        }

        private void listViewSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSeries.SelectedItems.Count > 0 && radioCambiar.Checked)
            {
                string fileName = listViewSeries.SelectedItems[0].Text;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                txtTitulo.Text = fileNameWithoutExtension;
            }
        }
        

      
        private void listViewSeries_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lastColumnSorted)
            {
                lastSortOrder = lastSortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                lastSortOrder = SortOrder.Ascending;
                lastColumnSorted = e.Column;
            }

            listViewSeries.ListViewItemSorter = new ListViewItemComparer(e.Column, lastSortOrder);
            listViewSeries.Sort();
            UpdateColumnHeaders(e.Column);
        }

        private void UpdateColumnHeaders(int sortedColumn)
        {
            foreach (ColumnHeader column in listViewSeries.Columns)
            {
                if (column.Index == sortedColumn)
                {
                    column.Text = column.Index == 0
                        ? $"Nombre del archivo {(lastSortOrder == SortOrder.Ascending ? "↑" : "↓")}"
                        : $"Ruta completa {(lastSortOrder == SortOrder.Ascending ? "↑" : "↓")}";
                }
                else
                {
                    column.Text = column.Index == 0 ? "Nombre del archivo" : "Ruta completa";
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (listViewSeries.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos())
                return;

            ListViewItem selectedItem = listViewSeries.SelectedItems[0];
            string originalFilePath = selectedItem.SubItems[1].Text;
            string originalFileExtension = Path.GetExtension(originalFilePath);
            string directory = Path.GetDirectoryName(originalFilePath);

            string nuevoNombre = GenerarNuevoNombre(originalFileExtension);
            string nuevoFilePath = Path.Combine(directory, nuevoNombre);

            try
            {
                // Verificar si el archivo de destino ya existe
                if (File.Exists(nuevoFilePath))
                {
                    MessageBox.Show($"Ya existe un archivo con el nombre {nuevoNombre} en esta ubicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Renombrar el archivo
                File.Move(originalFilePath, nuevoFilePath);

                // Actualizar el ListView con el nuevo nombre y ruta
                selectedItem.Text = nuevoNombre;
                selectedItem.SubItems[1].Text = nuevoFilePath;

                // Incrementar el número de capítulo para el próximo archivo
                IncrementarCapitulo();

                MessageBox.Show("Archivo renombrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("No tiene permisos para renombrar este archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error al renombrar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardarTodo_Click(object sender, EventArgs e)
        {
            if (listViewSeries.Items.Count == 0)
            {
                MessageBox.Show("No hay archivos para guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(ruta_carpeta))
            {
                Directory.CreateDirectory(ruta_carpeta);
            }

            using (ProgressForm progressForm = new ProgressForm())
            {
                progressForm.Show();

                int totalFiles = listViewSeries.Items.Count;
                int processedFiles = 0;

                foreach (ListViewItem item in listViewSeries.Items)
                {
                    string originalFilePath = item.SubItems[1].Text;
                    string fileName = Path.GetFileName(originalFilePath);
                    string destinoPath = Path.Combine(ruta_carpeta, fileName);

                    try
                    {
                        processedFiles++;
                        progressForm.UpdateProgress(processedFiles, totalFiles, fileName);

                         File.Move(originalFilePath, destinoPath);

                        //await CopyLargeFileAsync(originalFilePath, destinoPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al copiar el archivo: {originalFilePath}\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                progressForm.Close();
            }

            MessageBox.Show("Todos los archivos fueron copiados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnCargarCarpetas_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserForm())
            {
                if (folderBrowser.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                if (folderBrowser.SelectedFolders.Count == 0)
                {
                    MessageBox.Show("No se seleccionaron carpetas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                listViewSeries.Items.Clear();

                using (ProgressForm progressForm = new ProgressForm())
                {
                    progressForm.Show();
                    progressForm.UpdateStatus("Buscando archivos de video...");

                    var files = await Task.Run(() =>
                    {
                        var extensions = new HashSet<string>(videoExtensions.Select(ext => ext.ToLower()));
                        List<string> allFiles = new List<string>();

                        foreach (string folder in folderBrowser.SelectedFolders)
                        {
                            try
                            {
                                var folderFiles = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)
                                    .Where(file => extensions.Contains(Path.GetExtension(file).ToLower()));
                                allFiles.AddRange(folderFiles);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error al acceder a {folder}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        return allFiles;
                    });

                    progressForm.UpdateStatus("Cargando archivos...");
                    int totalFiles = files.Count;
                    int processedFiles = 0;

                    foreach (string file in files)
                    {
                        processedFiles++;
                        progressForm.UpdateProgress(processedFiles, totalFiles, Path.GetFileName(file));

                        ListViewItem item = new ListViewItem(Path.GetFileName(file));
                        item.SubItems.Add(file);
                        listViewSeries.Items.Add(item);
                    }

                    progressForm.Close();
                }

                MessageBox.Show($"Se cargaron {listViewSeries.Items.Count} archivos de video.", "Carga completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("Por favor ingrese un título válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtTemporada.Text, out _))
            {
                MessageBox.Show("Por favor ingrese un número de temporada válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtCapitulo.Text, out _))
            {
                MessageBox.Show("Por favor ingrese un número de capítulo válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private string GenerarNuevoNombre(string extension)
        {
            int temporada = int.Parse(txtTemporada.Text);
            int capitulo = int.Parse(txtCapitulo.Text);
            return $"{txtTitulo.Text.Trim()} S{temporada:D2}E{capitulo:D2}{extension}";
        }

        private void IncrementarCapitulo()
        {
            if (int.TryParse(txtCapitulo.Text, out int capitulo))
            {
                txtCapitulo.Text = (capitulo + 1).ToString();
            }
        }

        private async Task CopyLargeFileAsync(string sourcePath, string destinationPath)
        {
            const int bufferSize = 81920;
            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
            using (FileStream destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
            {
                await sourceStream.CopyToAsync(destinationStream, bufferSize);
            }
        }
    }
}