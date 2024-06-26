using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CaseFilePreview : BaseModel
    {
        public string Type { get; internal set; } = default!;
        public DateOnly InitiationDate { get; internal set; }
        public ICollection<UserSearchModel> Suspects { get; internal set; } = [];
    }
}
