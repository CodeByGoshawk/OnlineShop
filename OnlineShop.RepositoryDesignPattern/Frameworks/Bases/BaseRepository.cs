using Microsoft.AspNetCore.Identity;
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
    public virtual async Task<IResponse> InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return new Response(entity);
    }

    // Read
    public virtual async Task<IResponse<TEntity>> SelectByIdAsync(TPrimaryKey id)
    {
        if (id is null) return new Response<TEntity>(MessageResource.Error_NullInputId);
        var entity = await _dbSet.FindAsync(id);
        return entity is not null ? new Response<TEntity>(entity) : new Response<TEntity>(MessageResource.Error_FindEntityFailed);
    }
    public virtual async Task<IResponse<List<TEntity>>> SelectAllAsync()
    {
        var entityList = await _dbSet.AsNoTracking().ToListAsync();
        return new Response<List<TEntity>>(entityList);
    }

    // Update
    public virtual async Task<IResponse> UpdateAsync(TEntity entity)
    {
        await Task.Yield();
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        return new Response(entity);
    }

    // Delete
    public virtual async Task<IResponse> DeleteAsync(TPrimaryKey id)
    {
        if (id is null) return new Response(MessageResource.Error_NullInputId);
        var entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete is null) return new Response(MessageResource.Error_FindEntityFailed);
        _dbSet.Remove(entityToDelete);
        return new Response(entityToDelete);
    }
    public virtual async Task<IResponse> DeleteAsync(TEntity entity)
    {
        await Task.Yield();
        if (_dbSet.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        return new Response(entity);
    }

    // Save
    public virtual async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
