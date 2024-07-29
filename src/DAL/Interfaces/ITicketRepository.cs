using DAL.Entities;

namespace DAL.Interfaces;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetPersonTicketsAsync(int personId);
}