
using IntegriraniSistemiH1.BLL.Services.Interfaces;
using IntegriraniSistemiH1.DAL.DatabaseContext;
using IntegriraniSistemiH1.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json.Serialization;
using System.Drawing.Text;

namespace IntegriraniSistemiH1.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IShoppingCartService shoppingCartService)
        {
            _context = context;
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var userEmail = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user != null)
            {
                var tickets = user.ShoppingCart?.Tickets?.ToList();
                return View(tickets);
            }

            return Problem("Entity set 'ApplicationDbContext.ShoppingCarts'  is null.");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userEmail = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user != null)
            {
                await _shoppingCartService.RemoveTicketFromCart(user, id);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchaseShoppingCart()
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.Identity.Name;
                var user = await _userManager.FindByEmailAsync(userEmail);

                await _shoppingCartService.PurchaseShoppingCart(user);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
