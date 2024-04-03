using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    public class Task
    {
        private DateTime _dateTime;
        private string _description;
        private PriorityType _priority;
        public Task(DateTime dateTime, string description, PriorityType priorityType) 
        { 
            _dateTime = dateTime;
            _description = description;
            _priority = priorityType;
        }
        public string Date 
        {
            get { return _dateTime.ToString("D"); }
        }
        public string Hour
        {
            get { return _dateTime.ToString("HH:mm"); }
        }
        public string Priority
        {
            get { return _priority.ToString(); }
        }
        public string Description
        {
            get { return _description; }
        }
        public bool SetDate(DateTime date)
        {
            try
            {
                _dateTime = date;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetDescription(string description)
        {
            try
            {
                _description = description;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetPriority(PriorityType priority)
        {
            try
            {
                _priority = priority;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
