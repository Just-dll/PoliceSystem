using BLL.Models;

namespace BLL.Interfaces;

public interface ICaseFileService : IService<CaseFileModel>
{
    Task<IEnumerable<CaseFilePreview>?> GetAssignedCaseFiles(int personId);
}
