using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Frameworks.Abstracts;
public interface IRepository<TEntity, in TPrimaryKey> where TEntity : class
{
    // Create
    Task<IResponse<object>> InsertAsync(TEntity entity);

    // Read
    Task<IResponse<List<TEntity>>> SelectAsync();
    Task<IResponse<TEntity>> SelectByIdAsync(TPrimaryKey id);

    // Update
    Task<IResponse<object>> UpdateAsync(TEntity entity);

    // Delete
    Task<IResponse<object>> DeleteAsync(TPrimaryKey id);
    Task<IResponse<object>> DeleteAsync(TEntity entity);

    // Save
    Task SaveAsync();
}
