using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Hangfire;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PoliceDAL.Entities;
using PoliceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TicketService : ITicketService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ProsecutorAssignationService prosecutorAssignationService;
        private readonly NotificationService notificationService;
        public TicketService(IMapper mapper, IUnitOfWork unitOfWork, 
            ProsecutorAssignationService prosecutorAssignationService, NotificationService notificationService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.prosecutorAssignationService = prosecutorAssignationService;
            this.notificationService = notificationService;
        }

        public async Task AddAsync(TicketModel ticket, User issuer)
        {
            var caseFile = new CaseFile()
            {
                CaseFileTypeId = 1,
            };

            await unitOfWork.CaseFileRepository.AddAsync(caseFile);
            var report = new Report()
            {
                IssuerId = issuer.Id,
                DateOfIssuing = DateOnly.FromDateTime(DateTime.UtcNow),
                CaseFileId = caseFile.Id,
            };
            await unitOfWork.ReportRepository.AddAsync(report);
            var ticketNew = new Ticket()
            {
                ReportId = report.Id,
                Fine = ticket.Fine,
                ViolatorId = ticket.ViolatorId,
            };
            notificationService.CreateExchange($"caseFile_{caseFile.Id}");
            await notificationService.ConnectToExchange($"user_{ticket.ViolatorId}", $"caseFile_{caseFile.Id}");
            await notificationService.Publish($"caseFile_{caseFile.Id}", "Ticket issued");
            await unitOfWork.TicketRepository.AddAsync(ticketNew);
            BackgroundJob.Schedule(() => CheckPayment(caseFile.Id), TimeSpan.FromSeconds(30));
            ticket.Id = ticketNew.Id;
        }

        public async Task CheckPayment(int caseFileId)
        {
            var ticket = unitOfWork.TicketRepository.GetByIdAsync(caseFileId);
            if(ticket != null)
            {
                await prosecutorAssignationService.AssignProsecutor(caseFileId);
            }
        }
        public async Task<IEnumerable<TicketModel>> GetAllAsync()
        {
            var allTickets = await unitOfWork.TicketRepository.GetAllAsync();
            var ticketModels = allTickets.Select(ticket => mapper.Map<TicketModel>(ticket)).ToList();
            return ticketModels;
        }


        public Task<TicketModel> GetByIdAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PersonTicketModel>> GetPersonTicketsAsync(int personId)
        {
            var tickets = await unitOfWork.TicketRepository.GetPersonTicketsAsync(personId);
                
            return tickets.Select(e => mapper.Map<PersonTicketModel>(e));
        }

        public Task UpdateAsync(TicketModel ticket)
        {
            throw new NotImplementedException();
        }
    }
}
