using AutoMapper;
using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;
using calendarr_web_api.Application.InfrastructureInterfaces.Services;
using calendarr_web_api.Domain;
using calendarr_web_api.Infrastructure.dtos;

namespace calendarr_web_api.Application.UseCases.Config.Jellyseer;

public class UpdateJellyseerEntityUseCase : IUpdateJellyseerEntityUseCase
{
    private readonly IUpdateJellyseerEntityService _updateJellyseerEntityService;
    private readonly IEntityService _entityService;
    private readonly IMapper _mapper;

    public UpdateJellyseerEntityUseCase(IUpdateJellyseerEntityService updateJellyseerEntityService,
        IEntityService entityService, IMapper mapper)
    {
        _updateJellyseerEntityService = updateJellyseerEntityService;
        _entityService = entityService;
        _mapper = mapper;
    }

    public async Task Update(UpdatedJellyseerDto entity, CancellationToken ct)
    {
        entity = await _entityService.SetApiKeyIfUnset(entity);
        var apiEntity = _mapper.Map<JellyseerEntity>(entity);
        await _updateJellyseerEntityService.Update(apiEntity, entity.OldName, ct);
    }
}

public interface IUpdateJellyseerEntityUseCase
{
    public Task Update(UpdatedJellyseerDto entity, CancellationToken ct);
}