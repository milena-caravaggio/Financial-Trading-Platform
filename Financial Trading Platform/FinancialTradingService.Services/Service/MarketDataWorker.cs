using FinancialTradingService.Server.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinancialTradingService.Server.Service
{
    internal class MarketDataWorker
    {
        private readonly int _port;
        private readonly MarketAnalysisService _analysisService;
        private readonly Timer _timer;

        public MarketDataWorker(int port, MarketAnalysisService analysisService, int intervalMinutes)
        {
            _port = port;
            _analysisService = analysisService;
            _timer = new Timer(ProcessData, null, TimeSpan.Zero, TimeSpan.FromMinutes(intervalMinutes));
        }

        private void ProcessData(object state)
        {
            // Simulação de recebimento de dados
            // Implemente sua lógica de obter os dados reais aqui
            var requestData = new MarketDataRequest
            {
                Data = new List<MarketDataPoint>
                {
                    new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-7), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.10, ClosePrice = 28.50, Volume = 5000000 },
                    new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-6), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.50, ClosePrice = 29.00, Volume = 5050000 },
                    new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-5), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.00, ClosePrice = 28.75, Volume = 4950000 },
                    new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-4), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.75, ClosePrice = 28.40, Volume = 4850000 },
                    new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-3), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.40, ClosePrice = 28.60, Volume = 4900000 },
                    new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-2), Interval = "1d", Symbol = "PETR4", OpenPrice = 28.60, ClosePrice = 29.05, Volume = 5000000 },
                    new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-1), Interval = "1d", Symbol = "PETR4", OpenPrice = 29.05, ClosePrice = 29.50, Volume = 5100000 }

                }
            };

            var response = _analysisService.AnalyzeMarketData(requestData);
            string jsonResponse = JsonSerializer.Serialize(response);

            // Lógica para enviar dados de volta ao cliente
            // A implementação exata dependerá do seu caso de uso específico
            Console.WriteLine(jsonResponse);
        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => HandleClient(client));
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // Processar os dados recebidos e responder
            // Assumindo que os dados recebidos estão no formato JSON apropriado para MarketDataRequest
            var requestData = JsonSerializer.Deserialize<MarketDataRequest>(dataReceived);
            var response = _analysisService.AnalyzeMarketData(requestData);
            string jsonResponse = JsonSerializer.Serialize(response);

            byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);

            client.Close();
        }
    }
}
