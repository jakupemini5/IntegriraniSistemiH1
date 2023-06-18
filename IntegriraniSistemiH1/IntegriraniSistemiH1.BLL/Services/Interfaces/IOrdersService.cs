using IntegriraniSistemiH1.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegriraniSistemiH1.BLL.Services.Interfaces
{
    public interface IOrdersService
    {
        Task CreateOrder(Order item);
        Task<Order> DeleteOrder(string id);
        Task<Order> GetOrderById(string id);
    }
}
