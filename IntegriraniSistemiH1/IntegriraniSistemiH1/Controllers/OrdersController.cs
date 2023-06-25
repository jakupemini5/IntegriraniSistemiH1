using IntegriraniSistemiH1.BLL.Services.Interfaces;
using IntegriraniSistemiH1.DAL.DatabaseContext;
using IntegriraniSistemiH1.DAL.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IntegriraniSistemiH1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IOrdersService _ordersService;

        public OrdersController(UserManager<ApplicationUser> userManager, IOrdersService ordersService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _ordersService = ordersService;
            _context = context;
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

        public ActionResult GeneratePDF(int id)
        {
            // Fetch the order and its tickets from the database using the provided ID
            var order = _context.Order.FirstOrDefault(order => true);

            // Create a new PDF document
            Document document = new Document();

            // Create a memory stream to write the PDF content
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            // Open the document for writing
            document.Open();

            // Add the order details to the document
            Paragraph orderDetails = new Paragraph("Order Details");
            document.Add(orderDetails);
            document.Add(new Paragraph("Date Purchased: " + order.DatePurchased));

            // Add a table for the tickets
            PdfPTable table = new PdfPTable(6);
            table.AddCell("Name");
            table.AddCell("Description");
            table.AddCell("Date Created");
            table.AddCell("Date Expired");
            table.AddCell("Price");
            table.AddCell("Type");

            foreach (var ticket in order.Tickets)
            {
                table.AddCell(ticket.Name);
                table.AddCell(ticket.Description);
                table.AddCell(ticket.DateCreated.ToString());
                table.AddCell(ticket.DateExpired.ToString());
                table.AddCell(ticket.Price.ToString());
                table.AddCell(ticket.Type.ToString());
            }

            document.Add(table);

            // Close the document
            document.Close();

            // Convert the memory stream to a byte array
            byte[] fileData = stream.ToArray();

            // Return the PDF file
            return File(fileData, "application/pdf", "order.pdf");
        }

    }
}
