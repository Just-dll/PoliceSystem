using PoliceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDAL.Interfaces
{
    public interface ICaseFileRepository : IRepository<CaseFile>
    {
        Task<IEnumerable<Warrant>> GetIssuedWarrantsByCaseFile(int caseFileId);
    }
}
