using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models;

public class TicketModel : BaseModel
{
    public decimal Fine { get; set; }
    public DateOnly? IssueTime { get; set; }
    public string? Description { get; set; }
    public required int ViolatorId { internal get; set; }
    public string? ViolatorName { get; internal set; }
    public string? ViolatorSurname { get; internal set; }
    
}
