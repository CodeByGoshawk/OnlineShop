using System.Net;

namespace ResponseFramewrok;

public class Response<TResult> : IResponse<TResult>
{
    public Response(TResult resultModel, bool isSuccessful, string? message, string? errorMessage, HttpStatusCode httStatusCode)
    {
        ResultModel = resultModel;
        IsSuccessful = isSuccessful;
        Message = message;
        ErrorMessage = errorMessage;
        HttpStatusCode = httStatusCode;
    }
    public Response(TResult result)
    {
        ResultModel = result;
        if (result is not null)
        {
            IsSuccessful = true;
            Message = "عملیات با موفقیت انجام شد";
            ErrorMessage = string.Empty;
            HttpStatusCode = HttpStatusCode.OK;
        }
        else
        {
            IsSuccessful = false;
            Message = string.Empty;
            ErrorMessage = "عملیات ناموفق";
            HttpStatusCode = HttpStatusCode.NotAcceptable;
        }

    }
    public Response(string errorMessage)
    {
        IsSuccessful = false;
        Message = string.Empty;
        ErrorMessage = errorMessage;
        ResultModel = default;
        HttpStatusCode = HttpStatusCode.Ambiguous;
    }

    public Response(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
        if (isSuccessful)
        {
            Message = "عملیات با موفقیت انجام شد";
            ErrorMessage = string.Empty;
            HttpStatusCode = HttpStatusCode.OK;
        }
        else
        {
            Message = string.Empty;
            ErrorMessage = "عملیات ناموفق";
            HttpStatusCode = HttpStatusCode.NotAcceptable;
        }
    }

    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
    public string? ErrorMessage { get; set; }
    public TResult? ResultModel { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
}

public class Response : Response<object>, IResponse
{
    public Response(string errorMessage) : base(errorMessage)
    {
    }

    public Response(bool isSuccessful) : base(isSuccessful)
    {
    }

    public Response(object result) : base(result)
    {
    }
}
