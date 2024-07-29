using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

[Table("report")]
[Index("IssuerId", Name = "IX_report_issuer_id")]
public partial class Report : BaseEntity
{
    [Column("date_of_issuing")]
    public DateOnly DateOfIssuing { get; set; }

    [Column("report_file_location")]
    [StringLength(250)]
    [Unicode(false)]
    public string? ReportFileLocation { get; set; }

    [Column("issuer_id")]
    public int IssuerId { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("case_file_id")]
    public int? CaseFileId { get; set; } 

    [ForeignKey("IssuerId")]
    [InverseProperty("Reports")]
    public virtual User Issuer { get; set; } = null!;

    [InverseProperty("Report")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("CaseFileId")]
    [InverseProperty("Reports")] 
    public virtual CaseFile CaseFile { get; set; } = null!;
}