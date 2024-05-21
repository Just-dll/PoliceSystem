using AngularApp1.Server.Data;
using AutoFixture;
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
        [Fact]
        public void GetCaseFile_FullCaseFileRequest_CaseFile()
        {
            // Arrange
            var contextMock = new Mock<ICaseFileRepository>();
            var sut = new CaseFileRepository(new PolicedatabaseContext());
            var fixture = new Fixture();

            // Act

            // Assert
        }
    }

    public class CaseFileRepositoryMock : ICaseFileRepository
    {
        public Task AddAsync(CaseFile entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<CaseFile> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CaseFile>> FindAsync(Expression<Func<CaseFile, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CaseFile>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CaseFile?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Warrant>> GetIssuedWarrantsByCaseFile(int caseFileId)
        {
            throw new NotImplementedException();
        }

        public void Remove(CaseFile entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<CaseFile> entities)
        {
            throw new NotImplementedException();
        }

        public Task Update(CaseFile entity)
        {
            throw new NotImplementedException();
        }

    }
}
