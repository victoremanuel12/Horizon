using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces.Repositories;
using Horizon.Infra.Data.Context;

namespace Horizon.Infra.Data.Repositories
{
    public class VisitorRepository : Repository<Visitor>, IVisitorRepository
    {
        public VisitorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
