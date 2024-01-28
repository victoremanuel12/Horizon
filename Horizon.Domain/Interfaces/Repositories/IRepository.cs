﻿using System.Linq.Expressions;

namespace Horizon.Domain.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByExpressionAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetListByExpressionAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(Guid id);
        Task<IQueryable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        Task<T> UpdateWithCondition(Expression<Func<T, bool>> condition);
        T Delete(T entity);
    }
}
