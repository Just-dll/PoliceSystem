using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class BaseEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
}
