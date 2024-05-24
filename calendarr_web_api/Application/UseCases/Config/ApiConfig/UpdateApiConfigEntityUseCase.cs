using calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.UseCases.Config.ApiConfig;

public class UpdateApiConfigEntityUseCase : IUpdateApiConfigEntityUseCase
{
    private readonly IUpdateApiConfigEntityService _updateApiConfigEntityService;
    private readonly IPropagateApiConfigEntityService _propagateApiConfigEntityService;

    public UpdateApiConfigEntityUseCase(IUpdateApiConfigEntityService updateApiConfigEntityService,
        IPropagateApiConfigEntityService propagateApiConfigEntityService)
    {
        _updateApiConfigEntityService = updateApiConfigEntityService;
        _propagateApiConfigEntityService = propagateApiConfigEntityService;
    }

    public async Task Update(ApiConfigEntity entity, CancellationToken ct)
    {
        await _updateApiConfigEntityService.Update(entity, ct);
        if (entity.ConfigFromApiEnabled)
        {
            await _propagateApiConfigEntityService.execute(entity, ct);
        }
    }
}

public interface IUpdateApiConfigEntityUseCase
{
    public Task Update(ApiConfigEntity entity, CancellationToken ct);
}