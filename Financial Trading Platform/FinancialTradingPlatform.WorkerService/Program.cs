using FinancialTradingPlatform.Infrastructure.Messaging;
using FinancialTradingPlatform.Infrastructure.Messaging.Interfaces;
using FinancialTradingPlatform.Infrastructure.Messaging.Options;
using FinancialTradingPlatform.Services;
using FinancialTradingPlatform.Services.Interfaces;
using RabbitMQ.Client;

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

var rabbitMqConfig = configuration.GetSection("RabbitMqConfiguration").Get<RabbitMqOptions>();
builder.Services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMqConfiguration"));

builder.Services.AddSingleton<IConnectionFactory>(_ =>
    new ConnectionFactory
    {
        HostName = rabbitMqConfig.HostName,
        Port = rabbitMqConfig.Port,
        UserName = rabbitMqConfig.UserName,
        Password = rabbitMqConfig.Password,
        Ssl = new SslOption
        {
            Enabled = rabbitMqConfig.SslEnabled,
            ServerName = rabbitMqConfig.ServerName,
            CertPath = rabbitMqConfig.CertPath,
            CertPassphrase = rabbitMqConfig.CertPassphrase
        }
    });

builder.Services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();

var host = builder.Build();
host.Run();

