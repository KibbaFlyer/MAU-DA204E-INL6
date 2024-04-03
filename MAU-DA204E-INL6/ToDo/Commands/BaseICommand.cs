using System.Windows.Input;

namespace ToDo.Commands
{
    /// <summary>
    /// An extension of ICommand, to create a RelayCommand to be used in ViewModels
    /// </summary>
    public class BaseICommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public BaseICommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute  = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
