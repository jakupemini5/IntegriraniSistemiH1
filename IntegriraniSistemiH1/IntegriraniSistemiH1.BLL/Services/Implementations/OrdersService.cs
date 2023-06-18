using IntegriraniSistemiH1.BLL.Services.Interfaces;
using IntegriraniSistemiH1.DAL.Entities;
using IntegriraniSistemiH1.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegriraniSistemiH1.BLL.Services.Implementations
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task CreateOrder(Order item)
        {
            await _ordersRepository.CreateOrder(item);
        }

        public async Task<Order> DeleteOrder(string id)
        {
            var order = await GetOrderById(id);
            await _ordersRepository.DeleteOrder(order);
            return order;
        }

        public async Task<Order> GetOrderById(string id)
        {
            var foundOrder = await _ordersRepository.GetOrderById(id);
            if(foundOrder == null)
            {
                throw new KeyNotFoundException();
            }
            return foundOrder;
        }
    }
}
