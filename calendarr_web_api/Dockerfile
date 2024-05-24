#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5557


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["calendarr_web_api/calendarr_web_api.csproj", "calendarr_web_api/"]

RUN dotnet restore "calendarr_web_api/calendarr_web_api.csproj"
COPY . .
WORKDIR "/src/calendarr_web_api"
RUN dotnet build "calendarr_web_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "calendarr_web_api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "calendarr_web_api.dll"]
