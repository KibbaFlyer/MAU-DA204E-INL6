using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ToDo.Model
{
    /// <summary>
    /// A Task, used in a ObservableCollection for the main purpose of the ToDo application
    /// </summary>
    public class Task
    {
        private DateTime _dateTime;
        private string _description;
        private PriorityType? _priority;
        // JSON Properties are here used in order to be able to serialize and deserialize it into a JSON file
        // The DateAsString and HourAsString is used in order to make the datetime more friendly for the user
        [JsonPropertyName("DateAsString")]
        public string DateAsString 
        {
            get { return _dateTime.ToString("D"); }
        }
        [JsonPropertyName("HourAsString")]
        public string HourAsString
        {
            get { return _dateTime.ToString("HH:mm"); }
        }
        [JsonPropertyName("Date")]
        public DateTime Date
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }
        [JsonPropertyName("Priority")]
        public PriorityType? Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        [JsonPropertyName("Description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
