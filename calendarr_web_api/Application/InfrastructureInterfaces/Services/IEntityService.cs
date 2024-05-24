using calendarr_web_api.Domain;
using calendarr_web_api.Infrastructure.dtos;

namespace calendarr_web_api.Application.InfrastructureInterfaces.Services;


public interface IEntityService
{
    /// <summary>
    /// Use for example when you modify an Apientity. The apikey is never send to the client for security
    /// In this fact, if the user don't modify the apikey in the client, we need to se it
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<UpdateApiEntityDto> SetApiKeyIfUnset(UpdateApiEntityDto entity);
    /// <summary>
    /// Use for example when you modify an Apientity. The apikey is never send to the client for security
    /// In this fact, if the user don't modify the apikey in the client, we need to se it
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<UpdatedJellyseerDto> SetApiKeyIfUnset(UpdatedJellyseerDto entity);
}