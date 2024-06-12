using ResponseFramewrok;

namespace OnlineShop.Application.Frameworks.ServiceFrameworks.Abstracts;
public interface IService<TGetDto, TGetResultDto, TGetAllResultDto, TPostDto, TPutDto, TDeleteDto, in TPrimaryKey>
{
    //Get
    Task<IResponse<TGetAllResultDto>> GetAll();
    Task<IResponse<TGetResultDto>> Get(TGetDto model);

    // Post
    Task<IResponse<object>> Post(TPostDto model);

    // Put
    Task<IResponse<object>> Put(TPutDto model);

    // Delete
    Task<IResponse<object>> Delete(TDeleteDto model);
}
