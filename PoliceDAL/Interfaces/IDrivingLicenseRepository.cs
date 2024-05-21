using AngularApp1.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDAL.Interfaces
{
    public interface IDrivingLicenseRepository : IRepository<DrivingLicense>
    {
        Task<DrivingLicense?> GetPersonDrivingLicense(int personId);
    }
}
