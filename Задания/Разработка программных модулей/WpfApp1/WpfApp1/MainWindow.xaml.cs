using System.Windows;
using TaskManagerApp.ViewModels;
using TaskManagerApp.Views;

namespace TaskManagerApp
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
            MainContent.Content = new DashboardPage { DataContext = ViewModel };
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DashboardPage { DataContext = ViewModel };
        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TasksPage { DataContext = ViewModel };
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new SettingsPage();
        }
    }
}