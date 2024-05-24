using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.InfrastructureInterfaces.Api;

public interface IAddApiService
{
    public Task Add(ApiEntity? entity, CancellationToken ct);
}