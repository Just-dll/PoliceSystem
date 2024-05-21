using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using Microsoft.EntityFrameworkCore;
using PoliceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDAL.Repositories
{
    public class DrivingLicenseRepository : Repository<DrivingLicense>, IDrivingLicenseRepository
    {
        public DrivingLicenseRepository(PolicedatabaseContext context) : base(context)
        {
        }

        public async Task<DrivingLicense?> GetPersonDrivingLicense(int personId)
        {
            var license = await _dbSet.FirstOrDefaultAsync(dl => dl.DriverId == personId);

            return license;
        }
    }
}
