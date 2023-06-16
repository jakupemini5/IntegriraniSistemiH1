using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IntegriraniSistemiH1.Models;

namespace IntegriraniSistemiH1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<IntegriraniSistemiH1.Models.Ticket>? Ticket { get; set; }
        public DbSet<IntegriraniSistemiH1.Models.Order>? Order { get; set; }
    }
}