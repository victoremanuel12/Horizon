using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces.Repositories;
using Horizon.Infra.Data.Context;

namespace Horizon.Infra.Data.Repositories
{
    public class ClassTypeRepository : Repository<ClassType>, IClassTypeRepository
    {
        public ClassTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
