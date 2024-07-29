using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models;

public class UserModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Position { get; set; }
    public ICollection<PersonTicketModel> Tickets { get; set; } = [];
    public ICollection<ReportModel> Reports { get; set; } = [];
    public ICollection<WarrantModel> Warrants { get; set; } = [];

}
