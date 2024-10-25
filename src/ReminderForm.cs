using System.Windows.Forms;

namespace MorningReminderApp
{
    public partial class ReminderForm : Form
    {
        public ReminderForm(string reminderMessage)
        {
            InitializeComponent();
            this.Load += (sender, e) => ReminderForm_Load(reminderMessage);
            this.KeyDown += ReminderForm_KeyDown;
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

        private void ReminderForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Allow closing with the Escape key
            }
        }
    }
}