using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ToDo.ViewModel
{
    public class About: ViewModelBase
    {
        private string _title { get; set; }
        private string _description { get; set; }
        private string _uri { get; set; }

        public About(string title, string description, string picUri) 
        { 
            _title = title;
            _description = description;
            _uri = picUri;

        }
        public string Title
        {
            get
            {
                return _title;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
        }
        public string Uri
        {
            get
            {
                return _uri;
            }
        }
    }

}
