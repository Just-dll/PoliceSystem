using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models;

public class CaseFileConnectionModel : BaseModel
{
    public required UserSearchModel ConnectedPerson { get; set; }
    public string TypeOfConnection { get; set; } = CaseFileConnectionTypeEnum.Witness.ToString();
}
