using OnlineShop.Application.Frameworks.ServiceFrameworks.Abstracts;
using OnlineShop.EFCore;
using OnlineShop.RepositoryDesignPattern.Frameworks.Bases;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.Application.Frameworks.ServiceFrameworks.Bases;
public class BaseService<TRepository, TEntity, TGetResultDto, TGetAllResultDto, TPostDto, TPostResultDto, TPutDto, TPutResultDto, TDeleteDto, TDeleteResultDto, TPrimaryKey> :
IService<TGetResultDto, TGetAllResultDto, TPostDto, TPostResultDto, TPutDto, TPutResultDto, TDeleteDto, TDeleteResultDto, TPrimaryKey> where TRepository : BaseRepository<OnlineShopDbContext, TEntity, TPrimaryKey> where TEntity : class
{
    protected readonly TRepository _repository;

    // Get
    public async Task<IResponse<TGetResultDto>> Get(TPrimaryKey id)
    {
        var selectedEntity = await _repository.SelectByIdAsync(id);
        if (selectedEntity is null) return new Response<TGetResultDto>(MessageResource.Error_FindEntityFailed);
        throw new NotImplementedException();
    }
    public async Task<IResponse<TGetAllResultDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    // Post
    public async Task<IResponse<TPostResultDto>> Post(TPostDto model)
    {
        throw new NotImplementedException();
    }

    // Put
    public async Task<IResponse<TPutResultDto>> Put(TPutDto model)
    {
        throw new NotImplementedException();
    }

    // Delete
    public async Task<IResponse<TDeleteResultDto>> Delete(TDeleteDto model)
    {
        throw new NotImplementedException();
    }
}
