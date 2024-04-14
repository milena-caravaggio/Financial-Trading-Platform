using System.Net.Sockets;
using System.Net;
using System.Text;

namespace FinancialTradingPlatform.WorkerService
{
    public class TCPWorker : BackgroundService
    {
        private readonly TcpListener _listener;

        public TCPWorker()
        {
            _listener = new TcpListener(IPAddress.Loopback, 8080);
        }

        public TCPWorker(int port)
        {
            _listener = new TcpListener(IPAddress.Loopback, port);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener.Start();
            Console.WriteLine("TCP Worker running on IP " + _listener.LocalEndpoint);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    Console.WriteLine("Connection accepted.");

                    // Directly call the method here if you want to keep this structure
                    await HandleClient(stoppingToken);
                }
            }
            finally
            {
                _listener.Stop();
            }
        }

        public async Task HandleClient(CancellationToken stoppingToken)
        {
            await ExecuteAsync(stoppingToken);
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
                Console.WriteLine("Received: " + request);

                await stream.WriteAsync(buffer, 0, bytesRead, stoppingToken);
            }

            client.Close();
        }
    }
}
