using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces.Repositories;
using Horizon.Infra.Data.Context;

namespace Horizon.Infra.Data.Repositories
{
    public class BuyerRepository : Repository<Buyer>, IBuyerRepository
    {
        public BuyerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
