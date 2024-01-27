using Horizon.Domain.Interfaces.Repositories;
using Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Horizon.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> GetByExpressionAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
        }
        public async Task<IQueryable<T>> GetAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result.AsQueryable();
        }
        public async Task<IEnumerable<T>> GetListByExpressionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            var idProperty = entity.GetType().GetProperty("Id");

            if (idProperty?.PropertyType == typeof(Guid) && (Guid)idProperty.GetValue(entity) == Guid.Empty)
            {
                idProperty.SetValue(entity, Guid.NewGuid());
            }
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }


        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Remove(entity);
            return entity;
        }

    }
}
