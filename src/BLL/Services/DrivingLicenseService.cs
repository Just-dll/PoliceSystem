using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.EntityFrameworkCore;
using PoliceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DrivingLicenseService : IDrivingLicenseService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public DrivingLicenseService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAsync(DrivingLicenseModel entity)
        {
            var drivingLicense = mapper.Map<DrivingLicense>(entity);
            drivingLicense.IssueDate = DateOnly.FromDateTime(DateTime.UtcNow);
            drivingLicense.ExpirationDate = drivingLicense.IssueDate.AddYears(5);

            await unitOfWork.DrivingLicenseRepository.AddAsync(drivingLicense);
            entity.Id = drivingLicense.Id;
            entity.IssueDate = drivingLicense.IssueDate;
            entity.ExpirationDate = drivingLicense.ExpirationDate;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DrivingLicenseModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<DrivingLicenseModel?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DrivingLicenseModel?> GetPersonDrivingLicense(int personId)
        {
            var license = await unitOfWork.DrivingLicenseRepository.GetPersonDrivingLicense(personId);
            
            return mapper.Map<DrivingLicenseModel>(license);
        }

        public Task UpdateAsync(DrivingLicenseModel entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, DrivingLicenseModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
