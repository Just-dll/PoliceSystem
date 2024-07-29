using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

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
