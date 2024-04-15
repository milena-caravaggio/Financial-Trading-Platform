using FinancialTradingPlatform.Services;
using FinancialTradingPlatform.Services.Interfaces;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<TCPWorker>();

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
    .AddEnvironmentVariables();

IConfiguration configuration = configBuilder.Build();
builder.Configuration.AddConfiguration(configuration);

builder.Services.AddSingleton<IMACDService, MACDService>();
builder.Services.AddSingleton<ISMAService, SMAService>();
builder.Services.AddSingleton<IMarketAnalysisService, MarketAnalysisService>();

var host = builder.Build();
host.Run();

