using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDo.Commands;
using ToDo.Model;

namespace ToDo.ViewModel
{
    /// <summary>
    /// The TaskManager is the main ViewModel of the application.
    /// It collects lists of Tasks to be displayed by the view.
    /// </summary>
    public class TaskManager : ViewModelBase
    {
        private DateTime _currentDateTime = DateTime.Now;
        private ObservableCollection<ToDo.Model.Task> _tasks;
        private ToDo.Model.Task? _selectedTask;
        private PriorityType? _currentPriority;
        private string _currentDescription = "";
        private string _windowName;
        private List<PriorityType> _priorityTypes;
        /// <summary>
        /// TaskManager can be initialized with a list of Tasks, or not
        /// </summary>
        /// <param name="windowName">Name of the MainWindow</param>
        /// <param name="preloadedTasks">Initialization happens with a list of Tasks when a file is loaded</param>
        public TaskManager(string windowName, ObservableCollection<ToDo.Model.Task>? preloadedTasks = null)
        {
            _windowName = windowName;

            if(preloadedTasks != null)
            {
                _tasks = preloadedTasks;
            }
            else
            {
                _tasks = new ObservableCollection<ToDo.Model.Task>();
            }

            _priorityTypes = Enum.GetValues(typeof(PriorityType)).Cast<PriorityType>().ToList();
            // Here we create a list of more display-friendly enumerator values
            PriorityListDisplayFriendly = new List<PriorityListDisplayFriendly>(
            _priorityTypes.Select(priorityEnum => new PriorityListDisplayFriendly
            {
                DisplayName = priorityEnum.ToString().Replace("_", " "),
                Value = priorityEnum
            }));
            // We add some commands to interact between view and viewmodel
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
        public string DateTimeTooltip
        {
            get
            {
                return "Click on the arrow to the right to open calender view";
            }
        }
        public string ChangeTooltip
        {
            get
            {
                return "Choose an item in the list, and enter the new values you wish for it to have, then press change";
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
                // Here there is no need for a validator, since it is a DateTimePicker that we are using, the user cannot enter a faulty value
                // But if it was a textbox I would have added something in the likes of:
                // && DateTime.TryParseExact(value, "yyyy-MM-dd hh:mm:ss",CultureInfo.InvariantCulture,DateTimeStyles.None, out valueDT)
                // In the below if statement
                if (value != _currentDateTime)
                {
                    _currentDateTime = value;
                    OnPropertyChanged(nameof(CurrentDateTime));
                }
            }
        }

        public ToDo.Model.Task? SelectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                if (value != _selectedTask && value != null)
                {
                    _selectedTask = value;
                    CurrentPriority = value.Priority;
                    CurrentDescription = value.Description;
                    CurrentDateTime = value.Date;
                    OnPropertyChanged(nameof(SelectedTask));
                }
                if(value == null) 
                {
                    _selectedTask = null;
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
        /// <summary>
        /// Adds the current task details as a new task in the ObservableCollection
        /// </summary>
        public void AddCommandAction()
        {
            ToDo.Model.Task newTask = new ToDo.Model.Task();
            newTask.Date = _currentDateTime;
            newTask.Priority = (PriorityType)_currentPriority;
            newTask.Description = _currentDescription;
            _tasks.Add(newTask);
        }
        /// <summary>
        /// Changes the selectedTask with the current values enteres
        /// </summary>
        public void ChangeCommandAction()
        {
            if (_selectedTask != null)
            {
                _tasks.Remove(_selectedTask);
                ToDo.Model.Task newTask = new ToDo.Model.Task();
                newTask.Date = _currentDateTime;
                newTask.Priority = (PriorityType)_currentPriority;
                newTask.Description = _currentDescription;
                _tasks.Add(newTask);
                SelectedTask = _tasks.Last();
            }
        }
        /// <summary>
        /// Removes the selectedTask, asks the user before if they are sure
        /// </summary>
        public void DeleteCommandAction()
        {
            if (_selectedTask != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete the item?", "Think twice", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _tasks.Remove(_selectedTask);
                    SelectedTask = null;
                }
            }
        }
    }
}
