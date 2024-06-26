using AngularApp1.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDAL.Entities
{
    [Table("requests")]
    public class Request : BaseEntity
    {
        [Column("requester_id")]
        public required int RequesterId { get; set; }
        [Column("initiation_date")]
        public DateOnly InitiationDate { get; set; }
        [Column("status")]
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
        [Column("description")]
        public required string Description { get; set; }

        [ForeignKey("RequesterId")]
        [InverseProperty("Requests")]
        public virtual User? Requester { get; set; }
    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Denied
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
}
