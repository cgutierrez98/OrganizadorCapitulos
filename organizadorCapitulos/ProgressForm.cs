using System;
using System.Windows.Forms;

namespace organizadorCapitulos
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void UpdateProgress(int current, int total, string filename)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(current, total, filename)));
                return;
            }

            progressBar.Maximum = total;
            progressBar.Value = current;
            lblProgress.Text = $"Procesando {current} de {total} archivos";
            lblFile.Text = $"Archivo actual: {filename}";
            this.Refresh();
        }

        public void UpdateStatus(string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateStatus(status)));
                return;
            }

            lblProgress.Text = status;
            this.Refresh();
        }
    }
}