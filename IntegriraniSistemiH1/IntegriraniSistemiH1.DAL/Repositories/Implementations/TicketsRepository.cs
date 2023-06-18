using IntegriraniSistemiH1.DAL.DatabaseContext;
using IntegriraniSistemiH1.DAL.Entities;
using IntegriraniSistemiH1.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegriraniSistemiH1.DAL.Repositories.Implementations
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> GetOrderById(int id)
        {
            return await _context.Ticket.FirstOrDefaultAsync(ticket => ticket.Id == id);
        }

        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _context.Ticket.OrderByDescending(ticket => ticket.DateExpired).ToListAsync();
        }

        public async Task CreateTicket(Ticket ticket)
        {
            _context.Add(ticket);
            await _context.SaveChangesAsync();
        }
        public async Task UdpateTicket(Ticket ticket)
        {
            _context.Ticket.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicket(Ticket ticket)
        {
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
