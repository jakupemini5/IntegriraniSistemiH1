using IntegriraniSistemiH1.DAL.Enums;

namespace IntegriraniSistemiH1.DAL.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpired { get; set; }
        public float Price { get; set; }
        public MovieType Type { get; set; }
    }
}
