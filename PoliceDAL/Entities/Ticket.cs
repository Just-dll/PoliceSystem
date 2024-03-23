using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Models;

[Table("ticket")]
[Index("ReportId", Name = "IX_ticket_report_id")]
[Index("ViolatorId", Name = "IX_ticket_violator_id")]
public partial class Ticket
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("report_id")]
    public int ReportId { get; set; }

    [Column("violator_id")]
    public int ViolatorId { get; set; }

    [Column("fine")]
    public int? Fine { get; set; }

    [ForeignKey("ReportId")]
    [InverseProperty("Tickets")]
    public virtual Report Report { get; set; } = null!;

    [ForeignKey("ViolatorId")]
    [InverseProperty("Tickets")]
    public virtual User Violator { get; set; } = null!;
}
