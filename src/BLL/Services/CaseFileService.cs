using AngularApp1.Server.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PoliceDAL.Entities;
using PoliceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Grpc;
using StackExchange.Redis;

namespace BLL.Services
{
    public class CaseFileService : ICaseFileService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private Identity.IdentityClient identityClient;
        private NotificationService.NotificationServiceClient notificationClient;

        public CaseFileService(IMapper mapper, IUnitOfWork unitOfWork, Identity.IdentityClient client, NotificationService.NotificationServiceClient notificationClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            identityClient = client;
            this.notificationClient = notificationClient;
        }

        public async Task AddAsync(CaseFileModel entity)
        {
            entity.InitiationDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var ent = _mapper.Map<CaseFile>(entity);
            await _unitOfWork.CaseFileRepository.AddAsync(ent);
            entity.Id = ent.Id;
            await notificationClient.NotifyAsync(new() { ExchangeName = $"caseFile_{entity.Id}", Value = "Started the caseFile"});
        }

        public async Task<CaseFileModel?> GetByIdAsync(int id)
        {
            var res = await _unitOfWork.CaseFileRepository.GetByIdAsync(id);
            var test = _mapper.Map<CaseFileModel>(res);
            test.Prosecutor = _mapper.Map<UserSearchModel>(await GetPersonAsync(res.CaseFileConnections, JudiciaryPosition.Prosecutor));
            test.Judge = _mapper.Map<UserSearchModel>(await GetPersonAsync(res.CaseFileConnections, JudiciaryPosition.Judge));
            return test;
        }
        public async Task<IEnumerable<CaseFilePreview>?> GetAssignedCaseFiles(int personId)
        {
            var res = await _unitOfWork.CaseFileRepository.GetAssignedCaseFilesAsync(personId);
            return res.Select(cf => _mapper.Map<CaseFilePreview>(cf));
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

        private async Task<User?> GetPersonAsync(IEnumerable<CaseFileConnection> connections, JudiciaryPosition position)
        {
            foreach (var connection in connections)
            {
                var response = await identityClient.GetPersonRolesAsync(new() { PersonId = connection.PersonId });
                if (response.Roles.Any(r => r.Name == position.ToString()))
                {
                    return connection.Person;
                }
            }
            return null;
        }
    }
}
