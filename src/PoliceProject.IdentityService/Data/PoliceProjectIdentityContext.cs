using System;
using System.Collections.Generic;
using IdentityService.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data;

public class PoliceProjectIdentityContext : IdentityDbContext<User, Position, int>
{
    public PoliceProjectIdentityContext() 
        : base()
    {
    }

    public PoliceProjectIdentityContext(DbContextOptions<PoliceProjectIdentityContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PoliceProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    //    base.OnConfiguring(optionsBuilder);
    //}
}
