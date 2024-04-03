using System.ComponentModel;

namespace ToDo.Model
{
    /// <summary>
    /// Implements INotifyPropertyChanged for Models, such that nested properties can trigger changes for the parent
    /// </summary>
    internal class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
