using System;
using System.Windows;
using System.Windows.Threading;

namespace TaskManagerApp.Views
{
    public partial class SnackbarMessage : Window
    {
        public string Message
        {
            set { MsgText.Text = value; }
        }

        public SnackbarMessage()
        {
            InitializeComponent();
        }

        public void ShowSnack()
        {
            var area = SystemParameters.WorkArea;
            Left = area.Right - Width - 30;
            Top = area.Bottom - Height - 30;

            base.Show();

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                Close();
            };
            timer.Start();
        }
    }
}