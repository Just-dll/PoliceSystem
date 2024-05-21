using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using PoliceDAL.Entities;
using PoliceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CaseFileService : ICaseFileService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CaseFileService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CaseFileModel entity)
        {
            var ent = _mapper.Map<CaseFile>(entity);
            await _unitOfWork.CaseFileRepository.AddAsync(ent);
            entity.Id = ent.Id;
        }

        public async Task<CaseFileModel?> GetByIdAsync(int id)
        {
            var res = await _unitOfWork.CaseFileRepository.GetByIdAsync(id);
            var test = _mapper.Map<CaseFileModel>(res);
            return test;
        }

        public Task UpdateAsync(CaseFileModel entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, CaseFileModel entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CaseFileModel entity)
        {
            throw new NotImplementedException();
        }

        
    }
}
