using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    /// <summary>
    /// An Enumerator of PriorityTypes
    /// </summary>
    public enum PriorityType
    {
        Very_important,
        Important,
        Normal,
        Less_important,
        Not_important
    }
    /// <summary>
    /// Makes the enumerator readable and displayfriendly, to use in dropdowns
    /// </summary>
    public class PriorityListDisplayFriendly
    {
        public string DisplayName { get; set; }
        public PriorityType Value { get; set; }
    }
}
