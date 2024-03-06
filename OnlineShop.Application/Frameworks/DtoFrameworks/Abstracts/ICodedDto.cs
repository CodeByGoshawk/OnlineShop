namespace OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;
public interface ICodedDto<TCode>
{
    TCode Code { get; set; }
}
