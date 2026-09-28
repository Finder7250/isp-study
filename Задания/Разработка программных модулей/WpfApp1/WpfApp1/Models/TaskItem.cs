using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManagerApp.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _title = "";
        private string _status = "Новая";
        private string _priority = "Средний";
        private DateTime _dueDate = DateTime.Now.AddDays(7);
        private string _assignee = "";

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }

        public string Priority
        {
            get { return _priority; }
            set { _priority = value; OnPropertyChanged(); }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; OnPropertyChanged(); }
        }

        public string Assignee
        {
            get { return _assignee; }
            set { _assignee = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }
}