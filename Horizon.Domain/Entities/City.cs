using Horizon.Domain.Domain;

namespace Horizon.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public string UF { get; set; }

        public List<Airport> Airports { get; set; } = new List<Airport>();
    }
}
