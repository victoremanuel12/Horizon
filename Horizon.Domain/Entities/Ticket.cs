namespace Horizon.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public Guid ClassId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public decimal Price { get; set; }
        public bool Dispatch { get; set; }
        public Guid BuyId { get; set; }
        public bool Canceled { get; set; }
        public string BaggageId { get; set; } = string.Empty;

        public Class Class { get; set; }
        public Buy Buy { get; set; }
    }
}
