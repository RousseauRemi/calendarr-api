using AutoMapper;
using calendarr_web_api.Domain;
using calendarr_web_api.Infrastructure.dtos;

namespace calendarr_web_api.Infrastructure;

/// <summary>
///     Classe pour définir les map
/// </summary>
public class Mappers : Profile
{
    /// <summary>
    ///     Constructeur
    /// </summary>
    public Mappers()
    {
        CreateMap<ApiDto, ApiEntity>().ReverseMap();
        CreateMap<UpdateApiEntityDto, ApiEntity>().ReverseMap();
        CreateMap<JellyseerDto, JellyseerEntity>().ReverseMap();
        CreateMap<UpdatedJellyseerDto, JellyseerEntity>().ReverseMap();
      
        

    }
}