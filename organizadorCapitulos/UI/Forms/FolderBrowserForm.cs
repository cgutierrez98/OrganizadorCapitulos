using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace organizadorCapitulos
{
    public partial class FolderBrowserForm : Form
    {
        public List<string> SelectedFolders { get; private set; } = [];
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsSingleSelectionMode { get; set; } = false;

        public FolderBrowserForm()
        {
            InitializeComponent();
            InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            treeViewFolders.CheckBoxes = true;
            // treeViewFolders.AfterCheck += treeViewFolders_AfterCheck; // Removed to avoid double subscription (already in Designer)

            // Agregar unidades lógicas
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    TreeNode driveNode = new(drive.Name) { Tag = drive.Name };
                    treeViewFolders.Nodes.Add(driveNode);
                    LoadSubdirectories(driveNode);
                }
            }
        }

        private static void LoadSubdirectories(TreeNode parentNode)
        {
            try
            {
                string? path = parentNode.Tag as string;
                if (string.IsNullOrEmpty(path)) return;

                parentNode.Nodes.Clear();
                foreach (string directory in Directory.GetDirectories(path))
                {
                    TreeNode dirNode = new(Path.GetFileName(directory)) { Tag = directory };
                    parentNode.Nodes.Add(dirNode);

                    // Agregar nodo ficticio para mostrar el signo +
                    _ = dirNode.Nodes.Add(new TreeNode("..."));
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

        private void TreeViewFolders_AfterCheck(object? sender, TreeViewEventArgs e)
        {
            // Evitar reentrancia si estamos modificando programáticamente
            treeViewFolders.AfterCheck -= TreeViewFolders_AfterCheck;

            try
            {
                if (e.Node == null) return;

                if (IsSingleSelectionMode && e.Node.Checked)
                {
                    UncheckOtherNodes(treeViewFolders.Nodes, e.Node);
                    SelectedFolders.Clear(); // Limpiar selección anterior
                }

                string? tag = e.Node.Tag?.ToString();
                if (tag != null)
                {
                    if (e.Node.Checked)
                    {
                        if (!SelectedFolders.Contains(tag))
                            SelectedFolders.Add(tag);
                    }
                    else
                    {
                        SelectedFolders.Remove(tag);
                    }
                }

                // Actualizar el contador de carpetas seleccionadas
                int selectedCount = CountCheckedNodes(treeViewFolders.Nodes);
                lblStatus.Text = IsSingleSelectionMode
                    ? (selectedCount > 0 ? "Carpeta seleccionada" : "Ninguna carpeta seleccionada")
                    : $"{selectedCount} carpetas seleccionadas";
            }
            finally
            {
                treeViewFolders.AfterCheck += TreeViewFolders_AfterCheck;
            }
        }

        private void UncheckOtherNodes(TreeNodeCollection nodes, TreeNode current)
        {
            foreach (TreeNode node in nodes)
            {
                if (node != current && node.Checked)
                {
                    node.Checked = false;
                }
                UncheckOtherNodes(node.Nodes, current);
            }
        }

        private void treeViewFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "...")
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

            if (IsSingleSelectionMode && SelectedFolders.Count > 1)
            {
                MessageBox.Show("Por favor seleccione solo una carpeta destino.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private int CountCheckedNodes(TreeNodeCollection nodes)
        {
            int count = 0;
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                    count++;
                count += CountCheckedNodes(node.Nodes);
            }
            return count;
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            treeViewFolders.BeginUpdate();
            try
            {
                ExpandAllNodes(treeViewFolders.Nodes, true);
            }
            finally
            {
                treeViewFolders.EndUpdate();
            }
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            treeViewFolders.BeginUpdate();
            try
            {
                ExpandAllNodes(treeViewFolders.Nodes, false);
            }
            finally
            {
                treeViewFolders.EndUpdate();
            }
        }

        private void ExpandAllNodes(TreeNodeCollection nodes, bool expand)
        {
            foreach (TreeNode node in nodes)
            {
                node.Expand();
                if (expand)
                    node.ExpandAll();
                else
                    node.Collapse();
                ExpandAllNodes(node.Nodes, expand);
            }
        }
    }
}