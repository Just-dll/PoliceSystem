using DAL.Entities;

namespace DAL.Interfaces;

public interface IDrivingLicenseRepository : IRepository<DrivingLicense>
{
    Task<DrivingLicense?> GetPersonDrivingLicense(int personId);
}
