using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

[Table("registered_car")]
[Index("CarOwnerId", Name = "IX_registered_car_car_owner_id")]
[Index("PlateNumber", Name = "UQ__register__87EF9F5903CD33E6", IsUnique = true)]
public partial class RegisteredCar : BaseEntity
{
    [Column("plate_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string PlateNumber { get; set; } = null!;

    [Column("model")]
    [StringLength(50)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    [Column("color")]
    [StringLength(50)]
    [Unicode(false)]
    public string Color { get; set; } = null!;

    [Column("year")]
    public DateOnly Year { get; set; }

    [Column("car_owner_id")]
    public int CarOwnerId { get; set; }

    [ForeignKey("CarOwnerId")]
    [InverseProperty("RegisteredCars")]
    public virtual User CarOwner { get; set; } = null!;
}
