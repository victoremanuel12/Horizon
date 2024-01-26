using Horizon.Domain.Domain;
using Horizon.Domain.Interfaces.Repositories;
using Horizon.Infra.Data.Context;

namespace Horizon.Infra.Data.Repositories
{
    public class AirportRepository : Repository<Airport>, IAirportRepository
    {
        public AirportRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
