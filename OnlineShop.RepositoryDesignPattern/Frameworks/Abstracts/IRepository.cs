using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Frameworks.Abstracts;
public interface IRepository<TEntity, in TPrimaryKey> where TEntity : class
{
    // Create
    Task<IResponse> InsertAsync(TEntity entity);

    // Read
    Task<IResponse<List<TEntity>>> SelectAllAsync();
    Task<IResponse<TEntity>> SelectByIdAsync(TPrimaryKey id);

    // Update
    Task<IResponse> UpdateAsync(TEntity entity);

    // Delete
    Task<IResponse> DeleteAsync(TPrimaryKey id);
    Task<IResponse> DeleteAsync(TEntity entity);

    // Save
    Task SaveAsync();
}
