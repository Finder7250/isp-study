using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using TaskManagerApp.Models;

namespace TaskManagerApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<TaskItem> Tasks { get; private set; }

        public ICollectionView TasksView { get; private set; }

        private string _searchText = "";
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
                if (TasksView != null) TasksView.Refresh();
            }
        }

        private string _statusFilter = "Все";
        public string StatusFilter
        {
            get { return _statusFilter; }
            set
            {
                _statusFilter = value;
                OnPropertyChanged();
                if (TasksView != null) TasksView.Refresh();
            }
        }

        public string[] Statuses { get; private set; }

        // ---- Вычисляемые свойства для карточек ----
        public int CompletedCount
        {
            get
            {
                int count = 0;
                foreach (var t in Tasks)
                    if (t.Status == "Выполнено") count++;
                return count;
            }
        }

        public int OverdueCount
        {
            get
            {
                int count = 0;
                foreach (var t in Tasks)
                    if (t.Status == "Просрочено") count++;
                return count;
            }
        }

        public bool HasOverdue
        {
            get { return OverdueCount > 0; }
        }

        public MainViewModel()
        {
            Statuses = new[] { "Все", "Новая", "В работе", "Выполнено", "Просрочено" };

            Tasks = new ObservableCollection<TaskItem>();
            Tasks.Add(new TaskItem { Title = "Разработать макет", Status = "В работе", Priority = "Высокий", Assignee = "Иванов", DueDate = DateTime.Now.AddDays(3) });
            Tasks.Add(new TaskItem { Title = "Написать отчёт", Status = "Новая", Priority = "Средний", Assignee = "Петров", DueDate = DateTime.Now.AddDays(10) });
            Tasks.Add(new TaskItem { Title = "Тестирование", Status = "Просрочено", Priority = "Высокий", Assignee = "Сидоров", DueDate = DateTime.Now.AddDays(-2) });
            Tasks.Add(new TaskItem { Title = "Согласование ТЗ", Status = "Выполнено", Priority = "Низкий", Assignee = "Иванов", DueDate = DateTime.Now.AddDays(1) });

            // Обновление карточек при изменении коллекции
            Tasks.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(CompletedCount));
                OnPropertyChanged(nameof(OverdueCount));
                OnPropertyChanged(nameof(HasOverdue));
            };

            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.Filter = FilterTask;
        }

        private bool FilterTask(object obj)
        {
            var t = obj as TaskItem;
            if (t == null) return false;

            bool matchesSearch = string.IsNullOrWhiteSpace(SearchText)
                || (t.Title != null && t.Title.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0)
                || (t.Assignee != null && t.Assignee.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);

            bool matchesStatus = StatusFilter == "Все" || t.Status == StatusFilter;

            return matchesSearch && matchesStatus;
        }
    }
}