using IntegriraniSistemiH1.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegriraniSistemiH1.BLL.Services.Interfaces
{
    public interface ITicketsService
    {
        Task CreateTicket(Ticket item);
        Task UpdateTicket(Ticket item);
        Task<Ticket> DeleteTicket(int id);
        Task<Ticket> GetTicketById(int id);
        Task<List<Ticket>> GetAllTickets();
    }
}
