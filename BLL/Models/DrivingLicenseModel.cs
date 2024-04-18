using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DrivingLicenseModel
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public DateOnly IssueDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
    }
}
