namespace MorningReminderApp
{
    partial class ConfiguratorForm
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

        private System.Windows.Forms.DateTimePicker timePickerStart;
        private System.Windows.Forms.DateTimePicker timePickerEnd;
        private System.Windows.Forms.DateTimePicker timePickerEveningNotification;
        private System.Windows.Forms.NumericUpDown numericInactivityHours;
        private System.Windows.Forms.TextBox textReminderFile;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonEditReminderFile;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Label labelStartTime;
        private System.Windows.Forms.Label labelEndTime;
        private System.Windows.Forms.Label labelInactivityHours;
        private System.Windows.Forms.Label labelReminderFile;
        private System.Windows.Forms.Label labelEveningNotificationTime;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            this.timePickerStart = new System.Windows.Forms.DateTimePicker();
            this.timePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.timePickerEveningNotification = new System.Windows.Forms.DateTimePicker();
            this.numericInactivityHours = new System.Windows.Forms.NumericUpDown();
            this.textReminderFile = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonEditReminderFile = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.labelStartTime = new System.Windows.Forms.Label();
            this.labelEndTime = new System.Windows.Forms.Label();
            this.labelInactivityHours = new System.Windows.Forms.Label();
            this.labelReminderFile = new System.Windows.Forms.Label();
            this.labelEveningNotificationTime = new System.Windows.Forms.Label();

            // TimePicker settings
            this.timePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timePickerEveningNotification.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timePickerStart.ShowUpDown = true;
            this.timePickerEnd.ShowUpDown = true;
            this.timePickerEveningNotification.ShowUpDown = true;

            // Label text
            this.labelStartTime.Text = "Start Time:";
            this.labelEndTime.Text = "End Time:";
            this.labelInactivityHours.Text = "Inactivity Hours:";
            this.labelReminderFile.Text = "Reminder File:";
            this.labelEveningNotificationTime.Text = "Evening Notification Time:";

            // Adjust positioning to prevent overlap
            int labelWidth = 140;
            int controlLeftPosition = 180;

            this.labelStartTime.Location = new System.Drawing.Point(30, 30);
            this.labelStartTime.Size = new System.Drawing.Size(labelWidth, 20);
            this.timePickerStart.Location = new System.Drawing.Point(controlLeftPosition, 30);

            this.labelEndTime.Location = new System.Drawing.Point(30, 60);
            this.labelEndTime.Size = new System.Drawing.Size(labelWidth, 20);
            this.timePickerEnd.Location = new System.Drawing.Point(controlLeftPosition, 60);

            this.labelInactivityHours.Location = new System.Drawing.Point(30, 90);
            this.labelInactivityHours.Size = new System.Drawing.Size(labelWidth, 20);
            this.numericInactivityHours.Location = new System.Drawing.Point(controlLeftPosition, 90);

            this.labelReminderFile.Location = new System.Drawing.Point(30, 120);
            this.labelReminderFile.Size = new System.Drawing.Size(labelWidth, 20);
            this.textReminderFile.Location = new System.Drawing.Point(controlLeftPosition, 120);
            this.textReminderFile.Width = 200;

            this.labelEveningNotificationTime.Location = new System.Drawing.Point(30, 150);
            this.labelEveningNotificationTime.Size = new System.Drawing.Size(labelWidth, 20);
            this.timePickerEveningNotification.Location = new System.Drawing.Point(controlLeftPosition, 150);

            this.buttonSave.Location = new System.Drawing.Point(30, 210);
            this.buttonEditReminderFile.Location = new System.Drawing.Point(130, 210);
            this.buttonTest.Location = new System.Drawing.Point(230, 210);

            // Button text
            this.buttonSave.Text = "Save";
            this.buttonEditReminderFile.Text = "Edit Reminder File";
            this.buttonTest.Text = "Test";

            // Event handlers
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            this.buttonEditReminderFile.Click += new System.EventHandler(this.ButtonEditReminderFile_Click);
            this.buttonTest.Click += new System.EventHandler(this.ButtonTest_Click);

            // Add controls to form
            this.Controls.Add(this.labelStartTime);
            this.Controls.Add(this.timePickerStart);
            this.Controls.Add(this.labelEndTime);
            this.Controls.Add(this.timePickerEnd);
            this.Controls.Add(this.labelInactivityHours);
            this.Controls.Add(this.numericInactivityHours);
            this.Controls.Add(this.labelReminderFile);
            this.Controls.Add(this.textReminderFile);
            this.Controls.Add(this.labelEveningNotificationTime);
            this.Controls.Add(this.timePickerEveningNotification);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonEditReminderFile);
            this.Controls.Add(this.buttonTest);

            this.Text = "Morning Reminder App";
            this.ClientSize = new System.Drawing.Size(450, 270);
        }



        #endregion
    }
}