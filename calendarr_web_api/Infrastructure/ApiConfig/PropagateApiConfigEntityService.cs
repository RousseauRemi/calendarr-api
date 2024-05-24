using calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;
using calendarr_web_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.ApiConfig;

public class PropagateApiConfigEntityService : IPropagateApiConfigEntityService
{
    private readonly IConfiguration _configuration;
    private readonly CalendarrDbContext _dbContext;

    public PropagateApiConfigEntityService(IConfiguration configuration, CalendarrDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    public async Task execute(ApiConfigEntity entity, CancellationToken ct)
    {
        if (entity.ConfigAndDataFromApiEnabled)
        {
            var entitiesToAdd = new List<ApiEntity>();
            PrepareNewApiEntities(entitiesToAdd);
            //We clean the datas
            if (!_dbContext.ApiEntities.AsNoTracking().Any())
            {
                var apis = _dbContext.ApiEntities.AsNoTracking();
                foreach (var api in entitiesToAdd)
                {
                    var foundApi = await apis.FirstOrDefaultAsync(a => a.Url == api.Url);
                    if (foundApi != null)
                    {
                        api.Name = foundApi.Name;
                        api.Color = foundApi.Color;
                    }
                }
                _dbContext.ApiEntities.RemoveRange(apis);
                await _dbContext.SaveChangesAsync(ct);
            
                await _dbContext.ApiEntities.AddRangeAsync(entitiesToAdd, ct);
                await _dbContext.SaveChangesAsync(ct);
            }
            
        }
    }

    private void PrepareNewApiEntities(List<ApiEntity> entitiesToAdd)
    {
        List<string> radarrUrls = _configuration.GetSection("ArrApi:RadarrUrls").Get<List<string>>() ?? new();
        List<string> radarrApiKeys = _configuration.GetSection("ArrApi:RadarrApiKeys").Get<List<string>>()?? new();
        for (var index = 0; index < radarrUrls.Count; index++)
        {
            ApiEntity? apiToAdd = 
                new ApiEntity(
                    $"Radarr_{index.ToString()}",
                    radarrUrls[index],
                    radarrApiKeys[index],
                    ApiTypeEnum.Radarr);
            entitiesToAdd.Add(apiToAdd);
        }

        List<string> sonarrUrls = _configuration.GetSection("ArrApi:SonarrUrls").Get<List<string>>()?? new();
        List<string> sonarrApiKeys = _configuration.GetSection("ArrApi:SonarrApiKeys").Get<List<string>>()?? new();
        for (var index = 0; index < sonarrUrls.Count; index++)
        {
            ApiEntity? apiToAdd = 
                new ApiEntity(
                    $"Sonarr_{index.ToString()}",
                    sonarrUrls[index],
                    sonarrApiKeys[index],
                    ApiTypeEnum.Sonarr);
            entitiesToAdd.Add(apiToAdd);
        }
            
        List<string> readarrUrls = _configuration.GetSection("ArrApi:ReadarrUrls").Get<List<string>>()?? new();
        List<string> readarrApiKeys = _configuration.GetSection("ArrApi:ReadarrApiKeys").Get<List<string>>()?? new();
        for (var index = 0; index < readarrUrls.Count; index++)
        {
            ApiEntity? apiToAdd = 
                new ApiEntity(
                    $"Readarr_{index.ToString()}",
                    readarrUrls[index],
                    readarrApiKeys[index],
                    ApiTypeEnum.Readarr);
            entitiesToAdd.Add(apiToAdd);
        }
            
        List<string> lidarrUrls = _configuration.GetSection("ArrApi:LidarrUrls").Get<List<string>>()?? new();
        List<string> lidarrApiKeys = _configuration.GetSection("ArrApi:LidarrApiKeys").Get<List<string>>()?? new();
        for (var index = 0; index < lidarrUrls.Count; index++)
        {
            ApiEntity? apiToAdd = 
                new ApiEntity(
                    $"Lidarr_{index.ToString()}",
                    lidarrUrls[index],
                    lidarrApiKeys[index],
                    ApiTypeEnum.Lidarr);
            entitiesToAdd.Add(apiToAdd);
        }
    }
}