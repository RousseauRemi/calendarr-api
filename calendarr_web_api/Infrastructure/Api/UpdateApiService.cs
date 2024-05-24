using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Domain;
using calendarr_web_api.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.Api;

public class UpdateApiService: IUpdateApiService
{
    private readonly CalendarrDbContext _dbContext;

    public UpdateApiService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Update(ApiEntity entity, string oldName, CancellationToken ct)
    {
        var entityToModified = await _dbContext.ApiEntities.FirstOrDefaultAsync(e => e.Name == oldName, ct);
        if(entityToModified != null)
        {
            if (oldName != entity.Name)
            {
                bool isAlreadyExisting = _dbContext.ApiEntities.Any(a => a.Name == entity.Name);
                if (isAlreadyExisting)
                    throw new CustomDataException($"The ApiEntity with the name {entity.Name} already exist");
                _dbContext.ApiEntities.Remove(entityToModified);
                _dbContext.ApiEntities.Add(entity);
            }
            else
            {
                entityToModified.Modified(entity);
                _dbContext.ApiEntities.Update(entityToModified);
            }
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}