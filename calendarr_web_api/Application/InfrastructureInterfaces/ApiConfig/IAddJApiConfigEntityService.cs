using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;

public interface IAddApiConfigEntityService
{
    public Task Add(ApiConfigEntity entity, CancellationToken ct);
}