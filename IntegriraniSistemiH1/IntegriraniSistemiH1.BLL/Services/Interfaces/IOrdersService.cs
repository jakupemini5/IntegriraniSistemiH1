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
        Task<Order> GetOrderById(string id);
        Task<List<Order>> GetUsersOrders(ApplicationUser user);
    }
}
