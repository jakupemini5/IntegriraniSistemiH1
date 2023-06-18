using IntegriraniSistemiH1.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegriraniSistemiH1.DAL.Repositories.Interfaces
{
    public interface ITicketsRepository
    {
        Task<Ticket> GetOrderById(int id);
        Task<List<Ticket>> GetAllTickets();
        Task CreateTicket(Ticket order);
        Task UdpateTicket(Ticket ticket);
        Task DeleteTicket(Ticket order);
    }
}
