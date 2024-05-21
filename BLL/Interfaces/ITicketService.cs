using AngularApp1.Server.Models;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketModel>> GetAllAsync();
        Task<TicketModel> GetByIdAsync(int ticketId);
        Task AddAsync(TicketModel ticket, User issuer);
        Task UpdateAsync(TicketModel ticket);
        Task<IEnumerable<PersonTicketModel>> GetPersonTicketsAsync(int personId);
    }
}
