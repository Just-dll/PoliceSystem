using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using BLL.Grpc;
using BLL.Interfaces;
using DAL.Interfaces;
using BLL.Models;
using DAL.Entities;

namespace BLL.Services;

public class CaseFileService : ICaseFileService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private Identity.IdentityClient identityClient;
    private readonly IUserService userService;
    private NotificationService.NotificationServiceClient notificationClient;

    public CaseFileService(IMapper mapper, IUnitOfWork unitOfWork, IUserService userService, Identity.IdentityClient client, NotificationService.NotificationServiceClient notificationClient)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        identityClient = client;
        this.userService = userService;
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
        var retreivedCaseFile = await _unitOfWork.CaseFileRepository.GetByIdAsync(id);
        var mapped = _mapper.Map<CaseFileModel>(retreivedCaseFile);
        await PopulateConnectedPersons(mapped, retreivedCaseFile.CaseFileConnections);
        return mapped;
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

    private async Task PopulateConnectedPersons(CaseFileModel caseFile, IEnumerable<CaseFileConnection> connections)
    {
        var connectedPersons = new Dictionary<string, IEnumerable<UserSearchModel>>();

        if(connections == null)
        {
            return;
        }

        foreach (var connection in connections)
        {
            var connectionType = connection.CaseFileConnectionType?.Value ?? "Unknown";
            if (!connectedPersons.ContainsKey(connectionType))
            {
                connectedPersons[connectionType] = new List<UserSearchModel>();
            }

            var userModel = await userService.GetUserAsync(connection.PersonId);
            if (userModel != null)
            {
                ((List<UserSearchModel>)connectedPersons[connectionType]).Add(_mapper.Map<UserSearchModel>(userModel));
            }
        }

        caseFile.ConnectedPersons = connectedPersons;
    }
}
