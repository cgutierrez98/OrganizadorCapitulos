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
            treeViewFolders = new TreeView();
            btnAceptar = new Button();
            btnCancelar = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // treeViewFolders
            // 
            treeViewFolders.CheckBoxes = true;
            treeViewFolders.Dock = DockStyle.Fill;
            treeViewFolders.Location = new Point(3, 3);
            treeViewFolders.Name = "treeViewFolders";
            treeViewFolders.Size = new Size(1221, 779);
            treeViewFolders.TabIndex = 0;
            treeViewFolders.BeforeExpand += treeViewFolders_BeforeExpand;
            // 
            // btnAceptar
            // 
            btnAceptar.Anchor = AnchorStyles.Right;
            btnAceptar.Location = new Point(1150, 833);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(74, 30);
            btnAceptar.TabIndex = 1;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Right;
            btnCancelar.Location = new Point(1150, 792);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(74, 28);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(treeViewFolders, 0, 0);
            tableLayoutPanel1.Controls.Add(btnAceptar, 0, 1);
            tableLayoutPanel1.Controls.Add(btnCancelar, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.Size = new Size(1227, 868);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // FolderBrowserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1227, 868);
            Controls.Add(tableLayoutPanel1);
            Name = "FolderBrowserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Seleccionar Carpetas";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.TreeView treeViewFolders;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}