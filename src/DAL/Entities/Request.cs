using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities;

[Table("requests")]
public class Request : BaseEntity
{
    [Column("requester_id")]
    public required int RequesterId { get; set; }
    [Column("initiation_date")]
    public DateOnly SentDate { get; set; }
    [Column("status")]
    public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
    [Column("description")]
    public string? Description { get; set; }

    [ForeignKey("RequesterId")]
    [InverseProperty("Requests")]
    public virtual User? Requester { get; set; }
}

public enum RequestStatus
{
    Denied = -1,
    Pending,
    Approved,
}

//public enum RequestType
//{
//    None,
//    Warrant,
//    Driving, 
//    Exam,
//    Investigative,
//    OpenCaseFile
//}

