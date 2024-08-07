﻿using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTests
{
    public static class UserTestHelper
    {
        public static DbContextOptions<PolicedatabaseContext> GetDbContextOptions()
        {
            var contextOptions = new DbContextOptionsBuilder<PolicedatabaseContext>()
                .UseInMemoryDatabase("PoliceDatabase")
                .EnableSensitiveDataLogging()
                .Options;


            using var context = new PolicedatabaseContext(contextOptions);
            SeedData(context);

            return contextOptions;
        }
        public static List<CaseFileType> CaseFileTypes =>
        [
            //new CaseFileType { Id = 0, Value = "Bankrupcy" },
            new CaseFileType { Id = 1, Value = "Civil" },
            new CaseFileType { Id = 2, Value = "Criminal" }
        ];

        public static List<CaseFileConnectionType> CaseFileConnectionTypes =>
        [
            new CaseFileConnectionType { Id = 1, Value = "Prosecutor" },
            new CaseFileConnectionType { Id = 2, Value = "Suspect" },
            new CaseFileConnectionType { Id = 2, Value = "Judge" },
            new CaseFileConnectionType { Id = 2, Value = "Attourney" },
            new CaseFileConnectionType { Id = 2, Value = "Witness" },
        ];
        public static List<User> Users =>
        [
            //new User { Id = 0, IdentityId = 0 },
            new User { Id = 1, IdentityId = 1 },
            new User { Id = 2, IdentityId = 2 },
            new User { Id = 3, IdentityId = 3 },
            new User { Id = 4, IdentityId = 4 },
            new User { Id = 5, IdentityId = 5 },
            new User { Id = 6, IdentityId = 6 },
        ];
        public static List<CaseFile> CaseFiles =>
        [
           // new CaseFile { Id = 0, CaseFileTypeId = 1 },
            new CaseFile { Id = 1, InitiationDate = new DateOnly(2024, 1, 1), CaseFileTypeId = 1 },
            new CaseFile { Id = 2, InitiationDate = new DateOnly(2024, 2, 1), CaseFileTypeId = 2 },
            new CaseFile { Id = 3, InitiationDate = new DateOnly(2024, 3, 1), CaseFileTypeId = 1 },
            new CaseFile { Id = 4, InitiationDate = new DateOnly(2024, 4, 1), CaseFileTypeId = 2 },
            new CaseFile { Id = 5, InitiationDate = new DateOnly(2024, 5, 1), CaseFileTypeId = 1 },
            new CaseFile { Id = 6, InitiationDate = new DateOnly(2024, 6, 1), CaseFileTypeId = 2 }
        ];
        public static List<CaseFileConnection> CaseFileConnections =>
        [
            new CaseFileConnection { Id = 1, CaseFileId = 1, PersonId = 1, CaseFileConnectionTypeId = 1 },
            new CaseFileConnection { Id = 2, CaseFileId = 2, PersonId = 2, CaseFileConnectionTypeId = 1 },
            new CaseFileConnection { Id = 3, CaseFileId = 3, PersonId = 3, CaseFileConnectionTypeId = 1 },
            new CaseFileConnection { Id = 4, CaseFileId = 4, PersonId = 4, CaseFileConnectionTypeId = 1 },
            new CaseFileConnection { Id = 5, CaseFileId = 5, PersonId = 5, CaseFileConnectionTypeId = 1 },
        ];

        public static List<Report> Reports => 
        [
            //new Report { Id = 0, },
            new Report { Id = 1, DateOfIssuing = new DateOnly(2024, 1, 15), ReportFileLocation = "report1.pdf", IssuerId = 1, Description = "Report 1 description", CaseFileId = 1 },
            new Report { Id = 2, DateOfIssuing = new DateOnly(2024, 2, 15), ReportFileLocation = "report2.pdf", IssuerId = 2, Description = "Report 2 description", CaseFileId = 2 },
            new Report { Id = 3, DateOfIssuing = new DateOnly(2024, 3, 15), ReportFileLocation = "report3.pdf", IssuerId = 3, Description = "Report 3 description", CaseFileId = 3 },
            new Report { Id = 4, DateOfIssuing = new DateOnly(2024, 4, 15), ReportFileLocation = "report4.pdf", IssuerId = 4, Description = "Report 4 description", CaseFileId = 4 },
            new Report { Id = 5, DateOfIssuing = new DateOnly(2024, 5, 15), ReportFileLocation = "report5.pdf", IssuerId = 5, Description = "Report 5 description", CaseFileId = 5 },
            new Report { Id = 6, DateOfIssuing = new DateOnly(2024, 6, 15), ReportFileLocation = "report6.pdf", IssuerId = 6, Description = "Report 6 description", CaseFileId = 6 }
        ];
        public static List<Warrant> Warrants => 
        [
            //new Warrant { Id = 0, CaseFileId = 0, JudgeId = 0, SuspectId = 0 },
            new Warrant { Id = 1, JudgeId = 1, Description = "Warrant 1 description", IssueDate = new DateOnly(2024, 1, 20), SuspectId = 2, CaseFileId = 1 },
            new Warrant { Id = 2, JudgeId = 2, Description = "Warrant 2 description", IssueDate = new DateOnly(2024, 2, 20), SuspectId = 3, CaseFileId = 2 },
            new Warrant { Id = 3, JudgeId = 3, Description = "Warrant 3 description", IssueDate = new DateOnly(2024, 3, 20), SuspectId = 4, CaseFileId = 3 },
            new Warrant { Id = 4, JudgeId = 4, Description = "Warrant 4 description", IssueDate = new DateOnly(2024, 4, 20), SuspectId = 5, CaseFileId = 4 },
            new Warrant { Id = 5, JudgeId = 5, Description = "Warrant 5 description", IssueDate = new DateOnly(2024, 5, 20), SuspectId = 6, CaseFileId = 5 },
            new Warrant { Id = 6, JudgeId = 6, Description = "Warrant 6 description", IssueDate = new DateOnly(2024, 6, 20), SuspectId = 1, CaseFileId = 6 }
        ];
        public static List<Decision> Decisions => 
        [
            //new Decision { Id = 0, JudgeId = 0 },
            new Decision { Id = 1, JudgeId = 1, Description = "Decision 1 description", IssueDate = new DateOnly(2024, 1, 25) },
            new Decision { Id = 2, JudgeId = 2, Description = "Decision 2 description", IssueDate = new DateOnly(2024, 2, 25) },
            new Decision { Id = 3, JudgeId = 3, Description = "Decision 3 description", IssueDate = new DateOnly(2024, 3, 25) },
            new Decision { Id = 4, JudgeId = 4, Description = "Decision 4 description", IssueDate = new DateOnly(2024, 4, 25) },
            new Decision { Id = 5, JudgeId = 5, Description = "Decision 5 description", IssueDate = new DateOnly(2024, 5, 25) },
            new Decision { Id = 6, JudgeId = 6, Description = "Decision 6 description", IssueDate = new DateOnly(2024, 6, 25) }
        ];
        public static List<DrivingLicense> DrivingLicenses => 
        [
            //new DrivingLicense { Id = 0 }, 
            new DrivingLicense { Id = 1, DriverId = 1, IssueDate = new DateOnly(2023, 1, 1), ExpirationDate = new DateOnly(2025, 1, 1) },
            new DrivingLicense { Id = 2, DriverId = 2, IssueDate = new DateOnly(2023, 2, 1), ExpirationDate = new DateOnly(2025, 2, 1) },
            new DrivingLicense { Id = 3, DriverId = 3, IssueDate = new DateOnly(2023, 3, 1), ExpirationDate = new DateOnly(2025, 3, 1) },
            new DrivingLicense { Id = 4, DriverId = 4, IssueDate = new DateOnly(2023, 4, 1), ExpirationDate = new DateOnly(2025, 4, 1) },
            new DrivingLicense { Id = 5, DriverId = 5, IssueDate = new DateOnly(2023, 5, 1), ExpirationDate = new DateOnly(2025, 5, 1) },
            new DrivingLicense { Id = 6, DriverId = 6, IssueDate = new DateOnly(2023, 6, 1), ExpirationDate = new DateOnly(2025, 6, 1) }
        ];
        public static List<RegisteredCar> RegisteredCars => 
        [
            //new RegisteredCar { Id = 0 },
            new RegisteredCar { Id = 1, PlateNumber = "ABC123", Model = "Toyota", Color = "Red", Year = new DateOnly(2020, 1, 1), CarOwnerId = 1 },
            new RegisteredCar { Id = 2, PlateNumber = "DEF456", Model = "Honda", Color = "Blue", Year = new DateOnly(2019, 1, 1), CarOwnerId = 2 },
            new RegisteredCar { Id = 3, PlateNumber = "GHI789", Model = "Ford", Color = "Black", Year = new DateOnly(2018, 1, 1), CarOwnerId = 3 },
            new RegisteredCar { Id = 4, PlateNumber = "JKL012", Model = "BMW", Color = "White", Year = new DateOnly(2021, 1, 1), CarOwnerId = 4 },
            new RegisteredCar { Id = 5, PlateNumber = "MNO345", Model = "Audi", Color = "Gray", Year = new DateOnly(2022, 1, 1), CarOwnerId = 5 },
            new RegisteredCar { Id = 6, PlateNumber = "PQR678", Model = "Mercedes", Color = "Silver", Year = new DateOnly(2023, 1, 1), CarOwnerId = 6 }
        ];
        public static List<Ticket> Tickets => 
        [
            //new Ticket { Id = 0 }, 
            new Ticket { Id = 1, ReportId = 1, ViolatorId = 2, Fine = 100.0m },
            new Ticket { Id = 2, ReportId = 2, ViolatorId = 3, Fine = 150.0m },
            new Ticket { Id = 3, ReportId = 3, ViolatorId = 4, Fine = 200.0m },
            new Ticket { Id = 4, ReportId = 4, ViolatorId = 5, Fine = 250.0m },
            new Ticket { Id = 5, ReportId = 5, ViolatorId = 6, Fine = 300.0m },
            new Ticket { Id = 6, ReportId = 6, ViolatorId = 1, Fine = 350.0m }
        ];

        public static void SeedData(PolicedatabaseContext context)
        {
            context.Users.AddRange(Users);
            context.CaseFileTypes.AddRange(CaseFileTypes);
            context.CaseFiles.AddRange(CaseFiles);
            context.CaseFileConnections.AddRange(CaseFileConnections);
            context.Reports.AddRange(Reports);
            context.Tickets.AddRange(Tickets);
            context.Warrants.AddRange(Warrants);
            //context.Decisions.AddRange(Decisions);
            context.DrivingLicenses.AddRange(DrivingLicenses);
            context.RegisteredCars.AddRange(RegisteredCars);
            context.SaveChanges();
        }
    }
}
