﻿using AngularApp1.Server.Data;
using Microsoft.EntityFrameworkCore;
using PoliceDAL.Entities;
using PoliceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDAL.Repositories
{
    public class CaseFileRepository : Repository<CaseFile>, ICaseFileRepository
    {
        public CaseFileRepository(PolicedatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Warrant>> GetIssuedWarrantsByCaseFile(int caseFileId)
        {
            var caseFile = await _dbSet.Include(cf => cf.Warrants)
                .Include(e => e.CaseFileConnections)
                    .ThenInclude(e => e.Person)
                .Include(e => e.CaseFileType)
                .Where(cf => cf.Id == caseFileId).FirstOrDefaultAsync();

            if(caseFile == null)
            {
                return [];
            }

            return caseFile.Warrants;

        }

        public async override Task<CaseFile?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(cf => cf.Warrants)
                .Include(e => e.Reports)
                .Include(e => e.CaseFileConnections)
                    .ThenInclude(e => e.Person)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<CaseFile>> GetAssignedCaseFilesAsync(int personId)
        {
            return await _dbSet.Include(cf => cf.Warrants)
                .Include(cf => cf.Reports)
                .Include(cf => cf.CaseFileType)
                .Include(cf => cf.CaseFileConnections)
                .Where(cf => cf.CaseFileConnections.Any(cfc => cfc.PersonId == personId)).ToListAsync();
        }
    }
}
