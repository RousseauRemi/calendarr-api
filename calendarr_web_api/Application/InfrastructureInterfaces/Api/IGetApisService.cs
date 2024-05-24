using calendarr_web_api.Domain;

namespace calendarr_web_api.Application.InfrastructureInterfaces.Api;

public interface IGetApisService
{
    public Task<ApiEntity?[]> Get(CancellationToken ct);
    public Task<ApiEntity?[]> Get(ApiTypeEnum apiTypeEnum, CancellationToken ct);
    public Task<ApiEntity?[]> GetByName(string name, CancellationToken ct);
}