using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities;

[Table("case_file_connection_type")]
public class CaseFileConnectionType : BaseEntity
{
    [Column("value")]
    public required string Value { get; set; }

    [InverseProperty("CaseFileConnectionType")]
    public virtual ICollection<CaseFileConnection> CaseFileConnections { get; set; } = [];
}

public enum CaseFileConnectionTypeEnum
{
    Witness = 1,
    Suspect,
    Attourney, 
    Prosecutor,
    Judge
}

