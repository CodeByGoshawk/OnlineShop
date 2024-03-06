using ResponseFramewrok;

namespace OnlineShop.Application.Frameworks.ServiceFrameworks.Abstracts;
public interface IService<TGetResultDto, TGetAllResultDto, TPostDto, TPostResultDto, TPutDto, TPutResultDto, TDeleteDto, TDeleteResultDto, in TPrimaryKey>
{
    //Get
    Task<IResponse<TGetAllResultDto>> GetAll();
    Task<IResponse<TGetResultDto>> Get(TPrimaryKey id);

    // Post
    Task<IResponse<TPostResultDto>> Post(TPostDto model);

    // Put
    Task<IResponse<TPutResultDto>> Put(TPutDto model);

    // Delete
    Task<IResponse<TDeleteResultDto>> Delete(TDeleteDto model);
}
