using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;

public interface IGetApiConfigEntityService
{
    public Task<ApiConfigEntity?> Get(CancellationToken ct);
}