using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHolder.Models
{
    /// <summary>
    /// Категория события
    /// </summary>
    public class EventCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<Event> Events { get; set; }
    }
}
