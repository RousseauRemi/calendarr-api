using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.UseCases.Config.Api;

public class AddApiUseCase : IAddApiUseCase
{
    private readonly IAddApiService _addApiService; 
    public AddApiUseCase(IAddApiService addApiService)
    {
        this._addApiService = addApiService;
    }
    
    public Task Add( ApiEntity? entity, CancellationToken ct)
    {
        return _addApiService.Add(entity, ct);
    }
}

public interface IAddApiUseCase
{
    public Task Add(ApiEntity? entity, CancellationToken ct);
}