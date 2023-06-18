using IntegriraniSistemiH1.DAL.DatabaseContext;
using IntegriraniSistemiH1.DAL.Entities;
using IntegriraniSistemiH1.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntegriraniSistemiH1.DAL.Repositories.Implementations
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext _context;

        public OrdersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(string id)
        {
            return await _context.Order.FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task CreateOrder(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(Order order)
        {
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
