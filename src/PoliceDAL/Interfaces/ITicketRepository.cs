using AngularApp1.Server.Models;

namespace PoliceDAL.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetPersonTicketsAsync(int personId);
    }
}