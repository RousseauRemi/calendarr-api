namespace calendarr_web_api.Application.InfrastructureInterfaces.Api;

public interface IDeleteApiService
{
    public Task Delete(string name, CancellationToken ct);
}