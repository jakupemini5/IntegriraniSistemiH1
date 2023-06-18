using IntegriraniSistemiH1.BLL.Services.Interfaces;
using IntegriraniSistemiH1.DAL.DatabaseContext;
using IntegriraniSistemiH1.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegriraniSistemiH1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrdersService _ordersService;

        public OrdersController(UserManager<ApplicationUser> userManager, IOrdersService ordersService)
        {
            _userManager = userManager;
            _ordersService = ordersService;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var userEmail = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userEmail);

            var orders = await _ordersService.GetUsersOrders(user);

            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var userEmail = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userEmail);

            var order = await _ordersService.GetOrderById(id);

            return View(order);
        }
    }
}
