using Horizon.Domain.Entities;

namespace Horizon.Aplication.Dtos
{
    public class FlightDetailsDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime Time { get; set; }
        public Guid OriginId { get; set; }
        public Guid DestinyId { get; set; }
        public string NameOrigin { get; set; }
        public string NameDestiny { get; set; }
        public bool Canceled { get; set; }
        public List<ClassDto> Classes { get; set; }
    }
}
