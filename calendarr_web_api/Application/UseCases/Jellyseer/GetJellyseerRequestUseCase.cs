using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;
using calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;
using calendarr_web_api.Application.UseCases.ArrApi;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.UseCases.Jellyseer;

public class GetJellyseerRequestUseCase : IGetJellyseerRequestUseCase
{
    private readonly IGetJellyseerEntitysService _getJellyseerEntitysService;
    private readonly IJellyseerExternalServiceRepository _jellyseerExternalServiceRepository;
    private readonly string _baseUrl = "api/v1/user";

    public GetJellyseerRequestUseCase(IJellyseerExternalServiceRepository jellyseerExternalServiceRepository, IGetJellyseerEntitysService getJellyseerEntitysService)
    {
        _jellyseerExternalServiceRepository = jellyseerExternalServiceRepository;
        _getJellyseerEntitysService = getJellyseerEntitysService;
    }


    public async Task<List<JellyseerApiResult>> Get(int userId, string name, CancellationToken ct)
    {
        List<JellyseerApiResult> results = new();
        JellyseerEntity[] jellyseerEntities = (await _getJellyseerEntitysService.Get(ct)).Where(e => e.Name == name).ToArray();//todo get only by name

        for (int i = 0; i < jellyseerEntities.Length; i++)
        {
            var api = jellyseerEntities[i];
            bool isReacheable = await _jellyseerExternalServiceRepository.TestJellyseerAsync(api.Url, ct);
            string? result = ArrApiExtension.DefaultContent;
            if(isReacheable)
            {
                var fullUrl =
                    $"{api.Url}/{_baseUrl}/{userId}/requests?take=500&skip=0";
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

public interface IGetJellyseerRequestUseCase
{
    public Task<List<JellyseerApiResult>> Get(int userId, string name, CancellationToken ct);
}