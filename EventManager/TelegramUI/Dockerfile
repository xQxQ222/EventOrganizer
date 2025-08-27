FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY TelegramUI/TelegramUI.csproj TelegramUI/
COPY UtilityHolder/UtilityHolder.csproj UtilityHolder/

RUN dotnet restore TelegramUI/TelegramUI.csproj

COPY . .

WORKDIR /src/TelegramUI
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TelegramUI.dll"]
