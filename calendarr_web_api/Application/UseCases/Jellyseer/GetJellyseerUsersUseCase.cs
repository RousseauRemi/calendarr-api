using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;
using calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;
using calendarr_web_api.Application.UseCases.ArrApi;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.UseCases.Jellyseer;

public class GetJellyseerUsersUseCase : IGetJellyseerUsersUseCase
{
    private readonly IGetJellyseerEntitysService _getJellyseerEntitysService;
    private readonly IJellyseerExternalServiceRepository _jellyseerExternalServiceRepository;
    private readonly string _baseUrl = "api/v1/user";

    public GetJellyseerUsersUseCase(IJellyseerExternalServiceRepository jellyseerExternalServiceRepository, IGetJellyseerEntitysService getJellyseerEntitysService)
    {
        _jellyseerExternalServiceRepository = jellyseerExternalServiceRepository;
        _getJellyseerEntitysService = getJellyseerEntitysService;
    }


    public async Task<List<JellyseerApiResult>> Get(CancellationToken ct)
    {
        List<JellyseerApiResult> results = new();
        var jellyseerEntities = await _getJellyseerEntitysService.Get(ct);

        for (int i = 0; i < jellyseerEntities.Length; i++)
        {
            var api = jellyseerEntities[i];
            bool isReacheable = await _jellyseerExternalServiceRepository.TestJellyseerAsync(api.Url, ct);
            string? result = ArrApiExtension.DefaultContent;
            if(isReacheable)
            {
                var fullUrl =
                    $"{api.Url}/{_baseUrl}?take=100&skip=0&sort=created";
                result = await _jellyseerExternalServiceRepository.GetAsync(fullUrl, api.ApiKey, ct);
            }
            results.Add(
                new(
                    api.Url,
                    isReacheable,
                    result
                )
            );
        }

        return results;
    }
}

public interface IGetJellyseerUsersUseCase
{
    public Task<List<JellyseerApiResult>> Get(CancellationToken ct);
}