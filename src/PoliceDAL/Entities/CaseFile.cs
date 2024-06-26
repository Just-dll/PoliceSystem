using AngularApp1.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDAL.Entities
{
    [Table("case_file")]
    public class CaseFile : BaseEntity
    {
        [Column("initation_date")]
        public DateOnly InitiationDate { get; set; }

        [Column("case_file_type_id")]
        public required int CaseFileTypeId { get; set; }

        [ForeignKey("CaseFileTypeId")]
        [InverseProperty("CaseFiles")]
        public CaseFileType? CaseFileType { get; set; }

        [InverseProperty("CaseFile")]
        public ICollection<Report> Reports { get; set; } = [];

        [InverseProperty("CaseFile")]
        public ICollection<Warrant> Warrants { get; set; } = [];

        [InverseProperty("CaseFile")]
        public ICollection<CaseFileConnection> CaseFileConnections { get; set; } = [];
        
    }
}
