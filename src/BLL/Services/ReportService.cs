using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using PoliceDAL.Interfaces;
using PoliceDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportService : IService<ReportModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddAsync(ReportModel entity)
        {
            entity.DateOfReport = DateOnly.FromDateTime(DateTime.UtcNow);
            var report = mapper.Map<Report>(entity);
            await unitOfWork.ReportRepository.AddAsync(report);
            entity.Id = report.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var report = await unitOfWork.ReportRepository.GetByIdAsync(id);
            if(report == null)
            {
                return;
            }
            unitOfWork.ReportRepository.Remove(report);
        }

        public Task DeleteAsync(ReportModel entity)
        {
            var report = mapper.Map<Report>(entity);
            unitOfWork.ReportRepository.Remove(report);
            return Task.CompletedTask;
        }

        public async Task<ReportModel?> GetByIdAsync(int id)
        {
            var report = await unitOfWork.ReportRepository.GetByIdAsync(id);
            return mapper.Map<ReportModel?>(report);
        }

        public Task UpdateAsync(ReportModel entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, ReportModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
