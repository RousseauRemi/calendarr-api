using System.Text.Json.Serialization;
using AutoMapper;
using calendarr_web_api.Application.UseCases.ArrApi;
using calendarr_web_api.Application.UseCases.Config.Api;
using calendarr_web_api.Application.UseCases.Config.ApiConfig;
using calendarr_web_api.Application.UseCases.Config.Jellyseer;
using calendarr_web_api.Application.UseCases.Jellyseer;
using calendarr_web_api.Domain;
using calendarr_web_api.Infrastructure;
using calendarr_web_api.Infrastructure.dtos;
using calendarr_web_api.Infrastructure.Ioc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Mappers));
builder.Services.AddLayers(configuration).Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.MaxDepth = 10000;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddCors(confg =>
    {
        var allowedOrigins = configuration.GetSection("cors:allow").Get<string[]>();
        confg.AddPolicy(
            "CorsPolicy",
            p => p.WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader());
    }
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.MapGet("/ping",
        () => { return true; })
    .WithName("Ping")
    .WithOpenApi();
app.MapGet("/apiEntityWithApiKey",
        async (
            [FromServices] IGetApisUseCase useCase,
            CancellationToken ct) =>
        {
            var apiEntities = await useCase.Get(ct);
            return apiEntities;
        })
    .WithName("GetApiEntityWithApiKey")
    .WithOpenApi();
app.MapGet("/apiEntity",
        async (
            [FromServices] IGetApisUseCase useCase,
            [FromServices] IMapper mapper,
            CancellationToken ct) =>
        {
            var apiEntities = await useCase.Get(ct);
            return apiEntities.Select(a => mapper.Map<ApiDto>(a));
        })
    .WithName("GetApiEntity")
    .WithOpenApi();

app.MapPost("/apiEntity",
        async (
            [FromServices] IAddApiUseCase useCase,
            [FromServices] IMapper mapper,
            [FromBody] ApiEntity? entity, CancellationToken ct) =>
        {
            await useCase.Add(entity, ct);
        })
    .WithName("AddApiEntity")
    .WithOpenApi();
app.MapPut("/apiEntity",
        async ([FromServices] IUpdateApiUseCase useCase, [FromBody] UpdateApiEntityDto entity, CancellationToken ct) =>
        {
            await useCase.Update(entity, ct);
        })
    .WithName("UpdateApiEntity")
    .WithOpenApi();

app.MapDelete("/apiEntity",
        async ([FromServices] IDeleteApiUseCase useCase, [FromBody] string name, CancellationToken ct) =>
        {
            await useCase.Delete(name, ct);
        })
    .WithName("DeleteApiEntity")
    .WithOpenApi();

app.MapGet("/jellyseerEntityWithApiKey",
        async ([FromServices] IGetJellyseerEntitysUseCase useCase, CancellationToken ct) =>
        {
            return await useCase.Get(ct);
        })
    .WithName("GetjellyseerEntityWithApiKey")
    .WithOpenApi();
app.MapGet("/jellyseerEntity",
        async ([FromServices] IGetJellyseerEntitysUseCase useCase,
            [FromServices] IMapper mapper,
            CancellationToken ct) =>
        {
            var results = await useCase.Get(ct);
            return results.Select(r => mapper.Map<JellyseerDto>(r));
        })
    .WithName("GetjellyseerEntity")
    .WithOpenApi();

app.MapPost("/jellyseerEntity",
        async ([FromServices] IAddJellyseerEntityUseCase useCase, [FromBody] JellyseerEntity entity,
            CancellationToken ct) =>
        {
            await useCase.Add(entity, ct);
        })
    .WithName("AddjellyseerEntity")
    .WithOpenApi();
app.MapPut("/jellyseerEntity",
        async ([FromServices] IUpdateJellyseerEntityUseCase useCase, [FromBody] UpdatedJellyseerDto entity,
            CancellationToken ct) =>
        {
            await useCase.Update(entity, ct);
        })
    .WithName("UpdatejellyseerEntity")
    .WithOpenApi();

app.MapDelete("/jellyseerEntity/{name}",
        async ([FromServices] IDeleteJellyseerEntityUseCase useCase, [FromRoute] string name, CancellationToken ct) =>
        {
            await useCase.Delete(name, ct);
        })
    .WithName("DeletejellyseerEntity")
    .WithOpenApi();


app.MapGet("/movies",
        async (
            [FromServices] IGetAllMoviesUseCase useCase,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end,
            CancellationToken ct) =>
        {
            return await useCase.Get(start, end, ct);
        })
    .WithName("GetMovies")
    .WithOpenApi();

app.MapGet("/series",
        async (
            [FromServices] IGetAllSeriesUseCase useCase,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end,
            CancellationToken ct) =>
        {
            return await useCase.Get(start, end, ct);
        })
    .WithName("GetSeries")
    .WithOpenApi();

app.MapGet("/musics",
        async (
            [FromServices] IGetAllMusicsUseCase useCase,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end,
            CancellationToken ct) =>
        {
            return await useCase.Get(start, end, ct);
        })
    .WithName("GetMusics")
    .WithOpenApi();

app.MapGet("/books",
        async (
            [FromServices] IGetAllBooksUseCase useCase,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end,
            CancellationToken ct) =>
        {
            return await useCase.Get(start, end, ct);
        })
    .WithName("GetBooks")
    .WithOpenApi();

app.MapGet("/apiConfigEntity",
        async ([FromServices] IGetApiConfigEntitysUseCase useCase, CancellationToken ct) =>
        {
            return await useCase.Get(ct);
        })
    .WithName("GetApiConfigEntity")
    .WithOpenApi();

app.MapPost("/apiConfigEntity",
        async ([FromServices] IAddApiConfigEntityUseCase useCase, [FromBody] ApiConfigEntity entity,
            CancellationToken ct) =>
        {
            await useCase.Add(entity, ct);
        })
    .WithName("AddApiConfigEntity")
    .WithOpenApi();
app.MapPut("/apiConfigEntity",
        async ([FromServices] IUpdateApiConfigEntityUseCase useCase, [FromBody] ApiConfigEntity entity,
            CancellationToken ct) =>
        {
            await useCase.Update(entity, ct);
        })
    .WithName("UpdateApiConfigEntity")
    .WithOpenApi();

app.MapGet("/checkArrApis",
        async (
            [FromServices] ITestApisUseCase useCase,
            [FromServices] IMapper mapper,
            CancellationToken ct) =>
        {
            var apis = await useCase.CheckApis(ct);
            return apis.ToDictionary(x => x.Key.Name, x => x.Value);
        })
    .WithName("CheckArrApis")
    .WithOpenApi();
app.MapGet("/checkArrApi/{name}",
        async (
            [FromServices] ITestApisUseCase useCase,
            [FromRoute] string name,
            CancellationToken ct) =>
        {
            return await useCase.CheckApi(name, ct);
        })
    .WithName("CheckArrApi")
    .WithOpenApi();
app.MapGet("/checkJellyseers",
        async ([FromServices] ITestJellyseerUseCase useCase,
            [FromServices] IMapper mapper, CancellationToken ct) =>
        {
            var apis = await useCase.CheckAll(ct);
            return apis.ToDictionary(x => x.Key.Name, x => x.Value);
        })
    .WithName("CheckJellyseers")
    .WithOpenApi();
app.MapGet("/CheckJellyseer/{name}",
        async (
            [FromServices] ITestJellyseerUseCase useCase,
            [FromRoute] string name,
            CancellationToken ct) =>
        {
            return await useCase.Check(name, ct);
        })
    .WithName("CheckJellyseer")
    .WithOpenApi();


app.MapGet("/jellyseerEntity/Users",
        async (
            [FromServices] IGetJellyseerUsersUseCase useCase,
            [FromServices] IMapper mapper,
            CancellationToken ct) =>
        {
            var apis = await useCase.Get(ct);
            return apis;
        })
    .WithName("GetUsers")
    .WithOpenApi();

app.MapGet("/jellyseerEntity/{name}/Users/{userId}/requests",
        async (
            [FromServices] IGetJellyseerRequestUseCase useCase,
            string name,
            int userId,
            [FromServices] IMapper mapper,
            CancellationToken ct) =>
        {
            var apis = await useCase.Get(userId, name,  ct);
            return apis;
        })
    .WithName("GetRequestByUserId")
    .WithOpenApi();


app.MapGet("/remote-image",
        async (
            [FromServices] IGetRemoteImage useCase,
            string imageUrl,
            [FromServices] IMapper mapper,
            CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Results.BadRequest("URL is required.");
            }

            try
            {
                var imageBytes =  await useCase.Get(imageUrl,  ct);
                var contentType = "image/jpeg"; // You can determine the actual content type if needed

                return Results.File(imageBytes, contentType);
            }
            catch (OperationCanceledException)
            {
                return Results.StatusCode(499); // Client Closed Request
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return Results.StatusCode(500);
            }
        })
    .WithName("GetRemoteImage")
    .WithOpenApi();


app.MapGet("/arr-image",
        async (
            [FromServices] IGetArrImage useCase,
            string apiName,
            string imageUrl,
            [FromServices] IMapper mapper,
            CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Results.BadRequest("URL is required.");
            }

            try
            {
                var imageBytes =  await useCase.Get(apiName, imageUrl,  ct);
                var contentType = "image/jpeg"; // You can determine the actual content type if needed

                return Results.File(imageBytes, contentType);
            }
            catch (OperationCanceledException)
            {
                return Results.StatusCode(499); // Client Closed Request
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return Results.StatusCode(500);
            }
        })
    .WithName("GetArrImage")
    .WithOpenApi();

app.Run();