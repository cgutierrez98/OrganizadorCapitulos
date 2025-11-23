using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using organizadorCapitulos.Core.Interfaces.Observers;

namespace organizadorCapitulos
{
    public partial class ProgressForm : Form, IProgressObserver
    {
        // Custom controls for modern progress bar
        private Panel pnlTrack;
        private Panel pnlProgress;
        private System.Windows.Forms.Timer animationTimer;
        private float currentWidth = 0;
        private float targetWidth = 0;
        private int totalFiles = 1;

        public ProgressForm()
        {
            InitializeComponent();
            ConfigureModernStyle();
        }

        private void ConfigureModernStyle()
        {
            // Form Style
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.Size = new Size(500, 160);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Add a simple border via padding or painting
            this.Padding = new Padding(1);

            // Track (Background of progress bar)
            pnlTrack = new Panel();
            pnlTrack.Size = new Size(460, 8);
            pnlTrack.Location = new Point(20, 60);
            pnlTrack.BackColor = Color.FromArgb(243, 244, 246); // Gray-100
            pnlTrack.Parent = this;

            // Progress (The actual bar)
            pnlProgress = new Panel();
            pnlProgress.Height = 8;
            pnlProgress.Width = 0;
            pnlProgress.Location = new Point(0, 0);
            pnlProgress.BackColor = Color.FromArgb(59, 130, 246); // Blue-500
            pnlProgress.Parent = pnlTrack;

            // Adjust Labels
            lblProgress.Location = new Point(20, 20);
            lblProgress.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblProgress.ForeColor = Color.FromArgb(31, 41, 55); // Gray-800

            lblFile.Location = new Point(20, 80);
            lblFile.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblFile.ForeColor = Color.FromArgb(107, 114, 128); // Gray-500
            lblFile.MaximumSize = new Size(460, 40); // Allow multiline if needed

            // Animation Timer
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 16; // ~60 FPS
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Smooth interpolation
            if (Math.Abs(currentWidth - targetWidth) > 0.1f)
            {
                currentWidth = currentWidth + (targetWidth - currentWidth) * 0.15f;
                pnlProgress.Width = (int)currentWidth;
            }
            else
            {
                pnlProgress.Width = (int)targetWidth;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw a subtle border
            using (Pen p = new Pen(Color.FromArgb(229, 231, 235), 1))
            {
                e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        public void UpdateProgress(int current, int total, string filename)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(current, total, filename)));
                return;
            }

            this.totalFiles = total;

            // Calculate target width
            float percentage = (float)current / total;
            if (percentage > 1) percentage = 1;

            targetWidth = pnlTrack.Width * percentage;

            lblProgress.Text = $"Procesando... {Math.Round(percentage * 100)}%";
            lblFile.Text = filename;

            // Force UI refresh for labels
            lblProgress.Update();
            lblFile.Update();
        }

        public void UpdateStatus(string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateStatus(status)));
                return;
            }
            lblProgress.Text = status;
            lblProgress.Update();
        }

        // Allow dragging the borderless form
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84) // WM_NCHITTEST
            {
                m.Result = (IntPtr)0x2; // HTCAPTION
            }
        }
    }
}