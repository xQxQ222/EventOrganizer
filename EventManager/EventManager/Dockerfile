FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем проекты
COPY EventManager/EventManager.csproj EventManager/
COPY ModelHolder/ModelHolder.csproj ModelHolder/
COPY UtilityHolder/UtilityHolder.csproj UtilityHolder/

# Восстанавливаем зависимости
RUN dotnet restore EventManager/EventManager.csproj

# Копируем исходники
COPY . .

WORKDIR /src/EventManager
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EventManager.dll"]
