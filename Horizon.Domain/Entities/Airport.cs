using Horizon.Domain.Entities;

namespace Horizon.Domain.Domain
{
    public class Airport : BaseEntity
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public City City { get; set; }
    }
}
 