using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Domain;

namespace calendarr_web_api.Infrastructure.Api;

public class AddApiService : IAddApiService
{
    private readonly CalendarrDbContext _dbContext;

    public AddApiService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(ApiEntity? entity, CancellationToken ct)
    {
        await _dbContext.ApiEntities.AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}