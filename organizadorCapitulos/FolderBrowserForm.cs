using System;
using System.IO;
using System.Windows.Forms;

namespace organizadorCapitulos
{
    public partial class FolderBrowserForm : Form
    {
        public List<string> SelectedFolders { get; private set; } = new List<string>();

        public FolderBrowserForm()
        {
            InitializeComponent();
            InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            treeViewFolders.CheckBoxes = true;
            treeViewFolders.AfterCheck += TreeViewFolders_AfterCheck;

            // Agregar unidades lógicas
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    TreeNode driveNode = new TreeNode(drive.Name) { Tag = drive.Name };
                    treeViewFolders.Nodes.Add(driveNode);
                    LoadSubdirectories(driveNode);
                }
            }
        }

        private void LoadSubdirectories(TreeNode parentNode)
        {
            try
            {
                string path = parentNode.Tag as string;
                if (string.IsNullOrEmpty(path)) return;

                parentNode.Nodes.Clear();
                foreach (string directory in Directory.GetDirectories(path))
                {
                    TreeNode dirNode = new TreeNode(Path.GetFileName(directory)) { Tag = directory };
                    parentNode.Nodes.Add(dirNode);

                    // Agregar nodo ficticio para mostrar el signo +
                    dirNode.Nodes.Add(new TreeNode("..."));
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Ignorar directorios sin acceso
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar directorios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TreeViewFolders_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                SelectedFolders.Add(e.Node.Tag.ToString());
            }
            else
            {
                SelectedFolders.Remove(e.Node.Tag.ToString());
            }
        }

        private void treeViewFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "...")
            {
                e.Node.Nodes.Clear();
                LoadSubdirectories(e.Node);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (SelectedFolders.Count == 0)
            {
                MessageBox.Show("Por favor seleccione al menos una carpeta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}