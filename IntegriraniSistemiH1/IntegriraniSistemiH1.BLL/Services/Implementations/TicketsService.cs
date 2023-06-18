using IntegriraniSistemiH1.BLL.Services.Interfaces;
using IntegriraniSistemiH1.DAL.Entities;
using IntegriraniSistemiH1.DAL.Repositories.Implementations;
using IntegriraniSistemiH1.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegriraniSistemiH1.BLL.Services.Implementations
{
    public class TicketsService : ITicketsService
    {
        private readonly ITicketsRepository _ticketsRepository;

        public TicketsService(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task CreateTicket(Ticket item)
        {
            await _ticketsRepository.CreateTicket(item);
        }

        public async Task UpdateTicket(Ticket item)
        {
            await _ticketsRepository.UdpateTicket(item);
        }

        public async Task<Ticket> DeleteTicket(int id)
        {
            var order = await GetTicketById(id);
            await _ticketsRepository.DeleteTicket(order);
            return order;
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            var foundOrder = await _ticketsRepository.GetOrderById(id);
            if (foundOrder == null)
            {
                throw new KeyNotFoundException();
            }
            return foundOrder;
        }

        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _ticketsRepository.GetAllTickets();
        }
    }
}
