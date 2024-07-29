using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

[Table("ticket")]
[Index("ReportId", Name = "IX_ticket_report_id")]
[Index("ViolatorId", Name = "IX_ticket_violator_id")]
public partial class Ticket : BaseEntity
{
    [Required]
    [Column("report_id")]
    public int ReportId { get; set; }

    [Required]
    [Column("violator_id")]
    public int ViolatorId { get; set; } 

    [Column("fine")]
    public decimal Fine { get; set; }

    [ForeignKey("ReportId")]
    [InverseProperty("Tickets")]
    public virtual Report? Report { get; set; }

    [ForeignKey("ViolatorId")]
    [InverseProperty("Tickets")]
    public virtual User? Violator { get; set; }
}
