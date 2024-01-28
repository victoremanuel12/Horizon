namespace Horizon.Aplication.Dtos
{
    public class FlightWithNameAirportDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime Time { get; set; }
        public Guid OriginId { get; set; }
        public Guid DestinyId { get; set; }
        public string NameOrigin { get; set; }
        public string NameDestiny { get; set; }
        public bool Canceled { get; set; }
    }
}
