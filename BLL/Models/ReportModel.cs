using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ReportModel : BaseModel
    {
        public string? Description { get; set; }
        public DateOnly DateOfReport { get; set; }
        public string? ReportedLocation { get; set; }
        public int ReporterId { get; set; }
    }
}
