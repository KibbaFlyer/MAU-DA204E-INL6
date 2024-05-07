using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDo.ViewModel;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for AboutPopup.xaml
    /// </summary>
    public partial class AboutPopup : Window
    {
        public AboutPopup(About viewModel, string icon)
        {
            this.Icon = new BitmapImage(new Uri(icon));
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
