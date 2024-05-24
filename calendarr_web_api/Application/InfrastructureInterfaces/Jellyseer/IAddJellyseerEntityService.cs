using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;

public interface IAddJellyseerEntityService
{
    public Task Add(JellyseerEntity entity, CancellationToken ct);
}