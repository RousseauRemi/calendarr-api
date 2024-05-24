using calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;
using calendarr_web_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.ApiConfig;

public class UpdateApiConfigEntityService : IUpdateApiConfigEntityService
{
    private readonly CalendarrDbContext _dbContext;

    public UpdateApiConfigEntityService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Update(ApiConfigEntity entity, CancellationToken ct)
    {
        var entityToModified = await _dbContext.ApiConfigEntities.FirstOrDefaultAsync(a => a.Url == entity.Url, ct);
        if (entityToModified == null)
        {
            _dbContext.ApiConfigEntities.RemoveRange(_dbContext.ApiConfigEntities);
            await _dbContext.SaveChangesAsync(ct);
            _dbContext.ApiConfigEntities.Add(entity);
            await _dbContext.SaveChangesAsync(ct);
        }
        else
        {
            entityToModified.Modified(entity);
            _dbContext.ApiConfigEntities.Update(entityToModified);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}