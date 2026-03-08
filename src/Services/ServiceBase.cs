namespace sportdesk_backend.Services;

using sportdesk_backend.Repositories;
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

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await Repository.GetByIdAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Repository.GetAllAsync();
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        return await Repository.CreateAsync(entity);
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        return await Repository.UpdateAsync(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await Repository.DeleteAsync(id);
    }
}

