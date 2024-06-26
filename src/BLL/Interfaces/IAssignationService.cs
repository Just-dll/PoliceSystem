using AngularApp1.Server.Models;

namespace BLL.Interfaces
{
    public interface IAssignationService
    {
        Task<int> Assign(int caseFileId, JudiciaryPosition position);
        Task Assign(int caseFileId, int personId);
    }
}