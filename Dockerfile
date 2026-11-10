FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish DynamicChartCase.WebAPI/DynamicChartCase.WebAPI.csproj \
    -c Release -o /out --no-self-contained /p:UseAppHost=false \
    && rm -f /out/appsettings*.json

FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble
WORKDIR /app
RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
COPY --from=build --chown=app:app /out/ .
COPY deploy/appsettings.Production.json ./appsettings.Production.json
ENV ASPNETCORE_HTTP_PORTS=8080 ASPNETCORE_ENVIRONMENT=Production
USER $APP_UID
EXPOSE 8080
HEALTHCHECK --interval=10s --timeout=5s --start-period=15s --retries=6 \
    CMD curl -fsS --max-time 4 -H 'Host: localhost' http://127.0.0.1:8080/healthz || exit 1
ENTRYPOINT ["dotnet", "DynamicChartCase.WebAPI.dll"]
