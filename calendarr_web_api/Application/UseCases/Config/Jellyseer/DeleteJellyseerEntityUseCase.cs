using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;

namespace calendarr_web_api.Application.UseCases.Config.Jellyseer;

public class DeleteJellyseerEntityUseCase : IDeleteJellyseerEntityUseCase
{
    private readonly IDeleteJellyseerEntityService _deleteJellyseerEntityService;

    public DeleteJellyseerEntityUseCase(IDeleteJellyseerEntityService deleteJellyseerEntityService)
    {
        _deleteJellyseerEntityService = deleteJellyseerEntityService;
    }

    public Task Delete(string name, CancellationToken ct)
    {
        return _deleteJellyseerEntityService.Delete(name, ct);
    }
}

public interface IDeleteJellyseerEntityUseCase
{
    public Task Delete(string name, CancellationToken ct);
}