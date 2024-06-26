using AngularApp1.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDAL.Entities
{
    [Table("case_file_assignation")]
    public class CaseFileConnection : BaseEntity
    {
        public int CaseFileId { get; set; }
        public int PersonId { get; set; }

        [ForeignKey("CaseFileId")]
        public virtual CaseFile? CaseFile { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("CaseFileConnections")]
        public virtual User? Person { get; set; }
    }
}
