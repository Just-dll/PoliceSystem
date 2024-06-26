using System;
using System.Collections.Generic;
using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PoliceDAL.Entities;

namespace AngularApp1.Server.Data;

public partial class PolicedatabaseContext : DbContext
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

    public virtual DbSet<CaseFile> CaseFiles { get; set; }

    public virtual DbSet<CaseFileType> CaseFileTypes { get; set; }

    public virtual DbSet<Warrant> Warrants { get; set; }

    public virtual DbSet<Decision> Decisions { get; set; }

    public virtual DbSet<CaseFileConnection> CaseFileConnections { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User_Main_Id");
            entity.HasIndex("IdentityId")
                .IsUnique(true);
        });

        modelBuilder.Entity<DrivingLicense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__driving___3213E83F5F21BAB8");

            entity.HasOne(d => d.Driver).WithOne(p => p.DrivingLicense)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_driver_id");
        });

        modelBuilder.Entity<CaseFileConnection>(entity =>
        {
            entity.HasKey(cfc => cfc.Id);

            entity.HasOne(cfc => cfc.Person)
                .WithMany(p => p.CaseFileConnections)
                .IsRequired();

            entity.HasOne(cfc => cfc.CaseFile)
                .WithMany(cf => cf.CaseFileConnections)
                .IsRequired();
        });

        modelBuilder.Entity<CaseFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_case_file_3002032");

            entity.HasOne(c => c.CaseFileType).WithMany(ct => ct.CaseFiles)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_case_caseType_");
        });

        modelBuilder.Entity<CaseFileType>(entity =>
        {
            entity.HasKey(cft => cft.Id).HasName("PK_case_file_type");
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

            entity.HasOne(e => e.Issuer).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Issuer_Report_ref");

            entity.HasOne(r => r.CaseFile).WithMany(cf => cf.Reports)
                .OnDelete(DeleteBehavior.NoAction);
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

        modelBuilder.Entity<Warrant>(entity =>
        {
            entity.HasOne(e => e.Suspect).WithMany(s => s.WarrantsOn)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(e => e.CaseFile).WithMany(cf => cf.Warrants)
                .IsRequired()
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        });

        modelBuilder.Entity<Decision>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Judge).WithMany(j => j.Decisions)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PoliceProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        base.OnConfiguring(optionsBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
