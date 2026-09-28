using System.Windows;
using System.Windows.Controls;
using TaskManagerApp.Models;
using TaskManagerApp.ViewModels;

namespace TaskManagerApp.Views
{
    public partial class TasksPage : UserControl
    {
        public TasksPage()
        {
            InitializeComponent();
        }

        private MainViewModel VM
        {
            get { return DataContext as MainViewModel; }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TaskDialog(new TaskItem());
            dialog.Owner = Window.GetWindow(this);

            if (dialog.ShowDialog() == true && VM != null)
            {
                VM.Tasks.Add(dialog.Result);
                ShowSnack("Задача добавлена");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selected = TasksGrid.SelectedItem as TaskItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите задачу для редактирования.", "Внимание");
                return;
            }

            var copy = new TaskItem
            {
                Title = selected.Title,
                Assignee = selected.Assignee,
                Priority = selected.Priority,
                Status = selected.Status,
                DueDate = selected.DueDate
            };

            var dialog = new TaskDialog(copy);
            dialog.Owner = Window.GetWindow(this);

            if (dialog.ShowDialog() == true)
            {
                selected.Title = dialog.Result.Title;
                selected.Assignee = dialog.Result.Assignee;
                selected.Priority = dialog.Result.Priority;
                selected.Status = dialog.Result.Status;
                selected.DueDate = dialog.Result.DueDate;
                ShowSnack("Изменения сохранены");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selected = TasksGrid.SelectedItem as TaskItem;
            if (selected == null) return;

            var result = MessageBox.Show(
                "Удалить задачу «" + selected.Title + "»?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes && VM != null)
            {
                VM.Tasks.Remove(selected);
                ShowSnack("Задача удалена");
            }
        }

        private void ShowSnack(string message)
        {
            var snack = new SnackbarMessage();
            snack.Message = message;
            snack.ShowSnack();
        }
    }
}