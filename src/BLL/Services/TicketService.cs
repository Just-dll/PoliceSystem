using AutoMapper;
using BLL.Grpc;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLL.Grpc.NotificationService;

namespace BLL.Services;

public class TicketService : ITicketService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly IAssignationService assignationService;
    private readonly NotificationServiceClient notificationService;
    public TicketService(IMapper mapper, IUnitOfWork unitOfWork, 
        IAssignationService assignationService, NotificationServiceClient notificationService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.assignationService = assignationService;
        this.notificationService = notificationService;
    }

    public async Task AddAsync(TicketModel ticket, int issuerId)
    {
        var caseFile = new CaseFile()
        {
            CaseFileTypeId = 1,
            InitiationDate = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        await unitOfWork.CaseFileRepository.AddAsync(caseFile);
        var report = new Report()
        {
            IssuerId = issuerId,
            DateOfIssuing = DateOnly.FromDateTime(DateTime.UtcNow),
            CaseFileId = caseFile.Id,
            Description = ticket.Description,
        };
        await unitOfWork.ReportRepository.AddAsync(report);
        var ticketNew = new Ticket()
        {
            ReportId = report.Id,
            Fine = ticket.Fine,
            ViolatorId = ticket.ViolatorId,
        };
        var exchangeName = $"caseFile_{caseFile.Id}";
        await notificationService.ConnectPersonToExchangeAsync(new() { ExchangeName = exchangeName, PersonId = ticket.ViolatorId });
        await notificationService.NotifyAsync(new() { ExchangeName = exchangeName, Value = "Ticket issued" });
        await unitOfWork.TicketRepository.AddAsync(ticketNew);
        BackgroundJob.Schedule(() => CheckPayment(caseFile.Id), TimeSpan.FromSeconds(30));
        ticket.Id = ticketNew.Id;
    }

    public async Task CheckPayment(int caseFileId)
    {
        var ticket = unitOfWork.TicketRepository.GetByIdAsync(caseFileId);
        if (ticket != null)
        {
            await assignationService.Assign(caseFileId, JudiciaryPosition.Prosecutor);
        }
    }
    public async Task<IEnumerable<TicketModel>> GetAllAsync()
    {
        var allTickets = await unitOfWork.TicketRepository.GetAllAsync();
        var ticketModels = allTickets.Select(ticket => mapper.Map<TicketModel>(ticket)).ToList();
        return ticketModels;
    }


    public async Task<TicketModel?> GetByIdAsync(int ticketId)
    {
        var ticket = await unitOfWork.TicketRepository.GetByIdAsync(ticketId);

        return mapper.Map<TicketModel?>(ticket);
    }

    public async Task<IEnumerable<PersonTicketModel>> GetPersonTicketsAsync(int personId)
    {
        var tickets = await unitOfWork.TicketRepository.GetPersonTicketsAsync(personId);
            
        return tickets.Select(e => mapper.Map<PersonTicketModel>(e));
    }

    public Task RemoveAsync(TicketModel model)
    {
        var ticket = mapper.Map<Ticket>(model);

        unitOfWork.TicketRepository.Remove(ticket);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(TicketModel ticket)
    {
        throw new NotImplementedException();
    }
}
