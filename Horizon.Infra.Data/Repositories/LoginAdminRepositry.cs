using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces.Repositories;
using Horizon.Infra.Data.Context;

namespace Horizon.Infra.Data.Repositories
{
    public class LoginAdminRepositry : Repository<Login>, ILoginAdminRepository
    {
        public LoginAdminRepositry(ApplicationDbContext context) : base(context)
        {
        }
    }
}
