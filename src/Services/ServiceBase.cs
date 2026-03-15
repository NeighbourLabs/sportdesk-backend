namespace sportdesk_backend.Services;

using Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class ServiceBase<T, R> : IServiceBase<T> where T : class where R : IRepositoryBase<T>
{
    protected readonly R Repository;

    protected ServiceBase(R repository)
    {
        Repository = repository;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, Guid tenantId)
    {
        return await Repository.GetByIdAsync(id, tenantId);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(Guid tenantId)
    {
        return await Repository.GetAllAsync(tenantId);
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        return await Repository.CreateAsync(entity);
    }

    public virtual async Task<T> UpdateAsync(T entity, Guid tenantId)
    {
        return await Repository.UpdateAsync(entity, tenantId);
    }

    public virtual async Task DeleteAsync(Guid id, Guid tenantId)
    {
        await Repository.DeleteAsync(id, tenantId
        );
    }
}

