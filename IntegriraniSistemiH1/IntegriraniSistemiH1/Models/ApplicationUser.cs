using Microsoft.AspNetCore.Identity;

namespace IntegriraniSistemiH1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ShoppingCart ShoppingCart { get; set; } =  new ShoppingCart();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
