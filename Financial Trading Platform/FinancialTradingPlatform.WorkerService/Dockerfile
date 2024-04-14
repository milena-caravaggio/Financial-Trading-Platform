#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
#USER app
#WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS base
USER ContainerAdministrator
RUN net localgroup administrators /add "User Manager\ContainerUser"
USER ContainerUser
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FinancialTradingPlatform.WorkerService/FinancialTradingPlatform.WorkerService.csproj", "FinancialTradingPlatform.WorkerService/"]
RUN dotnet restore "./FinancialTradingPlatform.WorkerService/FinancialTradingPlatform.WorkerService.csproj"
COPY . .
WORKDIR "/src/FinancialTradingPlatform.WorkerService"
RUN dotnet build "./FinancialTradingPlatform.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FinancialTradingPlatform.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialTradingPlatform.WorkerService.dll"]