namespace calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;

public interface IDeleteJellyseerEntityService
{
    public Task Delete(string name, CancellationToken ct);
}