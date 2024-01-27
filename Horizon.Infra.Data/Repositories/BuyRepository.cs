using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces.Repositories;
using Horizon.Infra.Data.Context;

namespace Horizon.Infra.Data.Repositories
{
    public class BuyRepository : Repository<Buy>, IBuyRepository
    {
        public BuyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
