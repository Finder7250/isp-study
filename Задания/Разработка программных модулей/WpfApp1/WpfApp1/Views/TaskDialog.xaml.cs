using System;
using System.Windows;
using System.Windows.Controls;
using TaskManagerApp.Models;

namespace TaskManagerApp.Views
{
    public partial class TaskDialog : Window
    {
        public TaskItem Result { get; private set; }

        public TaskDialog(TaskItem item)
        {
            InitializeComponent();
            Result = item;

            TitleBox.Text = item.Title;
            AssigneeBox.Text = item.Assignee;
            DateBox.SelectedDate = item.DueDate;
            SelectCombo(PriorityBox, item.Priority);
            SelectCombo(StatusBox, item.Status);
        }

        private static void SelectCombo(ComboBox box, string value)
        {
            foreach (var obj in box.Items)
            {
                var it = obj as ComboBoxItem;
                if (it != null && (string)it.Content == value)
                {
                    box.SelectedItem = it;
                    return;
                }
            }
            box.SelectedIndex = 0;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                TitleError.Visibility = Visibility.Visible;
                return;
            }
            TitleError.Visibility = Visibility.Collapsed;

            Result.Title = TitleBox.Text.Trim();
            Result.Assignee = AssigneeBox.Text.Trim();
            Result.Priority = (PriorityBox.SelectedItem as ComboBoxItem) != null
                ? (string)(PriorityBox.SelectedItem as ComboBoxItem).Content
                : "Средний";
            Result.Status = (StatusBox.SelectedItem as ComboBoxItem) != null
                ? (string)(StatusBox.SelectedItem as ComboBoxItem).Content
                : "Новая";
            Result.DueDate = DateBox.SelectedDate ?? DateTime.Now;

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}