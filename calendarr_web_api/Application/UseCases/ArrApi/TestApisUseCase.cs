using calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;
using calendarr_web_api.Domain;
using calendarr_web_api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Application.UseCases.Config.Api;

public class TestApisUseCase: ITestApisUseCase
{
    private readonly CalendarrDbContext _dbContext;
    private readonly IArrExternalServiceRepository _arrExternalServiceRepository;

    public TestApisUseCase(
        CalendarrDbContext dbContext,
        IArrExternalServiceRepository arrExternalServiceRepository)
    {
        _dbContext = dbContext;
        _arrExternalServiceRepository = arrExternalServiceRepository;
    }

    public async Task<Dictionary<ApiEntity, bool>> CheckApis(CancellationToken ct)
    {
        Dictionary<ApiEntity, bool> result = new ();
        ApiEntity?[] apis = await _dbContext.ApiEntities.AsNoTracking().ToArrayAsync(ct);
        foreach (ApiEntity? apiEntity in apis)
        {
            bool isOk = false;
            try
            {
                if(!string.IsNullOrWhiteSpace(apiEntity?.Url))
                {
                    isOk = await _arrExternalServiceRepository.TestArrAsync(apiEntity.Url, apiEntity.ApiKey, ct);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                result.Add(apiEntity, isOk);
            }
        }

        return result;
    }

    public async Task<bool> CheckApi(string name, CancellationToken ct)
    {
        ApiEntity? api = await _dbContext.ApiEntities.AsNoTracking().FirstOrDefaultAsync(a => a.Name == name, ct);
        if (api == null)
        {
            return false;
        }

        return await _arrExternalServiceRepository.TestArrAsync(api.Url, api.ApiKey, ct);
    }
}

public interface ITestApisUseCase
{
    public Task<Dictionary<ApiEntity, bool>> CheckApis(CancellationToken ct);
    public Task<bool> CheckApi(string name, CancellationToken ct);
}