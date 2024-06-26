using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class NotificationMessage
    {
        public Guid Id { get; } = Guid.NewGuid();
        public int UserId { get; set; }
        public string Message { get; set; } = default!;
    }
}
