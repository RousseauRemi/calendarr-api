using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.Api;

public class DeleteApiService: IDeleteApiService
{
    private readonly CalendarrDbContext _dbContext;

    public DeleteApiService(CalendarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Delete(string name, CancellationToken ct)
    {
        if (await _dbContext.ApiEntities.FirstOrDefaultAsync(e => e.Name == name, ct) is { } entityToDelete)
        {
            _dbContext.ApiEntities.Remove(entityToDelete);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}