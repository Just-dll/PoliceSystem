using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities;

[Table("warrant")]
public class Warrant : Decision
{
    [Column("suspect_id")]
    public required int SuspectId { get; set; }

    [Column("case_file_id")]
    public required int CaseFileId { get; set; }

    /*[Column("responded_request_id")]
    public int RespondedRequestId { get; set; }*/

    [ForeignKey("CaseFileId")]
    [InverseProperty("Warrants")]
    public virtual CaseFile? CaseFile { get; set; }

    [ForeignKey("SuspectId")]
    [InverseProperty("WarrantsOn")]
    public virtual User? Suspect { get; set; }

    /*[ForeignKey("RespondedRequestId")]
    [InverseProperty("WarrantRequest")]
    public virtual WarrantRequest? { get; set; }*/
}
