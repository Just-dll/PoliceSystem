using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Models;

[Table("report")]
[Index("IssuerId", Name = "IX_report_issuer_id")]
public partial class Report
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date_of_issuing", TypeName = "smalldatetime")]
    public DateTime DateOfIssuing { get; set; }

    [Column("report_file_location")]
    [StringLength(250)]
    [Unicode(false)]
    public string? ReportFileLocation { get; set; }

    [Column("issuer_id")]
    public int IssuerId { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty("Report")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
