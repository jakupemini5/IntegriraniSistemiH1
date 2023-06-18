using IntegriraniSistemiH1.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegriraniSistemiH1.DAL.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        Task<Order> GetOrderById(string id);
        Task CreateOrder(Order order);
        Task DeleteOrder(Order order);
    }
}
