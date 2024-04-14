using Financial_Trading_Platform;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<TcpWorker>();

//builder.Services.AddSingleton<IMACDService, MACDService>();
//builder.Services.AddSingleton<ISMAService, SMAService>();
//builder.Services.AddSingleton<IMarketAnalysisService, MarketAnalysisService>();


var host = builder.Build();
host.Run();
