using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.Json;
using Timer = System.Windows.Forms.Timer;

namespace MorningReminderApp
{
    static class Program
    {
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        static Timer checkTimer;
        internal static AppConfig appConfig;
        static DateTime lastReminderDate = DateTime.MinValue; // Tracks the last date the reminder was shown
        static Form? reminderForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load the configuration
            LoadConfiguration();

            // Show the configurator form
            ConfiguratorForm configuratorForm = new ConfiguratorForm(appConfig);
            configuratorForm.Show();

            // Set up the timer to check inactivity every minute
            checkTimer = new Timer
            {
                Interval = 60000 // 1 minute
            };
            checkTimer.Tick += CheckInactivity!;
            checkTimer.Start();

            // Listen for power mode changes
            SystemEvents.PowerModeChanged += OnPowerModeChanged;

            // Run the application
            Application.Run();
        }

        static void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Resume)
            {
                // Reset the inactivity check state
                checkTimer.Start();
            }
        }

        internal static void LoadConfiguration()
        {
            string configPath = "config.json";
            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);
                appConfig = JsonSerializer.Deserialize<AppConfig>(json)!;
            }
            else
            {
                appConfig = new AppConfig();
                SaveConfiguration();
            }
        }

        internal static string LoadReminderMessage()
        {
            string profileMessagePath = Path.Join(Environment.GetEnvironmentVariable("USERPROFILE"), appConfig.ReminderFile);
            if (File.Exists(profileMessagePath))
            {
                return File.ReadAllText(profileMessagePath);
            }
            string programFileMessagePath = "reminder.txt";
            if (File.Exists(programFileMessagePath))
            {
                return File.ReadAllText(programFileMessagePath);
            }

            return appConfig.ReminderMessage;
        }

        internal static void SaveConfiguration()
        {
            string json = JsonSerializer.Serialize(appConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("config.json", json);
        }

        static void CheckInactivity(object sender, EventArgs e)
        {
            if (IsUserInactive() && IsInTimeWindow())
            {
                ShowReminder();
            }
        }

        static bool IsUserInactive()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);

            if (!GetLastInputInfo(ref lastInputInfo))
                return false;

            uint idleTime = (uint)Environment.TickCount - lastInputInfo.dwTime;
            TimeSpan idleDuration = TimeSpan.FromMilliseconds(idleTime);

            return idleDuration >= TimeSpan.FromHours(appConfig.InactivityHours);
        }

        static bool IsInTimeWindow()
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            return currentTime >= appConfig.StartTime && currentTime <= appConfig.EndTime;
        }

        static void ShowReminder()
        {
            if (lastReminderDate.Date == DateTime.Now.Date)
            {
                return; // Do not show the reminder again today
            }

            if (reminderForm?.Visible != true)
            {
                reminderForm = new ReminderForm(LoadReminderMessage());
                reminderForm.ShowDialog();
                lastReminderDate = DateTime.Now; // Update the last reminder date to today
            }
        }
    }
}
