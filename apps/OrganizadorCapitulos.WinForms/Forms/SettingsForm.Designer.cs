using System.Windows.Forms;
using System.Drawing;

namespace OrganizadorCapitulos.WinForms.UI.Forms
{
    partial class SettingsForm
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
            this.txtApiKey = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            SuspendLayout();
            // 
            // txtApiKey
            // 
            txtApiKey.Location = new Point(15, 15);
            txtApiKey.Size = new Size(360, 23);
            txtApiKey.Name = "txtApiKey";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(200, 50);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(80, 30);
            btnSave.Text = "Guardar";
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(290, 50);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 30);
            btnCancel.Text = "Cancelar";
            btnCancel.Click += BtnCancel_Click;
            // 
            // SettingsForm
            // 
            ClientSize = new Size(400, 95);
            Controls.Add(txtApiKey);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Name = "SettingsForm";
            Text = "Configuración";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtApiKey;
        private Button btnSave;
        private Button btnCancel;
    }
}
