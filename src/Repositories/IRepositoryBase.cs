using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sportdesk_backend.Repositories;

public interface IRepositoryBase<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}