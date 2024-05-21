using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class WarrantModel : BaseModel
    {
        public string? SuspectName { get; set; } 
        public string? SuspectSurname { get; set; } 
        public string? Description { get; set; }
        public DateOnly IssueDate { get; set; }
    }
}
