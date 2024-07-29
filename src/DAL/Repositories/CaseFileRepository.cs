using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories;

public class CaseFileRepository : Repository<CaseFile>, ICaseFileRepository
{
    public CaseFileRepository(PolicedatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Warrant>> GetIssuedWarrantsByCaseFile(int caseFileId)
    {
        var caseFile = await GetCaseFiles()
            .Where(cf => cf.Id == caseFileId).FirstOrDefaultAsync();

        if (caseFile == null)
        {
            return [];
        }

        return caseFile.Warrants;

    }

    public async override Task<CaseFile?> GetByIdAsync(int id)
    {
        return await GetCaseFiles()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<CaseFile>> GetAssignedCaseFilesAsync(int personId)
    {
        return await GetCaseFiles()
            .Where(cf => cf.CaseFileConnections.Any(cfc => cfc.PersonId == personId)).ToListAsync();
    }

    private IQueryable<CaseFile> GetCaseFiles()
    {
        return _dbSet.Include(cf => cf.Warrants)
            .Include(e => e.Reports)
            .Include(e => e.CaseFileConnections)
                .ThenInclude(e => e.Person)
            .Include(e => e.CaseFileConnections)
                .ThenInclude(e => e.CaseFileConnectionType);
    }
}
