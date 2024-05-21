using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CaseFileModel : BaseModel
    {
        public string? ProsecutorName { get; set; }
        public string? ProsecutorSurname { get; set; }
        public required string Type { get; set; }
        public ICollection<ReportModel> Reports { get; internal set; } = [];
        public ICollection<WarrantModel> Warrants { get; internal set; } = [];
    }
}
