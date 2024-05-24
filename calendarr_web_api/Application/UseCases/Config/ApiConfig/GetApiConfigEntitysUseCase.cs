using calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.UseCases.Config.ApiConfig;

public class GetApiConfigEntitysUseCase: IGetApiConfigEntitysUseCase
{
    
    private readonly IGetApiConfigEntityService _getApiConfigEntitysService;

    public GetApiConfigEntitysUseCase(IGetApiConfigEntityService getApiConfigEntitysService)
    {
        _getApiConfigEntitysService = getApiConfigEntitysService;
    }

    public Task<ApiConfigEntity?> Get(CancellationToken ct)
    {
        return _getApiConfigEntitysService.Get(ct);
    }
}

public interface IGetApiConfigEntitysUseCase
{
    public Task<ApiConfigEntity?> Get(CancellationToken ct);
}