using calendarr_web_api.Application.InfrastructureInterfaces.Api;
using calendarr_web_api.Application.InfrastructureInterfaces.ApiConfig;
using calendarr_web_api.Application.InfrastructureInterfaces.Jellyseer;
using calendarr_web_api.Application.InfrastructureInterfaces.Services;
using calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;
using calendarr_web_api.Application.UseCases.ArrApi;
using calendarr_web_api.Application.UseCases.Config.Api;
using calendarr_web_api.Application.UseCases.Config.ApiConfig;
using calendarr_web_api.Application.UseCases.Config.Jellyseer;
using calendarr_web_api.Application.UseCases.Jellyseer;
using calendarr_web_api.Infrastructure.Api;
using calendarr_web_api.Infrastructure.ApiConfig;
using calendarr_web_api.Infrastructure.Jellyseer;
using calendarr_web_api.Infrastructure.Services;
using calendarr_web_api.Infrastructure.WebRepository;
using Microsoft.EntityFrameworkCore;

namespace calendarr_web_api.Infrastructure.Ioc;

public static class IocConfigurationExtension
{
    public static IServiceCollection AddLayers(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext(configuration)
            .AddUseCases()
            .AddServices()
            .AddWebRepository()
            .AddHttpClientIoc();
    }
    
    internal static IServiceCollection AddHttpClientIoc(this IServiceCollection services)
    {
        services.AddHttpClient();

        return services;
    }
    internal static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddTransient<IGetApisUseCase, GetApisUseCase>()
            .AddTransient<IAddApiUseCase, AddApiUseCase>()
            .AddTransient<IUpdateApiUseCase, UpdateApiUseCase>()
            .AddTransient<IDeleteApiUseCase, DeleteApiUseCase>()
            .AddTransient<IGetJellyseerEntitysUseCase, GetJellyseerEntitysUseCase>()
            .AddTransient<IAddJellyseerEntityUseCase, AddJellyseerEntityUseCase>()
            .AddTransient<IUpdateJellyseerEntityUseCase, UpdateJellyseerEntityUseCase>()
            .AddTransient<IDeleteJellyseerEntityUseCase, DeleteJellyseerEntityUseCase>()
            .AddTransient<IGetAllMoviesUseCase, GetAllMoviesUseCase>()
            .AddTransient<IGetAllBooksUseCase, GetAllBooksUseCase>()
            .AddTransient<IGetAllMusicsUseCase, GetAllMusicsUseCase>()
            .AddTransient<IGetAllSeriesUseCase, GetAllSeriesUseCase>()
            .AddTransient<IAddApiConfigEntityUseCase, AddApiConfigEntityUseCase>()
            .AddTransient<IGetApiConfigEntitysUseCase, GetApiConfigEntitysUseCase>()
            .AddTransient<IUpdateApiConfigEntityUseCase, UpdateApiConfigEntityUseCase>()
            .AddTransient<ITestApisUseCase, TestApisUseCase>()
            .AddTransient<ITestJellyseerUseCase, TestJellyseerUseCase>()
            .AddTransient<IGetJellyseerUsersUseCase, GetJellyseerUsersUseCase>()
            .AddTransient<IGetJellyseerRequestUseCase, GetJellyseerRequestUseCase>()
            .AddTransient<IGetArrImage, GetArrImage>()
            .AddTransient<IGetRemoteImage, GetRemoteImage>()
            ;
        return services;
    }
    internal static IServiceCollection AddWebRepository(this IServiceCollection services)
    {
        services.AddTransient<IArrExternalServiceRepository, ArrExternalServiceRepository>()
            .AddTransient<IJellyseerExternalServiceRepository, JellyseerExternalServiceRepository>()
            ;
        return services;
    }
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IGetApisService, GetApisService>()
                .AddTransient<IAddApiService, AddApiService>()
                .AddTransient<IUpdateApiService, UpdateApiService>()
                .AddTransient<IDeleteApiService, DeleteApiService>()
                .AddTransient<IGetJellyseerEntitysService, GetJellyseerEntitysService>()
                .AddTransient<IAddJellyseerEntityService, AddJellyseerEntityService>()
                .AddTransient<IUpdateJellyseerEntityService, UpdateJellyseerEntityService>()
                .AddTransient<IDeleteJellyseerEntityService, DeleteJellyseerEntityService>()
                .AddTransient<IAddApiConfigEntityService, AddApiConfigEntityService>()
                .AddTransient<IGetApiConfigEntityService, GetApiConfigEntityService>()
                .AddTransient<IUpdateApiConfigEntityService, UpdateApiConfigEntityService>()
                .AddTransient<IPropagateApiConfigEntityService, PropagateApiConfigEntityService>()
                .AddTransient<IEntityService, EntityService>()
            ;
        return services;
    }
    internal static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<CalendarrDbContext>(config =>
        {
            config.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            config.EnableSensitiveDataLogging(true);
            config.EnableDetailedErrors();
            config.UseNpgsql(connectionString,
                o =>
                {
                    o.UseNetTopologySuite();
                });
        });
        return services;
    }
}