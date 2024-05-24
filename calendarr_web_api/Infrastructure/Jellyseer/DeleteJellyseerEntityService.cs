using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.Jellyseer;

public class DeleteJellyseerEntityService: IDeleteJellyseerEntityService
{
    private readonly CalendarrDbContext _dbContext;

    public DeleteJellyseerEntityService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Delete(string name, CancellationToken ct)
    {
        
        if (await _dbContext.JellyseerEntities.FirstOrDefaultAsync(e => e.Name == name, ct) is { } entityToDelete)
        {
            _dbContext.JellyseerEntities.Remove(entityToDelete);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}