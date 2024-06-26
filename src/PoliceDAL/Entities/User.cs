using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoliceDAL.Entities;

namespace AngularApp1.Server.Models;

[Table("user")]
public partial class User : BaseEntity
{
    [Column("IdentityId")]
    public int IdentityId { get; set; }

    [Column("user_name")]
    public string UserName { get; set; } = string.Empty;

    [InverseProperty("Driver")]
    public virtual DrivingLicense? DrivingLicense { get; set; }

    [InverseProperty("Issuer")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    [InverseProperty("CarOwner")]
    public virtual ICollection<RegisteredCar> RegisteredCars { get; set; } = new List<RegisteredCar>();

    [InverseProperty("Violator")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [InverseProperty("Person")]
    public virtual ICollection<CaseFileConnection> CaseFileConnections { get; set; } = new List<CaseFileConnection>();

    [InverseProperty("Suspect")]
    public virtual ICollection<Warrant> WarrantsOn { get; set; } = new List<Warrant>();

    [InverseProperty("Judge")]
    public virtual ICollection<Decision> Decisions { get; set; } = new List<Decision>();

    [InverseProperty("Requester")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
