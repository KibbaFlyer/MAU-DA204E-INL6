using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    public enum PriorityType
    {
        Very_important,
        Important,
        Normal,
        Less_important,
        Not_important
    }
    public class PriorityListDisplayFriendly
    {
        public string DisplayName { get; set; }
        public PriorityType Value { get; set; }
    }
}
