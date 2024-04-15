using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.Services.Interfaces;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;
using FinancialTradingPlatform.CrossCutting.DTOs.Responses;

public class TCPWorker : BackgroundService
{
    private readonly TcpListener _listener;
    private readonly IMarketAnalysisService _analysisService;
    private readonly Timer _timer;
    private readonly int _port = 8080;

    public TCPWorker(IMarketAnalysisService analysisService, int intervalMinutes = 1)
    {
        _analysisService = analysisService;

        _listener = new TcpListener(IPAddress.Any, _port);
        _timer = new Timer(SimulateMarketDataProcessing, null, TimeSpan.Zero, TimeSpan.FromMinutes(intervalMinutes));
        
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _listener.Start();
        Console.WriteLine("TCP Worker running on IP " + _listener.LocalEndpoint);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("Connection accepted.");

                _ = Task.Run(() => ProcessConnectionAsync(client, stoppingToken), stoppingToken);

            }
        }
        finally
        {
            _listener.Stop();
        }
    }

    private async Task ProcessConnectionAsync(TcpClient client, CancellationToken stoppingToken)
    {
        var buffer = new byte[1024];
        var stream = client.GetStream();

        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, stoppingToken);
        if (bytesRead == 0)
        {
            client.Close();
            return;
        }

        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine("Received: " + receivedData);

        try
        {
            MarketDataRequest requestData = JsonSerializer.Deserialize<MarketDataRequest>(receivedData);
            MarketAnalysisResponse response = _analysisService.AnalyzeMarketData(requestData);

            string jsonResponse = JsonSerializer.Serialize(response);
            byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
            await stream.WriteAsync(responseBytes, 0, responseBytes.Length, stoppingToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing data: {ex.Message}");

            string errorResponse = "Error processing data.";
            byte[] responseBytes = Encoding.UTF8.GetBytes(errorResponse);
            await stream.WriteAsync(responseBytes, 0, responseBytes.Length, stoppingToken);
        }

        client.Close();
    }

    private void SimulateMarketDataProcessing(object? state = null)
    {
        var requestData = new MarketDataRequest
        {
            MarketDataPoints = new List<MarketDataPointRequest>
            {
                new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-40), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.10, ClosePrice = 28.50, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-39), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.10, ClosePrice = 28.50, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-38), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.50, ClosePrice = 29.00, Volume = 5050000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-37), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.00, ClosePrice = 28.75, Volume = 4950000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-36), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.75, ClosePrice = 28.40, Volume = 4850000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-35), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.10, ClosePrice = 28.50, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-34), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.50, ClosePrice = 29.00, Volume = 5050000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-33), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.00, ClosePrice = 28.75, Volume = 4950000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-32), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.75, ClosePrice = 28.40, Volume = 4850000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-31), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.40, ClosePrice = 28.60, Volume = 4900000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-30), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.60, ClosePrice = 29.05, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-29), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.05, ClosePrice = 29.50, Volume = 5100000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-28), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.10, ClosePrice = 28.50, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-27), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.50, ClosePrice = 29.00, Volume = 5050000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-26), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.00, ClosePrice = 28.75, Volume = 4950000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-25), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.75, ClosePrice = 28.40, Volume = 4850000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-24), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.40, ClosePrice = 28.60, Volume = 4900000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-23), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.60, ClosePrice = 29.05, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-22), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.05, ClosePrice = 29.50, Volume = 5100000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-21), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.10, ClosePrice = 28.50, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-20), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.50, ClosePrice = 29.00, Volume = 5050000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-19), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.00, ClosePrice = 28.75, Volume = 4950000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-18), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.75, ClosePrice = 28.40, Volume = 4850000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-17), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.40, ClosePrice = 28.60, Volume = 4900000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-16), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.60, ClosePrice = 29.05, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-15), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.05, ClosePrice = 29.50, Volume = 5100000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-14), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.10, ClosePrice = 28.50, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-13), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.50, ClosePrice = 29.00, Volume = 5050000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-12), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.00, ClosePrice = 28.75, Volume = 4950000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-11), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.75, ClosePrice = 28.40, Volume = 4850000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-10), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.40, ClosePrice = 28.60, Volume = 4900000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-9), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.60, ClosePrice = 29.05, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-8), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.05, ClosePrice = 29.50, Volume = 5100000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-7), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.10, ClosePrice = 28.50, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-6), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.50, ClosePrice = 29.00, Volume = 5050000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-5), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.00, ClosePrice = 28.75, Volume = 4950000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-4), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.75, ClosePrice = 28.40, Volume = 4850000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-3), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.40, ClosePrice = 28.60, Volume = 4900000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-2), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.60, ClosePrice = 29.05, Volume = 5000000 },
                    new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-1), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.05, ClosePrice = 29.50, Volume = 5100000 }
            }
        };

        var response = _analysisService.AnalyzeMarketData(requestData);
        string jsonResponse = JsonSerializer.Serialize(response);
        Console.WriteLine("Simulated Data Processed: " + jsonResponse);
    }
}
