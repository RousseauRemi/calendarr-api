using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;

public interface IUpdateApiConfigEntityService
{
    public Task Update(ApiConfigEntity entity, CancellationToken ct);
}