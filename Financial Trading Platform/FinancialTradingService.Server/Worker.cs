using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Financial_Trading_Platform
{
    public class TcpWorker : BackgroundService
    {
        private readonly TcpListener _listener;

        public TcpWorker()
        {
            _listener = new TcpListener(IPAddress.Loopback, 8080);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener.Start();
            Console.WriteLine("Worker rodando no IP " + _listener.LocalEndpoint);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    Console.WriteLine("Conexão aceita.");

                    _ = ProcessConnectionAsync(client, stoppingToken);
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

            while (!stoppingToken.IsCancellationRequested)
            {
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, stoppingToken);
                if (bytesRead == 0) break;

                var request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Recebido: " + request);

                await stream.WriteAsync(buffer, 0, bytesRead, stoppingToken);
            }

            client.Close();
        }
    }
}
