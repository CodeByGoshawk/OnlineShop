using PublicTools.Attributes;

namespace OnlineShop.Backoffice.WebApiEndPoint.Authorizations.Handlers;

public class OwnerOnlyResource(object service, object modelId)
{
    private readonly object _service = service;
    private readonly object _modelId = modelId;

    public async Task<List<string>> GetOwnerId()
    {
        var method = _service!.GetType().GetMethod("Authorize");
        dynamic response = method!.Invoke(_service, [_modelId!])!;
        var resultModel = (await response).ResultModel!;
        if (resultModel is null) return null!;

        var ownerIdProperties = ((object)resultModel!).GetType()
            .GetProperties()
            .Where(p => p.IsDefined(typeof(RequesterIdAttribute), false));

        return ownerIdProperties!.Select(p => (string)p.GetValue(resultModel)).ToList();
    }
}
