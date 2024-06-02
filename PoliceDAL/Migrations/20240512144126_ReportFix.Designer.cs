﻿// <auto-generated />
using System;
using AngularApp1.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AngularApp1.Server.Migrations
{
    [DbContext(typeof(PolicedatabaseContext))]
    [Migration("20240512144126_ReportFix")]
    partial class ReportFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AngularApp1.Server.Models.DrivingLicense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DriverId")
                        .HasColumnType("int")
                        .HasColumnName("driver_id");

                    b.Property<DateOnly>("ExpirationDate")
                        .HasColumnType("date")
                        .HasColumnName("expiration_date");

                    b.Property<DateOnly>("IssueDate")
                        .HasColumnType("date")
                        .HasColumnName("issue_date");

                    b.HasKey("Id")
                        .HasName("PK__driving___3213E83F5F21BAB8");

                    b.HasIndex(new[] { "DriverId" }, "UQ__driving___A411C5BCEA9DB9D2")
                        .IsUnique();

                    b.ToTable("driving_license");
                });

            modelBuilder.Entity("AngularApp1.Server.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("AngularApp1.Server.Models.RegisteredCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarOwnerId")
                        .HasColumnType("int")
                        .HasColumnName("car_owner_id");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("color");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("model");

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("plate_number");

                    b.Property<DateOnly>("Year")
                        .HasColumnType("date")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("PK__register__3213E83F2999EE23");

                    b.HasIndex(new[] { "CarOwnerId" }, "IX_registered_car_car_owner_id");

                    b.HasIndex(new[] { "PlateNumber" }, "UQ__register__87EF9F5903CD33E6")
                        .IsUnique();

                    b.ToTable("registered_car");
                });

            modelBuilder.Entity("AngularApp1.Server.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfIssuing")
                        .HasColumnType("smalldatetime")
                        .HasColumnName("date_of_issuing");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("IssuerId")
                        .HasColumnType("int")
                        .HasColumnName("issuer_id");

                    b.Property<string>("ReportFileLocation")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("char(250)")
                        .HasColumnName("report_file_location")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PK__report__3213E83F90184F01");

                    b.HasIndex(new[] { "IssuerId" }, "IX_report_issuer_id");

                    b.ToTable("report");
                });

            modelBuilder.Entity("AngularApp1.Server.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Fine")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("fine");

                    b.Property<int>("ReportId")
                        .HasColumnType("int")
                        .HasColumnName("report_id");

                    b.Property<int>("ViolatorId")
                        .HasColumnType("int")
                        .HasColumnName("violator_id");

                    b.HasKey("Id")
                        .HasName("PK__ticket__3213E83FE0964E0E");

                    b.HasIndex(new[] { "ReportId" }, "IX_ticket_report_id");

                    b.HasIndex(new[] { "ViolatorId" }, "IX_ticket_violator_id");

                    b.ToTable("ticket");
                });

            modelBuilder.Entity("AngularApp1.Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id")
                        .HasName("PK__user__3213E83F956FB020");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AngularApp1.Server.Models.DrivingLicense", b =>
                {
                    b.HasOne("AngularApp1.Server.Models.User", "Driver")
                        .WithOne("DrivingLicense")
                        .HasForeignKey("AngularApp1.Server.Models.DrivingLicense", "DriverId")
                        .IsRequired()
                        .HasConstraintName("FK_driver_id");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("AngularApp1.Server.Models.RegisteredCar", b =>
                {
                    b.HasOne("AngularApp1.Server.Models.User", "CarOwner")
                        .WithMany("RegisteredCars")
                        .HasForeignKey("CarOwnerId")
                        .IsRequired()
                        .HasConstraintName("FK_car_owner_ref");

                    b.Navigation("CarOwner");
                });

            modelBuilder.Entity("AngularApp1.Server.Models.Report", b =>
                {
                    b.HasOne("AngularApp1.Server.Models.User", "Issuer")
                        .WithMany("Reports")
                        .HasForeignKey("IssuerId")
                        .IsRequired()
                        .HasConstraintName("FK_Issuer_Report_ref");

                    b.Navigation("Issuer");
                });

            modelBuilder.Entity("AngularApp1.Server.Models.Ticket", b =>
                {
                    b.HasOne("AngularApp1.Server.Models.Report", "Report")
                        .WithMany("Tickets")
                        .HasForeignKey("ReportId")
                        .IsRequired()
                        .HasConstraintName("FK_ticket_report_id");

                    b.HasOne("AngularApp1.Server.Models.User", "Violator")
                        .WithMany("Tickets")
                        .HasForeignKey("ViolatorId")
                        .IsRequired()
                        .HasConstraintName("FK_ticket_violator");

                    b.Navigation("Report");

                    b.Navigation("Violator");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("AngularApp1.Server.Models.Position", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("AngularApp1.Server.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("AngularApp1.Server.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("AngularApp1.Server.Models.Position", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AngularApp1.Server.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("AngularApp1.Server.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AngularApp1.Server.Models.Report", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("AngularApp1.Server.Models.User", b =>
                {
                    b.Navigation("DrivingLicense");

                    b.Navigation("RegisteredCars");

                    b.Navigation("Reports");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}