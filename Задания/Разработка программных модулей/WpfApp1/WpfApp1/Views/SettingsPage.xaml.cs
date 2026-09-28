using System.Windows;
using System.Windows.Controls;

namespace TaskManagerApp.Views
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
            LightRadio.IsChecked = !App.IsDarkTheme;
            DarkRadio.IsChecked = App.IsDarkTheme;
        }

        private void Theme_Changed(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded) return;
            App.ApplyTheme(DarkRadio.IsChecked == true);
        }
    }
}