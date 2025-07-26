using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHolder.Models
{
    public class User
    {
        public int Id { get; set; }
        public string TelegramId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        HashSet<UserRequest> Requests { get; set; }
        HashSet<Ticket> Tickets { get; set; }
        public Role UserRole { get; set; }
    }
}
