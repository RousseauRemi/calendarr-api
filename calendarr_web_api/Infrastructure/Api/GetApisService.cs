using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.Api;

public class GetApisService: IGetApisService
{
    private readonly CalendarrDbContext _dbContext;

    public GetApisService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApiEntity?[]> Get(CancellationToken ct)
    {
        return await _dbContext.ApiEntities.AsNoTracking().ToArrayAsync(ct);
    }

    public async Task<ApiEntity?[]> Get(ApiTypeEnum apiTypeEnum, CancellationToken ct)
    {
        return await _dbContext.ApiEntities.AsNoTracking().Where(a => apiTypeEnum == a.ApiType).ToArrayAsync(ct);
    }

    public async Task<ApiEntity?[]> GetByName(string name, CancellationToken ct)
    {
        return await _dbContext.ApiEntities.AsNoTracking().Where(a => name == a.Name).ToArrayAsync(ct);
    }
}