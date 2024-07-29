using BLL.Models;

namespace BLL.Interfaces;

public interface ITicketService
{
    Task<IEnumerable<TicketModel>> GetAllAsync();
    Task<TicketModel?> GetByIdAsync(int ticketId);
    Task AddAsync(TicketModel ticket, int issuerId);
    Task UpdateAsync(TicketModel ticket);
    Task<IEnumerable<PersonTicketModel>> GetPersonTicketsAsync(int personId);
    Task RemoveAsync(TicketModel model);
}

