using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.UseCases.Config.Jellyseer;

public class GetJellyseerEntitysUseCase: IGetJellyseerEntitysUseCase
{
    
    private readonly IGetJellyseerEntitysService _getJellyseerEntitysService;

    public GetJellyseerEntitysUseCase(IGetJellyseerEntitysService getJellyseerEntitysService)
    {
        _getJellyseerEntitysService = getJellyseerEntitysService;
    }

    public Task<JellyseerEntity[]> Get(CancellationToken ct)
    {
        return _getJellyseerEntitysService.Get(ct);
    }
}

public interface IGetJellyseerEntitysUseCase
{
    public Task<JellyseerEntity[]> Get(CancellationToken ct);
}