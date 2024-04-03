using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo.Commands;
using ToDo.Model;

namespace ToDo.ViewModel
{
    public class TaskManager : ViewModelBase
    {
        private DateTime _currentDateTime = DateTime.Now;
        private ObservableCollection<ToDo.Model.Task> _tasks = new ObservableCollection<Model.Task>();
        private ToDo.Model.Task _selectedTask;
        private PriorityType? _currentPriority;
        private string _currentDescription = "";
        private string _windowName;
        private List<PriorityType> _priorityTypes;
        public TaskManager(string windowName)
        {

            _windowName = windowName;

            _priorityTypes = Enum.GetValues(typeof(PriorityType)).Cast<PriorityType>().ToList();

            PriorityListDisplayFriendly = new List<PriorityListDisplayFriendly>(
            _priorityTypes.Select(priorityEnum => new PriorityListDisplayFriendly
            {
                DisplayName = priorityEnum.ToString().Replace("_", " "),
                Value = priorityEnum
            }));

            AddCommand = new BaseICommand(AddCommandAction, CanAddCommand);
            ChangeCommand = new BaseICommand(ChangeCommandAction, CanChangeCommand);
            DeleteCommand = new BaseICommand(DeleteCommandAction, CanDeleteCommand);

            // We call our UpdateTime Task to start, which will never end but just update a label each second.
            _ = UpdateTime();
        }
        public List<PriorityListDisplayFriendly> PriorityListDisplayFriendly { get; set; }
        public ICommand AddCommand { get; }
        public ICommand ChangeCommand { get; }
        public ICommand DeleteCommand { get; }
        public string WindowName
        {
            get
            {
                return _windowName;
            }
        }
        public string Time
        {
            get
            {
                return DateTime.Now.ToString("HH:mm:ss"); ;
            }
        }
        public DateTime CurrentDateTime
        {
            get
            {
                return _currentDateTime;
            }
            set
            {
                if (value != _currentDateTime)
                {
                    _currentDateTime = value;
                    OnPropertyChanged(nameof(CurrentDateTime));
                }
            }
        }
        public ObservableCollection<ToDo.Model.Task> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                if (value != _tasks)
                {
                    _tasks = value;
                    OnPropertyChanged(nameof(Tasks));
                }
            }
        }
        public ToDo.Model.Task SelectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                if (value != _selectedTask)
                {
                    _selectedTask = value;
                    OnPropertyChanged(nameof(SelectedTask));
                }
            }
        }
        public PriorityType? CurrentPriority
        {
            get
            {
                return _currentPriority;
            }
            set
            {
                if (value != _currentPriority)
                {
                    _currentPriority = value;
                    OnPropertyChanged(nameof(CurrentPriority));
                }
            }
        }
        public string CurrentDescription
        {
            get
            {
                return _currentDescription;
            }
            set
            {
                if (value != _currentDescription)
                {
                    _currentDescription = value;
                    OnPropertyChanged(nameof(CurrentDescription));
                }
            }
        }
        private async System.Threading.Tasks.Task UpdateTime()
        {
            while (true)
            {
                await System.Threading.Tasks.Task.Delay(100);
                OnPropertyChanged(nameof(Time));
            }
        }

        public bool CanAddCommand()
        {
            return _currentDescription != null && _currentPriority != null;
        }
        public bool CanChangeCommand()
        {
            return _selectedTask != null;
        }
        public bool CanDeleteCommand()
        {
            return _selectedTask != null;
        }
        public void AddCommandAction()
        {
            _tasks.Add(new Model.Task(_currentDateTime, _currentDescription, (PriorityType)_currentPriority));
        }
        public void ChangeCommandAction()
        {

        }
        public void DeleteCommandAction()
        {
            _tasks.Remove(_selectedTask);
        }
    }
}
