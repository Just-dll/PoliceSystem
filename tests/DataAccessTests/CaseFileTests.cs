using AngularApp1.Server.Data;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using PoliceDAL.Entities;
using PoliceDAL.Interfaces;
using PoliceDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTests
{
    public class CaseFileTests
    {
        PolicedatabaseContext context;
        public CaseFileTests()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<PolicedatabaseContext>()
                .UseInMemoryDatabase("PoliceDatabase")
                .Options;

            context = new PolicedatabaseContext(contextOptionsBuilder);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            UserTestHelper.SeedData(context);
        }
        [Fact]
        public async void GetAssignedCaseFiles_GetMyCaseFilesRequest_ListOfAssignedCaseFiles()
        {
            // Assert
            var repository = new CaseFileRepository(context);
            // Act
            var casefiles = await repository.GetAssignedCaseFilesAsync(6);
            // Arrange
            Assert.NotNull(casefiles);
        }
    }
}
