using System;
using System.Collections.Generic;
using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Data;

public partial class PolicedatabaseContext : IdentityDbContext<User, Position, int>
{
    public PolicedatabaseContext()
    {
    }

    public PolicedatabaseContext(DbContextOptions<PolicedatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DrivingLicense> DrivingLicenses { get; set; }

    public virtual DbSet<RegisteredCar> RegisteredCars { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=policedatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<DrivingLicense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__driving___3213E83F5F21BAB8");

            entity.HasOne(d => d.Driver).WithOne(p => p.DrivingLicense)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_driver_id");
        });

        modelBuilder.Entity<RegisteredCar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__register__3213E83F2999EE23");

            entity.HasOne(d => d.CarOwner).WithMany(p => p.RegisteredCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_car_owner_ref");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__report__3213E83F90184F01");

            entity.Property(e => e.ReportFileLocation).IsFixedLength();
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ticket__3213E83FE0964E0E");

            entity.HasOne(d => d.Report).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_report_id");

            entity.HasOne(d => d.Violator).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_violator");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83F956FB020");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
