using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public decimal Fine { get; set; }
        public DateTime? IssueTime { get; set; }
        public string? Description { get; set; }
        public int ViolatorId { get; set; }
        public string? ViolatorName { get; set; }
        public string? ViolatorSurname { get; set; }
        
    }
}
