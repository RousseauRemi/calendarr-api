using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;

public interface IGetJellyseerEntitysService
{
    public Task<JellyseerEntity[]> Get(CancellationToken ct);
}