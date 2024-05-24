using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;

namespace calendarr_web_api.Application.UseCases.ArrApi;

public class GetRemoteImage : IGetRemoteImage
{
    private readonly IGetApisService _getApisService;
    private readonly IArrExternalServiceRepository _arrExternalServiceRepository;
    private readonly string _baseUrl = "api/v1";
    
    public GetRemoteImage(IArrExternalServiceRepository arrExternalServiceRepository, IGetApisService getApisService)
    {
        _arrExternalServiceRepository = arrExternalServiceRepository;
        _getApisService = getApisService;
    }


    public async Task<byte[]?> Get(string urlImage, CancellationToken ct)
    {
        return await _arrExternalServiceRepository.GetImageAsync(urlImage, ct);

    }
}

public interface IGetRemoteImage
{
    public Task<byte[]?> Get(string urlImage, CancellationToken ct);
}