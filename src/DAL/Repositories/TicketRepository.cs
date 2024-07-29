using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(PolicedatabaseContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await _dbSet.Include(e => e.Violator)
            .Include(e => e.Report)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetPersonTicketsAsync(int personId)
    {
        return await _dbSet.Include(e => e.Report).Where(t => t.ViolatorId == personId).ToListAsync();
    }
}
