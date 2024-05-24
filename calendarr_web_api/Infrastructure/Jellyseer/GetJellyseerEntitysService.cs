using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;
using calendarr_web_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.Jellyseer;

public class GetJellyseerEntitysService : IGetJellyseerEntitysService
{
    private readonly CalendarrDbContext _dbContext;

    public GetJellyseerEntitysService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<JellyseerEntity[]> Get(CancellationToken ct)
    {
        return await _dbContext.JellyseerEntities.AsNoTracking().ToArrayAsync(ct);
    }
}