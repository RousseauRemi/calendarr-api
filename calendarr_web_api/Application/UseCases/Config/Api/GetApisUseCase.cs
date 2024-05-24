using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.UseCases.Config.Api;

public class GetApisUseCase: IGetApisUseCase
{
    private readonly IGetApisService _apisService;

    public GetApisUseCase(IGetApisService apisService)
    {
        _apisService = apisService;
    }

    public Task<ApiEntity?[]> Get(CancellationToken ct)
    {
        return _apisService.Get(ct);
    }
}

public interface IGetApisUseCase
{
    public Task<ApiEntity?[]> Get(CancellationToken ct);
}