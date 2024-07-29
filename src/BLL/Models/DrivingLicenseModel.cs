using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models;

public class DrivingLicenseModel : BaseModel
{
    public int DriverId { get; set; }
    public DateOnly IssueDate { get; internal set; }
    public DateOnly ExpirationDate { get; internal set; }
}
