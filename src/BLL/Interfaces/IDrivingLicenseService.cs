using BLL.Models;

namespace BLL.Interfaces;

public interface IDrivingLicenseService : IService<DrivingLicenseModel>
{
    Task<DrivingLicenseModel?> GetPersonDrivingLicense(int personId);
}
