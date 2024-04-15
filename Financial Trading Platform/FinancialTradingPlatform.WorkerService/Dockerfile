# Use a base image that matches your project's requirements
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FinancialTradingPlatform.WorkerService/FinancialTradingPlatform.WorkerService.csproj", "./"]
RUN dotnet restore "FinancialTradingPlatform.WorkerService.csproj"
COPY . .
WORKDIR "/src/FinancialTradingPlatform.WorkerService"
RUN dotnet build "FinancialTradingPlatform.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinancialTradingPlatform.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialTradingPlatform.WorkerService.dll"]
