using AngularApp1.Server.Data;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly PolicedatabaseContext context;
        public DrivingLicenseService(IMapper mapper, PolicedatabaseContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public Task AddAsync(TicketModel ticket)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TicketModel> GetByIdAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public async Task<DrivingLicenseModel?> GetPersonDrivingLicence(int personId)
        {
            var licence = await context.DrivingLicenses.FirstOrDefaultAsync(e => e.DriverId == personId);
            
            return mapper.Map<DrivingLicenseModel>(licence);
        }

        public Task UpdateAsync(TicketModel ticket)
        {
            throw new NotImplementedException();
        }
    }
}
