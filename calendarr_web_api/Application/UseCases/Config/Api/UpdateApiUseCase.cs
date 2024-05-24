using AutoMapper;
using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Application.InfrastructureInterfaces.Services;
using calendarr_web_api.Domain;
using calendarr_web_api.Infrastructure.dtos;

namespace calendarr_web_api.Application.UseCases.Config.Api;

public class UpdateApiUseCase: IUpdateApiUseCase
{
    private readonly IUpdateApiService _updateApiService;
    private readonly IEntityService _entityService;
    private readonly IMapper _mapper;

    public UpdateApiUseCase(
        IUpdateApiService updateApiService,
        IEntityService entityService, IMapper mapper)
    {
        _updateApiService = updateApiService;
        _entityService = entityService;
        _mapper = mapper;
    }

    public async Task Update(UpdateApiEntityDto entity, CancellationToken ct)
    {
        entity = await _entityService.SetApiKeyIfUnset(entity);
        var apiEntity = _mapper.Map<ApiEntity>(entity);
        await _updateApiService.Update(apiEntity, entity.OldName, ct);
    }
}

public interface IUpdateApiUseCase
{
    public Task Update(UpdateApiEntityDto entity, CancellationToken ct);
}