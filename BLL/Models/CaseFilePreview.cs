using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CaseFilePreview : BaseModel
    {
        public string? Type { get; internal set; }
        public string? ViolatorName { get; internal set; }
    }
}
