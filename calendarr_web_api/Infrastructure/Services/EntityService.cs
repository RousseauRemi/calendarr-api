using calendarr_web_api.Application.InfrastructureInterfaces.Services;
using calendarr_web_api.Domain;
using calendarr_web_api.Infrastructure.dtos;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.Services;

public class EntityService: IEntityService
{
    private readonly CalendarrDbContext _dbContext;

    public EntityService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UpdateApiEntityDto> SetApiKeyIfUnset(UpdateApiEntityDto entity)
    {
        if(string.IsNullOrEmpty(entity.ApiKey))
        {
            ApiEntity? entityInbase = await _dbContext.ApiEntities.FirstAsync(a => a!.Name == entity.OldName);
            entity.ApiKey = entityInbase!.ApiKey;
        }

        return entity;

    }

    public async Task<UpdatedJellyseerDto> SetApiKeyIfUnset(UpdatedJellyseerDto entity)
    {
        if(string.IsNullOrEmpty(entity.ApiKey))
        {
            JellyseerEntity entityInbase = await _dbContext.JellyseerEntities.FirstAsync(a => a!.Name == entity.OldName);
            entity.ApiKey = entityInbase.ApiKey;
        }

        return entity;
    }
}
