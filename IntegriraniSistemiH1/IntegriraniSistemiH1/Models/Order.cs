namespace IntegriraniSistemiH1.Models
{
    public class Order
    {
        public string Id { get; set; }
        public List<Ticket> Tickets { get; set; }
        public DateTime DatePurchased { get; set; }
    }
}
