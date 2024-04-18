using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        private readonly PolicedatabaseContext context;
        public TicketService(IMapper mapper, PolicedatabaseContext context)
        {
            this.mapper = mapper;
            this.context = context; 
        }

        public async Task AddAsync(TicketModel ticket)
        {
            var report = new Report()
            {
                IssuerId = ticket.ViolatorId,
                DateOfIssuing = DateTime.UtcNow
            };
            var ticketNew = new Ticket()
            {
                Report = report,
                Fine = ticket.Fine,
                ViolatorId = ticket.ViolatorId,
            };

            context.Tickets.Add(ticketNew);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TicketModel>> GetAllAsync()
        {
            return await context.Tickets
                .Include(e => e.Violator)
                .Include(e => e.Report)
                .Select(e => mapper.Map<TicketModel>(e))
                .ToListAsync();
        }


        public Task<TicketModel> GetByIdAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TicketModel>> GetPersonTicketsAsync(int personId)
        {
            var tickets = context.Tickets
                .Include(e => e.Violator)
                .Include(e => e.Report)
                .Where(e => e.ViolatorId == personId);
                
            return await tickets.Select(e => mapper.Map<TicketModel>(e)).ToListAsync();
        }

        public Task UpdateAsync(TicketModel ticket)
        {
            throw new NotImplementedException();
        }
    }
}
