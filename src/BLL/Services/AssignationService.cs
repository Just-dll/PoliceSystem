using AngularApp1.Server.Models;
using AutoMapper;
using BLL.Grpc;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using PoliceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AssignationService : IAssignationService
    {
        private readonly Identity.IdentityClient identityClient;
        private readonly NotificationService.NotificationServiceClient notificationClient;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AssignationService(Identity.IdentityClient identityClient, NotificationService.NotificationServiceClient notificationClient, 
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.identityClient = identityClient;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.notificationClient = notificationClient;
        }

        public async Task<int> Assign(int casefileId, JudiciaryPosition position)
        {
            var caseFile = await unitOfWork.CaseFileRepository.GetByIdAsync(casefileId);
            ArgumentNullException.ThrowIfNull(caseFile);
            var randomPerson = await identityClient.GetRandomPersonInRoleAsync(new() { RoleName = position.ToString() });
            if (randomPerson.PersonId == 0 || caseFile.CaseFileConnections.Any(cfc => cfc.PersonId == randomPerson.PersonId))
            {
                return -1;
            }
            caseFile.CaseFileConnections.Add(new()
            {
                CaseFileId = casefileId,
                PersonId = randomPerson.PersonId,
            });
            await unitOfWork.CaseFileRepository.Update(caseFile);
            var caseFileExchange = $"caseFile_{casefileId}";
            var notificationMessage = $"Prosecutor {randomPerson.PersonId} has been assigned to case file ${casefileId}";
            await notificationClient.NotifyAsync(new() { ExchangeName = caseFileExchange, Value = notificationMessage } );
            return randomPerson.PersonId;
        }

        public async Task Assign(int casefileId, int personId)
        {
            var caseFile = await unitOfWork.CaseFileRepository.GetByIdAsync(casefileId);
            ArgumentNullException.ThrowIfNull(caseFile);
            if(caseFile.CaseFileConnections.Any(cfc => cfc.PersonId == personId))
            {
                return;
            }

            caseFile.CaseFileConnections.Add(new()
            {
                CaseFileId = casefileId,
                PersonId = personId,
            });

            await unitOfWork.CaseFileRepository.Update(caseFile);
            var caseFileExchange = $"caseFile_{casefileId}";
            var notificationMessage = $"Prosecutor {personId} has been assigned to case file ${casefileId}";
            await notificationClient.NotifyAsync(new() { ExchangeName = caseFileExchange, Value = notificationMessage });

        }
    }
}
