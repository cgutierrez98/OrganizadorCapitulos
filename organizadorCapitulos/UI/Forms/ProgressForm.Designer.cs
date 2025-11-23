namespace organizadorCapitulos
{
    partial class ProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblProgress.Location = new System.Drawing.Point(20, 20);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 19);
            this.lblProgress.TabIndex = 1;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFile.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblFile.Location = new System.Drawing.Point(20, 95);
            this.lblFile.MaximumSize = new System.Drawing.Size(460, 0);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(0, 15);
            this.lblFile.TabIndex = 2;
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(500, 140);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.lblProgress);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "⏳ Progreso";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblFile;
    }
}