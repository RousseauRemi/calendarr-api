using calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Infrastructure.ApiConfig;

public class AddApiConfigEntityService : IAddApiConfigEntityService
{
    private readonly CalendarrDbContext _dbContext;

    public AddApiConfigEntityService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(ApiConfigEntity entity, CancellationToken ct)
    {
        await _dbContext.ApiConfigEntities.AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}