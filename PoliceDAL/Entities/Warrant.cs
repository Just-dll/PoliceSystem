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
    [Table("warrant")]
    public class Warrant : Decision
    {
        [Column("suspect_id")]
        public required int SuspectId { get; set; }

        [Column("case_file_id")]
        public required int CaseFileId { get; set; }
#pragma warning disable
        [ForeignKey("CaseFileId")]
        [InverseProperty("Warrants")]
        public virtual CaseFile CaseFile { get; set; }

        [ForeignKey("SuspectId")]
        [InverseProperty("WarrantsOn")]
        public virtual User Suspect { get; set; }
#pragma warning restore
    }
}
