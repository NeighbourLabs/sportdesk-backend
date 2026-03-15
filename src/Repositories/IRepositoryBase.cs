namespace sportdesk_backend.Repositories;

public interface IRepositoryBase<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, Guid tenantId);
    Task<IEnumerable<T>> GetAllAsync(Guid tenantId);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity, Guid tenantId);
    Task DeleteAsync(Guid id, Guid tenantId);
    Task SaveChangesAsync();
}