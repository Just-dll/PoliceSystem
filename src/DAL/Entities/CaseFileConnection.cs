using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities;

[Table("case_file_assignation")]
public class CaseFileConnection : BaseEntity
{
    public int CaseFileId { get; set; }
    public int PersonId { get; set; }
    public int CaseFileConnectionTypeId { get; set; }

    [ForeignKey("CaseFileId")]
    [InverseProperty("CaseFileConnections")]
    public virtual CaseFile? CaseFile { get; set; }

    [ForeignKey("CaseFileConnectionTypeId")]
    [InverseProperty("CaseFileConnections")]
    public virtual CaseFileConnectionType? CaseFileConnectionType { get; set; }

    [ForeignKey("PersonId")]
    [InverseProperty("CaseFileConnections")]
    public virtual User? Person { get; set; }
}
