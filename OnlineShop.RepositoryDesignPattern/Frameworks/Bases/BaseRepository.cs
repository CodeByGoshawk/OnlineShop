﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.RepositoryDesignPattern.Frameworks.Abstracts;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Frameworks.Bases;
public class BaseRepository<TDbContext, TEntity, TPrimaryKey>(TDbContext dbContext) :
                            IRepository<TEntity, TPrimaryKey>
                                where TEntity : class
                                where TDbContext : IdentityDbContext<OnlineShopUser, OnlineShopRole, string, IdentityUserClaim<string>, OnlineShopUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>

{
    protected readonly TDbContext _dbContext = dbContext;
    protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    // Create
    public virtual async Task<IResponse<object>> InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return new Response<object>(entity);
    }

    // Read
    public virtual async Task<IResponse<TEntity>> SelectByIdAsync(TPrimaryKey id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity is not null ? new Response<TEntity>(entity) : new Response<TEntity>(MessageResource.Error_FindEntityFailed);
    }
    public virtual async Task<IResponse<List<TEntity>>> SelectAsync()
    {
        var entityList = await _dbSet.AsNoTracking().ToListAsync();
        return new Response<List<TEntity>>(entityList);
    }

    // Update
    public virtual async Task<IResponse<object>> UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        return new Response<object>(entity);
    }

    // Delete
    public virtual async Task<IResponse<object>> DeleteAsync(TPrimaryKey id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete is null) return new Response<object>(MessageResource.Error_FindEntityFailed);
        _dbSet.Remove(entityToDelete);
        return new Response<object>(entityToDelete);
    }
    public virtual async Task<IResponse<object>> DeleteAsync(TEntity entity)
    {
        if (_dbSet.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        return new Response<object>(entity);
    }

    // Save
    public virtual async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}