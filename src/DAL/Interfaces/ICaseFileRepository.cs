using DAL.Entities;

namespace DAL.Interfaces;

public interface ICaseFileRepository : IRepository<CaseFile>
{
    Task<IEnumerable<Warrant>> GetIssuedWarrantsByCaseFile(int caseFileId);
    Task<IEnumerable<CaseFile>> GetAssignedCaseFilesAsync(int personId);
}
