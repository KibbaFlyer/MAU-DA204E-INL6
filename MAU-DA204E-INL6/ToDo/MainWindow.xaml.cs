using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDo.Model;
using ToDo.ViewModel;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// THe application is mostly using a MVVM workflow, except here.
    /// I have chosen to connect the View a bit more directly with the ViewModel. 
    /// Because of the ease of doing this, I do not see a reason at this time to disconnect it more.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new TaskManager("ToDo Reminder by Kristoffer Flygare");
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/icons8-erinnerung-16.png"));
            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TaskManager("ToDo Reminder by Kristoffer Flygare");
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ToDo.Model.Task> data = FileManager.OpenFile();
            DataContext = new TaskManager("ToDo Reminder by Kristoffer Flygare", data);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var vm = (TaskManager)this.DataContext;
            FileManager.SaveFile(vm.Tasks);
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to end the program?", "Think twice", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyDescriptionAttribute descriptionAttribute = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            string assemblyDescription = descriptionAttribute != null ? descriptionAttribute.Description : "No Description Found";
            About viewModel = new About("Assembly Description", assemblyDescription, "pack://application:,,,/Resources/thumbsup.jpg");
            AboutPopup view = new AboutPopup(viewModel, "pack://application:,,,/Resources/icons8-erinnerung-16.png");
            view.Show();
        }
    }
}