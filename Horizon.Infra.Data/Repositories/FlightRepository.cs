using Horizon.Domain.Domain;
using Horizon.Domain.Interfaces.Repositories;
using Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Horizon.Infra.Data.Repositories
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(ApplicationDbContext context) : base(context)
        {
        }
      
    }
}
