using FinancialTradingPlatform.Services;
using FinancialTradingPlatform.Services.Interfaces;
using FinancialTradingPlatform.WorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<TCPWorker>();

builder.Services.AddSingleton<IMACDService, MACDService>();
builder.Services.AddSingleton<ISMAService, SMAService>();
builder.Services.AddSingleton<IMarketAnalysisService, MarketAnalysisService>();

var host = builder.Build();
host.Run();

