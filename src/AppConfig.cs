using System;

namespace MorningReminderApp
{
    public class AppConfig
    {
        public string ReminderMessage { get; set; } = "Good Morning! Don't forget your morning regimen!";
        public string ReminderFile { get; set; } = "reminder.txt";
        public TimeSpan StartTime { get; set; } = TimeSpan.FromHours(5);
        public TimeSpan EndTime { get; set; } = TimeSpan.FromHours(10);
        public int InactivityHours { get; set; } = 3;
        public TimeSpan EveningNotificationTime { get; set; } = TimeSpan.FromHours(21); // Default to 9 PM
        public bool IsMaximized { get; set; } = false;
        public int WindowWidth { get; set; } = 800; // Default width
        public int WindowHeight { get; set; } = 600; // Default height
        public int WindowLeft { get; set; } = 100; // Default left position
        public int WindowTop { get; set; } = 100; // Default top position
    }


}
