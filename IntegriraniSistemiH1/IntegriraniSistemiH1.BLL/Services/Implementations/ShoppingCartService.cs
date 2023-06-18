using IntegriraniSistemiH1.BLL.Services.Interfaces;
using IntegriraniSistemiH1.DAL.Entities;
using IntegriraniSistemiH1.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegriraniSistemiH1.BLL.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ITicketsService _ticketsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ITicketsService ticketsService, UserManager<ApplicationUser> userManager)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _ticketsService = ticketsService;
            _userManager = userManager;
        }

        public async Task<ShoppingCart> GetShoppingCartById(string id)
        {
            var foundShoppingCart = await _shoppingCartRepository.GetShoppingCartById(id);
            if (foundShoppingCart == null)
            {
                throw new KeyNotFoundException();
            }
            return foundShoppingCart;
        }

        public async Task AddTicketToCart(ApplicationUser user, Ticket ticket)
        {
            user.ShoppingCart.Tickets.Add(ticket);

            await _userManager.UpdateAsync(user);
        }

        public async Task RemoveTicketFromCart(ApplicationUser user, int ticketId)
        {
            var ticket = user.ShoppingCart.Tickets.FirstOrDefault(ticket => ticket.Id == ticketId);
            user.ShoppingCart.Tickets.Remove(ticket);

            await _userManager.UpdateAsync(user);
        }

        public async Task PurchaseShoppingCart(ApplicationUser user)
        {
            if(user.ShoppingCart.Tickets.Count() > 0)
            {
                var order = new Order()
                {
                    DatePurchased = DateTime.UtcNow,
                    Id = Guid.NewGuid().ToString(),
                    Tickets = new List<Ticket>()
                };
                order.Tickets.AddRange(user.ShoppingCart.Tickets.ToList());

                user.Orders.Add(order);
                user.ShoppingCart.Tickets = new List<Ticket>();

                await _userManager.UpdateAsync(user);
            }
        }
    }
}
