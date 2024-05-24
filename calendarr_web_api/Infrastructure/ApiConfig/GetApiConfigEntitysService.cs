using calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;
using calendarr_web_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.ApiConfig;

public class GetApiConfigEntityService : IGetApiConfigEntityService
{
    private readonly CalendarrDbContext _dbContext;

    public GetApiConfigEntityService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApiConfigEntity?> Get(CancellationToken ct)
    {
        return await _dbContext.ApiConfigEntities.AsNoTracking().FirstOrDefaultAsync(ct);
    }
}