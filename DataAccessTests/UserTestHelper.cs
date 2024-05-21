using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using PoliceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTests
{
    internal class UserTestHelper
    {
        private void SeedData(PolicedatabaseContext context)
        {
            context.Users.AddRange(
                new User { Id = 1, UserName = "john_doe", NormalizedUserName = "JOHN_DOE", Email = "john@example.com", NormalizedEmail = "JOHN@EXAMPLE.COM", PasswordHash = "hashedpassword" },
                new User { Id = 2, UserName = "jane_doe", NormalizedUserName = "JANE_DOE", Email = "jane@example.com", NormalizedEmail = "JANE@EXAMPLE.COM", PasswordHash = "hashedpassword" },
                new User { Id = 3, UserName = "mike_smith", NormalizedUserName = "MIKE_SMITH", Email = "mike@example.com", NormalizedEmail = "MIKE@EXAMPLE.COM", PasswordHash = "hashedpassword" },
                new User { Id = 4, UserName = "lisa_jones", NormalizedUserName = "LISA_JONES", Email = "lisa@example.com", NormalizedEmail = "LISA@EXAMPLE.COM", PasswordHash = "hashedpassword" }
            );

            context.DrivingLicenses.AddRange(
                new DrivingLicense { Id = 1, DriverId = 1, IssueDate = new DateOnly(2020, 1, 1), ExpirationDate = new DateOnly(2030, 1, 1) },
                new DrivingLicense { Id = 2, DriverId = 2, IssueDate = new DateOnly(2021, 5, 15), ExpirationDate = new DateOnly(2031, 5, 15) }
            );

            context.CaseFileTypes.AddRange(
                new CaseFileType { Id = 1, Value = "Criminal" },
                new CaseFileType { Id = 2, Value = "Civil" }
            );

            context.CaseFiles.AddRange(
                new CaseFile { Id = 1, ProsecutorId = 3, CaseFileTypeId = 1 },
                new CaseFile { Id = 2, ProsecutorId = 3, CaseFileTypeId = 2 }
            );

            context.Reports.AddRange(
                new Report { Id = 1, IssuerId = 2, DateOfIssuing = DateOnly.FromDateTime(new DateTime(2023, 1, 1)), Description = "Test Report 1", CaseFileId = 1 },
                new Report { Id = 2, IssuerId = 2, DateOfIssuing = DateOnly.FromDateTime(new DateTime(2023, 2, 1)), Description = "Test Report 2", CaseFileId = 1 },
                new Report { Id = 3, IssuerId = 4, DateOfIssuing = DateOnly.FromDateTime(new DateTime(2023, 3, 1)), Description = "Test Report 3", CaseFileId = 2 }
            );

            context.Tickets.AddRange(
                new Ticket { Id = 1, ReportId = 1, ViolatorId = 1, Fine = 100.00m },
                new Ticket { Id = 2, ReportId = 2, ViolatorId = 2, Fine = 200.00m },
                new Ticket { Id = 3, ReportId = 3, ViolatorId = 1, Fine = 150.00m }
            );

            context.Decisions.AddRange(
                new Decision { Id = 1, JudgeId = 4, Description = "Guilty on all counts" },
                new Decision { Id = 2, JudgeId = 4, Description = "Not guilty due to lack of evidence" }
            );

            context.SaveChanges();
        }

    }
}
