using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;

public interface IPropagateApiConfigEntityService
{
    public Task execute(ApiConfigEntity entity, CancellationToken ct);
}