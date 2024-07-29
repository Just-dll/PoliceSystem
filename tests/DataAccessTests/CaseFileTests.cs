using AutoFixture;
using DAL.Data;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataAccessTests
{
    public class CaseFileTests
    {
        [Theory]
        [InlineData(5)]
        public async Task GetAssignedCaseFiles_GetMyCaseFilesRequest_ListOfAssignedCaseFiles(int personId)
        {
            // Arrange
            using var context = new PolicedatabaseContext(UserTestHelper.GetDbContextOptions());
            var repository = new CaseFileRepository(context);

            // Act
            var casefiles = await repository.GetAssignedCaseFilesAsync(personId);

            // Assert
            Assert.Equal(casefiles, UserTestHelper.CaseFiles.Where(x => x.CaseFileConnections.Any(x => x.PersonId == personId)), new CaseFileComparer());
        }

        [Theory]
        [InlineData(2)]
        public async Task GetCaseFileConnections_GetCaseFileConnections_LitsOfConnections(int caseFileId)
        {
            using var context = new PolicedatabaseContext(UserTestHelper.GetDbContextOptions());
            var repository = new CaseFileRepository(context);

        }

        [Fact]
        public async Task GetCaseFiles_GetCaseFilesRequest_ListOfCaseFiles()
        {
            using var context = new PolicedatabaseContext(UserTestHelper.GetDbContextOptions());
            var repository = new CaseFileRepository(context);

            var casefiles = await repository.GetAllAsync();

            Assert.Equal(casefiles, UserTestHelper.CaseFiles, new CaseFileComparer());
        }

        [Theory]
        [InlineData(5)]
        public async Task GetCaseFile_GetCaseFileRequest_CaseFile(int caseFileId)
        {
            using var context = new PolicedatabaseContext(UserTestHelper.GetDbContextOptions());
            var repository = new CaseFileRepository(context);

            var casefile = await repository.GetByIdAsync(caseFileId);

            Assert.Equal(casefile, UserTestHelper.CaseFiles[caseFileId - 1], new CaseFileComparer());
        }
    }
}