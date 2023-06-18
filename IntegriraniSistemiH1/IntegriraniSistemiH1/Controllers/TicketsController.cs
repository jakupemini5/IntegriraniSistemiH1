using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;
using IntegriraniSistemiH1.DAL.DatabaseContext;
using IntegriraniSistemiH1.DAL.Entities;
using IntegriraniSistemiH1.BLL.Services.Interfaces;

namespace IntegriraniSistemiH1.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITicketsService _ticketService;
        private readonly IShoppingCartService _shoppingCartService;

        public TicketsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ITicketsService ticketService,
            IShoppingCartService shoppingCartService)
        {
            _context = context;
            _userManager = userManager;
            _ticketService = ticketService;
            _shoppingCartService = shoppingCartService;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            return View(await _ticketService.GetAllTickets());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _ticketService.GetTicketById(id));
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,DateCreated,DateExpired,Price")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                await _ticketService.CreateTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketService.GetTicketById(id);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DateCreated,DateExpired,Price")] Ticket ticket)
        {
            await _ticketService.UpdateTicket(ticket);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketService.GetTicketById(id);
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.Identity.Name;
                var user = await _userManager.FindByEmailAsync(userEmail);

                var ticket = await _ticketService.GetTicketById(id);
                await _shoppingCartService.AddTicketToCart(user, ticket);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }



        private bool TicketExists(int id)
        {
            return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
