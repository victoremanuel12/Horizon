namespace Horizon.Aplication.Dtos
{
    public class BuyDto
    {
        public Guid Id { get; set; }
        public Guid VisitorId { get; set; }
        public DateTime Date { get; set; }
        public bool Canceled { get; set; }
        public Guid BuyerId { get; set; }
        public List<TicketDto> Tickets { get; set; }
    }
}
