using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class PersonTicketModel : BaseModel
    {
        public decimal Fine { get; set; }
        public DateOnly? IssueTime { get; set; }
        public string? Description { get; set; }
    }
}
