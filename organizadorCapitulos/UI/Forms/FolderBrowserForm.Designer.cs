namespace organizadorCapitulos
{
    partial class FolderBrowserForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderBrowserForm));
            treeViewFolders = new TreeView();
            btnAceptar = new Button();
            btnCancelar = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            panelButtons = new Panel();
            btnExpandAll = new Button();
            btnCollapseAll = new Button();
            lblStatus = new Label();
            tableLayoutPanel1.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // treeViewFolders
            // 
            treeViewFolders.BackColor = Color.FromArgb(250, 251, 252);
            treeViewFolders.BorderStyle = BorderStyle.None;
            treeViewFolders.CheckBoxes = true;
            treeViewFolders.Dock = DockStyle.Fill;
            treeViewFolders.Font = new Font("Segoe UI", 10F);
            treeViewFolders.FullRowSelect = true;
            treeViewFolders.HideSelection = false;
            treeViewFolders.Indent = 20;
            treeViewFolders.ItemHeight = 24;
            treeViewFolders.Location = new Point(15, 15);
            treeViewFolders.Margin = new Padding(15);
            treeViewFolders.Name = "treeViewFolders";
            treeViewFolders.Size = new Size(1197, 778);
            treeViewFolders.TabIndex = 0;
            treeViewFolders.AfterCheck += TreeViewFolders_AfterCheck;
            treeViewFolders.BeforeExpand += treeViewFolders_BeforeExpand;
            // 
            // btnAceptar
            // 
            btnAceptar.Anchor = AnchorStyles.Right;
            btnAceptar.BackColor = Color.FromArgb(16, 185, 129);
            btnAceptar.Cursor = Cursors.Hand;
            btnAceptar.FlatAppearance.BorderSize = 0;
            btnAceptar.FlatAppearance.MouseOverBackColor = Color.FromArgb(5, 150, 105);
            btnAceptar.FlatStyle = FlatStyle.Flat;
            btnAceptar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAceptar.ForeColor = Color.White;
            btnAceptar.ImageAlign = ContentAlignment.MiddleLeft;
            btnAceptar.Location = new Point(937, 12);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(130, 38);
            btnAceptar.TabIndex = 1;
            btnAceptar.Text = "✓ Aceptar";
            btnAceptar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Right;
            btnCancelar.BackColor = Color.FromArgb(239, 68, 68);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 38, 38);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(1077, 12);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 38);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "✕ Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(treeViewFolders, 0, 0);
            tableLayoutPanel1.Controls.Add(panelButtons, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.Size = new Size(1227, 868);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(249, 250, 251);
            panelButtons.Controls.Add(btnExpandAll);
            panelButtons.Controls.Add(btnCollapseAll);
            panelButtons.Controls.Add(lblStatus);
            panelButtons.Controls.Add(btnCancelar);
            panelButtons.Controls.Add(btnAceptar);
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.Location = new Point(0, 808);
            panelButtons.Margin = new Padding(0);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(1227, 60);
            panelButtons.TabIndex = 4;
            // 
            // btnExpandAll
            // 
            btnExpandAll.Anchor = AnchorStyles.Left;
            btnExpandAll.BackColor = Color.FromArgb(209, 213, 219);
            btnExpandAll.Cursor = Cursors.Hand;
            btnExpandAll.FlatAppearance.BorderSize = 0;
            btnExpandAll.FlatAppearance.MouseOverBackColor = Color.FromArgb(156, 163, 175);
            btnExpandAll.FlatStyle = FlatStyle.Flat;
            btnExpandAll.Font = new Font("Segoe UI", 9F);
            btnExpandAll.ForeColor = Color.FromArgb(31, 41, 55);
            btnExpandAll.ImageAlign = ContentAlignment.MiddleLeft;
            btnExpandAll.Location = new Point(300, 15);
            btnExpandAll.Name = "btnExpandAll";
            btnExpandAll.Size = new Size(110, 32);
            btnExpandAll.TabIndex = 4;
            btnExpandAll.Text = "▼ Expandir todo";
            btnExpandAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExpandAll.UseVisualStyleBackColor = false;
            btnExpandAll.Click += btnExpandAll_Click;
            // 
            // btnCollapseAll
            // 
            btnCollapseAll.Anchor = AnchorStyles.Left;
            btnCollapseAll.BackColor = Color.FromArgb(209, 213, 219);
            btnCollapseAll.Cursor = Cursors.Hand;
            btnCollapseAll.FlatAppearance.BorderSize = 0;
            btnCollapseAll.FlatAppearance.MouseOverBackColor = Color.FromArgb(156, 163, 175);
            btnCollapseAll.FlatStyle = FlatStyle.Flat;
            btnCollapseAll.Font = new Font("Segoe UI", 9F);
            btnCollapseAll.ForeColor = Color.FromArgb(31, 41, 55);
            btnCollapseAll.ImageAlign = ContentAlignment.MiddleLeft;
            btnCollapseAll.Location = new Point(425, 15);
            btnCollapseAll.Name = "btnCollapseAll";
            btnCollapseAll.Size = new Size(110, 32);
            btnCollapseAll.TabIndex = 5;
            btnCollapseAll.Text = "▶ Colapsar todo";
            btnCollapseAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCollapseAll.UseVisualStyleBackColor = false;
            btnCollapseAll.Click += btnCollapseAll_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(107, 114, 128);
            lblStatus.Location = new Point(20, 22);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(136, 15);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "📁 0 carpetas seleccionadas";
            // 
            // FolderBrowserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1227, 868);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(800, 600);
            Name = "FolderBrowserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Seleccionar Carpetas";
            tableLayoutPanel1.ResumeLayout(false);
            panelButtons.ResumeLayout(false);
            panelButtons.PerformLayout();
            ResumeLayout(false);
        }

        private TreeView treeViewFolders;
        private Button btnAceptar;
        private Button btnCancelar;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelButtons;
        private Label lblStatus;
        private Button btnExpandAll;
        private Button btnCollapseAll;
    }
}