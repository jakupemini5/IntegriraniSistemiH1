using System.ComponentModel.DataAnnotations;

namespace IntegriraniSistemiH1.Models
{
    public class ShoppingCart
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public virtual List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
