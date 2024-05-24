using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.UseCases.ArrApi;


public class GetAllMoviesUseCase : IGetAllMoviesUseCase
{
    private readonly IGetApisService _getApisService;
    private readonly IArrExternalServiceRepository _arrExternalServiceRepository;
    private readonly string _baseUrl = "api/v3/calendar";

    public GetAllMoviesUseCase(IGetApisService getApisService, IArrExternalServiceRepository arrExternalServiceRepository)
    {
        _getApisService = getApisService;
        _arrExternalServiceRepository = arrExternalServiceRepository;
    }


    public async Task<List<ArrApiResult>> Get(DateTime start, DateTime end, CancellationToken ct)
    {
        List<ArrApiResult> results = new();
        var apis = await _getApisService.Get(ApiTypeEnum.Radarr, ct);
        
        for (int i = 0; i < apis.Length; i++)
        {
            var api = apis[i];

            bool isReacheable = await _arrExternalServiceRepository.TestArrAsync(api.Url, api.ApiKey, ct);
            string? result = ArrApiExtension.DefaultContent;
            if(isReacheable)
            {
                var startDate = start.ToUniversalTime().ToString("o");
                var endDate = end.ToUniversalTime().ToString("o");

                var fullUrl =
                    $"{api.Url}/{_baseUrl}?start={startDate}&end={endDate}&unmonitored=true&apikey={api.ApiKey}";
                result = await _arrExternalServiceRepository.GetAsync(fullUrl, ct);
            }
            results.Add(
                new(
                    api.Url,
                    isReacheable,
                    result ?? ArrApiExtension.DefaultContent
                )
            );
        }

        return results;
    }
}

public interface IGetAllMoviesUseCase
{
    public Task<List<ArrApiResult>> Get(DateTime start, DateTime end, CancellationToken ct);
}