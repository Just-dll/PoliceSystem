using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities;

[Table("case_file_type")]
public class CaseFileType : BaseEntity
{
    [Column("value")]
    public string? Value { get; set; }

    [InverseProperty("CaseFileType")]
    public ICollection<CaseFile> CaseFiles { get; set; } = new List<CaseFile>();
}

public enum CaseFileTypeEnum
{
    Civil = 1,
    Criminal
}
