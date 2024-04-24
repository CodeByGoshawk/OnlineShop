using System.Net;

namespace ResponseFramewrok;

public interface IResponse<TResult>
{
    bool IsSuccessful { get; set; }
    public string? Message { get; set; }
    public string? ErrorMessage { get; set; }
    TResult? ResultModel { get; set; }
    HttpStatusCode HttpStatusCode { get; set; }
}
