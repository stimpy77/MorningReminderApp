using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace MorningReminderApp
{
    public partial class ConfiguratorForm : Form
    {
        private NotifyIcon trayIcon;
        private AppConfig appConfig;
        private Timer notificationTimer;
        private DateTime? _lastNotificationTime;
        private bool _isClosing;

        public ConfiguratorForm(AppConfig config)
        {
            InitializeComponent();
            appConfig = config;
            LoadSettings();

            // Setup tray icon
            trayIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Text = "Morning Reminder App",
                Visible = true
            };
            trayIcon.DoubleClick += TrayIcon_DoubleClick;

            this.Resize += ConfiguratorForm_Resize;
            this.FormClosing += ConfiguratorForm_FormClosing;

            SetupNotificationTimer();
        }

        private void SetupNotificationTimer()
        {
            notificationTimer = new Timer
            {
                Interval = 60000 // 1 minute
            };
            notificationTimer.Tick += NotificationTimer_Tick;
            notificationTimer.Start();
        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.TimeOfDay.Hours == appConfig.EveningNotificationTime.Hours &&
                DateTime.Now.TimeOfDay.Minutes == appConfig.EveningNotificationTime.Minutes &&
                (_lastNotificationTime == null || _lastNotificationTime.Value.Day < DateTime.Now.Day))
            {
                ShowEveningNotification();
            }
        }

        private void ShowEveningNotification()
        {
            trayIcon.BalloonTipTitle = "Reminder Configuration";
            trayIcon.BalloonTipText = "It's time to configure tomorrow's morning reminder.";
            trayIcon.BalloonTipClicked -= TrayIcon_BalloonTipClicked; // Avoid multiple subscriptions
            trayIcon.BalloonTipClicked += TrayIcon_BalloonTipClicked;
            trayIcon.ShowBalloonTip(30000);
            _lastNotificationTime = DateTime.Now;
        }

        private void TrayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            TrayIcon_DoubleClick(sender, e);
        }

        private void LoadSettings()
        {
            timePickerStart.Value = DateTime.Today.Add(appConfig.StartTime);
            timePickerEnd.Value = DateTime.Today.Add(appConfig.EndTime);
            numericInactivityHours.Value = appConfig.InactivityHours;
            textReminderFile.Text = appConfig.ReminderFile;
            timePickerEveningNotification.Value = DateTime.Today.Add(appConfig.EveningNotificationTime);
        }

        private void SaveSettings()
        {
            appConfig.StartTime = timePickerStart.Value.TimeOfDay;
            appConfig.EndTime = timePickerEnd.Value.TimeOfDay;
            appConfig.InactivityHours = (int)numericInactivityHours.Value;
            appConfig.ReminderFile = textReminderFile.Text;
            appConfig.EveningNotificationTime = timePickerEveningNotification.Value.TimeOfDay;
            Program.SaveConfiguration();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
            MessageBox.Show("Settings saved.", "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonEditReminderFile_Click(object sender, EventArgs e)
        {
            string reminderFilePath = Path.Join(Environment.GetEnvironmentVariable("USERPROFILE"), appConfig.ReminderFile);

            if (!File.Exists(reminderFilePath))
            {
                File.WriteAllText(reminderFilePath, "Good Morning! Edit this file for your custom reminder.");
            }

            Process.Start("notepad.exe", reminderFilePath);
        }

        private void ButtonTest_Click(object sender, EventArgs e)
        {
            string reminderMessage = Program.LoadReminderMessage();
            ReminderForm reminderForm = new ReminderForm(reminderMessage);
            reminderForm.ShowDialog();
        }

        private void ConfiguratorForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                trayIcon.Visible = true;
            }
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void ConfiguratorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                _isClosing = true;
                trayIcon.Dispose();
                notificationTimer.Dispose();
                Application.Exit();
            }
            var result = _isClosing
                ? DialogResult.Yes
                : MessageBox.Show(
                    "Do you really want to exit the application?\n\nYes: Exit\nNo: Minimize to tray\nCancel: Keep open",
                    "Exit Confirmation",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    _isClosing = true;
                    trayIcon.Dispose();
                    notificationTimer.Dispose();
                    Application.Exit();
                    break;

                case DialogResult.No:
                    e.Cancel = true;
                    Hide();
                    break;

                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}
