namespace Horizon.Domain.Entities
{
    public class Buy : BaseEntity
    {
        public Guid? VisitorId { get; set; }
        public DateTime Date { get; set; }
        public bool Canceled { get; set; }
        public Guid? BuyerId { get; set; }

        public Visitor Visitor { get; set; }
        public Buyer Buyer { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
