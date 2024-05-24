using calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.UseCases.Config.ApiConfig;

public class AddApiConfigEntityUseCase : IAddApiConfigEntityUseCase
{
    private readonly IAddApiConfigEntityService _addApiConfigEntityService;
    private readonly IPropagateApiConfigEntityService _propagateApiConfigEntityService;

    public AddApiConfigEntityUseCase(
        IAddApiConfigEntityService addApiConfigEntityService,
        IPropagateApiConfigEntityService propagateApiConfigEntityService)
    {
        _addApiConfigEntityService = addApiConfigEntityService;
        _propagateApiConfigEntityService = propagateApiConfigEntityService;
    }

    public async Task  Add(ApiConfigEntity entity, CancellationToken ct)
    {
        await _addApiConfigEntityService.Add(entity, ct);
        if(entity.ConfigFromApiEnabled)
        {
            await _propagateApiConfigEntityService.execute(entity, ct);
        }
    }
}

public interface IAddApiConfigEntityUseCase
{
    public Task Add(ApiConfigEntity entity, CancellationToken ct);
}