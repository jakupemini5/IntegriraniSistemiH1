using IntegriraniSistemiH1.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegriraniSistemiH1.BLL.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetShoppingCartById(string id);
        Task AddTicketToCart(ApplicationUser user, Ticket ticket);
        Task RemoveTicketFromCart(ApplicationUser user, int ticketId);
        Task PurchaseShoppingCart(ApplicationUser user);
    }
}
