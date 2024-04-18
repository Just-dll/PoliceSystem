using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService
    {
        Task<IEnumerable<TicketModel>> GetAllAsync();
        Task<TicketModel> GetByIdAsync(int ticketId);
        Task AddAsync(TicketModel ticket);
        Task UpdateAsync(TicketModel ticket);
    }
}
