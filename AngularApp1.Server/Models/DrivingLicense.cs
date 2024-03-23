using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Models;

[Table("driving_license")]
[Index("DriverId", Name = "UQ__driving___A411C5BCEA9DB9D2", IsUnique = true)]
public partial class DrivingLicense
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("driver_id")]
    public int DriverId { get; set; } = default!;

    [Column("issue_date")]
    public DateOnly IssueDate { get; set; }

    [Column("expiration_date")]
    public DateOnly ExpirationDate { get; set; }

    [ForeignKey("DriverId")]
    [InverseProperty("DrivingLicense")]
    public virtual User? Driver { get; set; }
}
