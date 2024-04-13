using Financial_Trading_Platform;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<TcpWorker>();
    })
    .Build();

await host.RunAsync();

