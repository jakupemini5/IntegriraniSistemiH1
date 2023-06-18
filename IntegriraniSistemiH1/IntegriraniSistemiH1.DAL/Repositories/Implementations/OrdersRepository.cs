using IntegriraniSistemiH1.DAL.DatabaseContext;
using IntegriraniSistemiH1.DAL.Entities;
using IntegriraniSistemiH1.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
    }
}
