using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;

namespace calendarr_web_api.Application.UseCases.ArrApi;

public class GetArrImage : IGetArrImage
{
    private readonly IGetApisService _getApisService;
    private readonly IArrExternalServiceRepository _arrExternalServiceRepository;
    private readonly string _baseUrl = "api/v1";
    
    public GetArrImage(IArrExternalServiceRepository arrExternalServiceRepository, IGetApisService getApisService)
    {
        _arrExternalServiceRepository = arrExternalServiceRepository;
        _getApisService = getApisService;
    }


    public async Task<byte[]?> Get(string apiName, string urlImage, CancellationToken ct)
    {
        var apis = await _getApisService.GetByName(apiName, ct);

        for (int i = 0; i < apis.Length; i++)
        {
            var api = apis[i];
            bool isReacheable = await _arrExternalServiceRepository.TestArrAsync(api.Url, api.ApiKey, ct);
            if(isReacheable)
            {
                
                var fullUrl =
                    $"{api.Url}/{_baseUrl}{urlImage}&apikey={api.ApiKey}";

                return await _arrExternalServiceRepository.GetImageAsync(fullUrl, ct);
            }
        }

        return null;
    }
}
public interface IGetArrImage
{
    public Task<byte[]?> Get(string apiName, string urlImage, CancellationToken ct);
}