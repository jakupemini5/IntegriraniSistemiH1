using IntegriraniSistemiH1.Data;
using IntegriraniSistemiH1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegriraniSistemiH1.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        // GET: ShoppingCarts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ShoppingCarts == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ShoppingCarts == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var userEmail = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user != null)
            {
                var ticket = user.ShoppingCart.Tickets.FirstOrDefault(ticket => ticket.Id == id);
                user.ShoppingCart.Tickets.Remove(ticket);

                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ShoppingCarts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ShoppingCarts'  is null.");
            }
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCarts.Remove(shoppingCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(string id)
        {
            return (_context.ShoppingCarts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
