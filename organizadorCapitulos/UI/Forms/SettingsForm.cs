using System;
using System.Windows.Forms;

namespace organizadorCapitulos.UI.Forms
{
    public partial class SettingsForm : Form
    {
        public string ApiKey { get; private set; }

        public SettingsForm(string currentApiKey)
        {
            InitializeComponent();
            txtApiKey.Text = currentApiKey;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ApiKey = txtApiKey.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
