using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;

public interface IUpdateJellyseerEntityService
{
    public Task Update(JellyseerEntity entity, string oldName, CancellationToken ct);
}