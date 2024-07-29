using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces;

public interface IAssignationService
{
    Task<CaseFileConnectionModel> Assign(int caseFileId, JudiciaryPosition position);
    Task<CaseFileConnectionModel> Assign(int caseFileId, int personId, int connectionTypeId = 1);
}