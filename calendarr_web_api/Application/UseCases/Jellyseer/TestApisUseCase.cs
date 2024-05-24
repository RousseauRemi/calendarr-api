using calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;
using calendarr_web_api.Domain;
using calendarr_web_api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Application.UseCases.Jellyseer;

public class TestJellyseerUseCase: ITestJellyseerUseCase
{
    private readonly CalendarrDbContext _dbContext;
    private readonly IJellyseerExternalServiceRepository _jellyseerExternalServiceRepository;

    public TestJellyseerUseCase(
        CalendarrDbContext dbContext,
        IJellyseerExternalServiceRepository jellyseerExternalServiceRepository)
    {
        _dbContext = dbContext;
        _jellyseerExternalServiceRepository = jellyseerExternalServiceRepository;
    }
    public async Task<Dictionary<JellyseerEntity, bool>> CheckAll(CancellationToken ct)
    {
        Dictionary<JellyseerEntity, bool> result = new ();
        JellyseerEntity?[] apis = await _dbContext.JellyseerEntities.AsNoTracking().ToArrayAsync(ct);
        foreach (JellyseerEntity? apiEntity in apis)
        {
            if(apiEntity != null)
            {
                bool isOk = await _jellyseerExternalServiceRepository.TestJellyseerAsync(apiEntity.Url, ct);
                result.Add(apiEntity, isOk);
            }
        }

        return result;
    }
    public async Task<bool> Check(string name, CancellationToken ct)
    {
        JellyseerEntity? api = 
            await _dbContext.JellyseerEntities.AsNoTracking().FirstOrDefaultAsync(j => j.Name == name, ct);
        if (api == null)
        {
            return false;
        }

        return await _jellyseerExternalServiceRepository.TestJellyseerAsync(api.Url, ct);
    }
}

public interface ITestJellyseerUseCase
{
    Task<Dictionary<JellyseerEntity, bool>> CheckAll(CancellationToken ct);
    public Task<bool> Check(string name, CancellationToken ct);
}