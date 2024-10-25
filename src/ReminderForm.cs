using System;
using System.Windows.Forms;

namespace MorningReminderApp
{
    public partial class ReminderForm : Form
    {
        private AppConfig appConfig;

        public ReminderForm(string reminderMessage)
        {
            InitializeComponent();

            // Load configuration
            appConfig = Program.appConfig;

            // Restore window state
            LoadWindowState();

            this.Load += (sender, e) => ReminderForm_Load(reminderMessage);
            this.FormClosing += ReminderForm_FormClosing;
            this.KeyDown += ReminderForm_KeyDown;
            this.Resize += ReminderForm_Resize;
            this.ResizeEnd += ReminderForm_ResizeEnd;
            this.SizeChanged += ReminderForm_SizeChanged;

            // Disable minimize button
            this.MinimizeBox = false;
        }

        private void ReminderForm_Load(string message)
        {
            Label messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.Dock = DockStyle.Fill;
            messageLabel.TextAlign = ContentAlignment.TopLeft;
            messageLabel.Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);
            this.Controls.Add(messageLabel);
        }

        private void ReminderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveWindowState();
        }

        private void ReminderForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Allow closing with the Escape key
            }
        }

        private void ReminderForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                SaveWindowState();
            }
        }

        private void ReminderForm_ResizeEnd(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                SaveWindowState();
            }
        }

        private void ReminderForm_SizeChanged(object sender, EventArgs e)
        {
            appConfig.IsMaximized = WindowState == FormWindowState.Maximized;
        }

        private void LoadWindowState()
        {
            if (appConfig.IsMaximized)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                this.Width = appConfig.WindowWidth;
                this.Height = appConfig.WindowHeight;
                this.Left = appConfig.WindowLeft;
                this.Top = appConfig.WindowTop;
            }
        }

        private void SaveWindowState()
        {
            if (WindowState == FormWindowState.Normal)
            {
                appConfig.WindowWidth = Width;
                appConfig.WindowHeight = Height;
                appConfig.WindowLeft = Left;
                appConfig.WindowTop = Top;
            }

            appConfig.IsMaximized = WindowState == FormWindowState.Maximized;
            Program.SaveConfiguration();
        }
    }
}
