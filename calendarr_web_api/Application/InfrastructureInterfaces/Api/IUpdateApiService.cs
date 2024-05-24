using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.InfrastructureInterfaces.Api;

public interface IUpdateApiService
{
    public Task Update(ApiEntity entity, string oldName, CancellationToken ct);
}