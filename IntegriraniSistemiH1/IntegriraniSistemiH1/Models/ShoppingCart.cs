using System.ComponentModel.DataAnnotations;

namespace IntegriraniSistemiH1.Models
{
    public class ShoppingCart
    {
        [Key]
        public string Id { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
    }
}
