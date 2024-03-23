using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Models;

[Table("user")]
public partial class User : IdentityUser<int>
{
    [Key]
    [Column("id")]
    public override int Id { get; set; }

    [InverseProperty("Driver")]
    public virtual DrivingLicense? DrivingLicense { get; set; }

    [InverseProperty("CarOwner")]
    public virtual ICollection<RegisteredCar> RegisteredCars { get; set; } = new List<RegisteredCar>();

    [InverseProperty("Violator")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
