FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EmailNotificator/EmailNotificator.csproj", "EmailNotificator/"]
RUN dotnet restore "EmailNotificator/EmailNotificator.csproj"
COPY . .
WORKDIR "/src/EmailNotificator"
RUN dotnet build "EmailNotificator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailNotificator.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailNotificator.dll"]
