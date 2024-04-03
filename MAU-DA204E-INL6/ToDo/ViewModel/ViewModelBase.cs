using System.ComponentModel;

namespace ToDo.ViewModel
{
    /// <summary>
    /// A base ViewModel class to implement OnPropertyChanged
    /// This is to allow for reactiveness of the ViewModels
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
