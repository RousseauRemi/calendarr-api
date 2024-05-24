using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.UseCases.Config.Jellyseer;

public class AddJellyseerEntityUseCase : IAddJellyseerEntityUseCase
{
    private readonly IAddJellyseerEntityService _addJellyseerEntityService;

    public AddJellyseerEntityUseCase(IAddJellyseerEntityService addJellyseerEntityService)
    {
        _addJellyseerEntityService = addJellyseerEntityService;
    }

    public Task Add(JellyseerEntity entity, CancellationToken ct)
    {
        return _addJellyseerEntityService.Add(entity, ct);
    }
}

public interface IAddJellyseerEntityUseCase
{
    public Task Add(JellyseerEntity entity, CancellationToken ct);
}