using AutoMapper;
using BLL.Grpc;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services;

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

    public async Task<CaseFileConnectionModel> Assign(int casefileId, JudiciaryPosition position)
    {
        var caseFile = await unitOfWork.CaseFileRepository.GetByIdAsync(casefileId);
        ArgumentNullException.ThrowIfNull(caseFile);
        var randomPerson = await identityClient.GetRandomPersonInRoleAsync(new() { RoleName = position.ToString() });
        if (randomPerson.PersonId == 0 || caseFile.CaseFileConnections.Any(cfc => cfc.PersonId == randomPerson.PersonId))
        {
            return new()
            {
                ConnectedPerson = null,
            };
        }
        CaseFileConnection connection = new()
        {
            CaseFileId = casefileId,
            PersonId = randomPerson.PersonId,
            CaseFileConnectionTypeId = (int)ParseJudiciaryPositionToConnectionType(position)
        };
        caseFile.CaseFileConnections.Add(connection);
        await unitOfWork.CaseFileRepository.Update(caseFile);
        var caseFileExchange = $"caseFile_{casefileId}";
        var notificationMessage = $"Prosecutor {randomPerson.PersonId} has been assigned to case file ${casefileId}";
        await notificationClient.NotifyAsync(new() { ExchangeName = caseFileExchange, Value = notificationMessage } );
        return mapper.Map<CaseFileConnectionModel>(connection);
    }

    public async Task<CaseFileConnectionModel> Assign(int casefileId, int personId, int connectionTypeId = 1)
    {
        var caseFile = await unitOfWork.CaseFileRepository.GetByIdAsync(casefileId);
        ArgumentNullException.ThrowIfNull(caseFile);
        var connection = caseFile.CaseFileConnections.FirstOrDefault(cfc => cfc.PersonId == personId);
        if (connection != null)
        {
            return mapper.Map<CaseFileConnectionModel>(connection); ;
        }

        connection = new()
        {
            CaseFileId = casefileId,
            PersonId = personId,
            CaseFileConnectionTypeId = connectionTypeId,
        };
        caseFile.CaseFileConnections.Add(connection);

        await unitOfWork.CaseFileRepository.Update(caseFile);
        var caseFileExchange = $"caseFile_{casefileId}";
        var notificationMessage = $"Prosecutor {personId} has been assigned to case file ${casefileId}";
        await notificationClient.NotifyAsync(new() { ExchangeName = caseFileExchange, Value = notificationMessage });

        return mapper.Map<CaseFileConnectionModel>(connection);
    }

    private CaseFileConnectionTypeEnum ParseJudiciaryPositionToConnectionType(JudiciaryPosition position)
    {
        return position switch
        {
            JudiciaryPosition.Prosecutor => CaseFileConnectionTypeEnum.Prosecutor,
            JudiciaryPosition.Judge => CaseFileConnectionTypeEnum.Judge,
            JudiciaryPosition.Attourney => CaseFileConnectionTypeEnum.Attourney,
            _ => CaseFileConnectionTypeEnum.Witness,
        };
    }

}
