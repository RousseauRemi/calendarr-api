using calendarr_web_api.Application.InfrastructureInterfaces.Api;

namespace calendarr_web_api.Application.UseCases.Config.Api;

public class DeleteApiUseCase : IDeleteApiUseCase
{
    private readonly IDeleteApiService _deleteApiService;

    public DeleteApiUseCase(IDeleteApiService deleteApiService)
    {
        _deleteApiService = deleteApiService;
    }

    public Task Delete(string name, CancellationToken ct)
    {
        return _deleteApiService.Delete(name, ct);
    }
}

public interface IDeleteApiUseCase
{
    public Task Delete(string name, CancellationToken ct);
}