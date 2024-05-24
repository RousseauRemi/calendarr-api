using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;
using calendarr_web_api.Domain;
using calendarr_web_api.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.Jellyseer;

public class UpdateJellyseerEntityService : IUpdateJellyseerEntityService
{
    private readonly CalendarrDbContext _dbContext;

    public UpdateJellyseerEntityService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Update(JellyseerEntity entity, string oldName, CancellationToken ct)
    {
        var entityToModified = await _dbContext.JellyseerEntities.FirstOrDefaultAsync(e => e.Name == oldName, ct);
        if (entityToModified != null)
        {
            if (oldName != entity.Name)
            {
                bool isAlreadyExisting = _dbContext.JellyseerEntities.Any(a => a.Name == entity.Name);
                if (isAlreadyExisting)
                    throw new CustomDataException($"The JellyseerEntity with the name {entity.Name} already exist");
                _dbContext.JellyseerEntities.Remove(entityToModified);
                _dbContext.JellyseerEntities.Add(entity);
            }
            else
            {
                entityToModified.Modified(entity);
                _dbContext.JellyseerEntities.Update(entityToModified);
            }
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}