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
    [Table("decision")]
    public class Decision
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("judge_id")]
        [Required]
        public required int JudgeId { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("issue_date")]
        public DateOnly IssueDate { get; set; }

        [ForeignKey("JudgeId")]
        [InverseProperty("Decisions")]
        public virtual User Judge { get; set; }
    }
}
