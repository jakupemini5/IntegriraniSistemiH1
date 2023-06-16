﻿namespace IntegriraniSistemiH1.Models
{
    public class Order
    {
        public string Id { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public DateTime DatePurchased { get; set; }
    }
}
