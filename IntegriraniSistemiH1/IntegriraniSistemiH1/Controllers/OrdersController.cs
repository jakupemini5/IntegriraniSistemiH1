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

            return View(user.Orders.OrderByDescending(ticket => ticket.DatePurchased).ToList());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var userEmail = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userEmail);

            return View(user.Orders.FirstOrDefault(order => order.Id == id));
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var order = _ordersService.GetOrderById(id);
                return View(order);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound();
            }

            
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var order = _ordersService.DeleteOrder(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            
        }
    }
}
