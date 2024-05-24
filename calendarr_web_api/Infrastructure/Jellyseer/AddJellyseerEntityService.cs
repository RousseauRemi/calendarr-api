using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Infrastructure.Jellyseer;

public class AddJellyseerEntityService : IAddJellyseerEntityService
{
    private readonly CalendarrDbContext _dbContext;

    public AddJellyseerEntityService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(JellyseerEntity entity, CancellationToken ct)
    {
        await _dbContext.JellyseerEntities.AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}