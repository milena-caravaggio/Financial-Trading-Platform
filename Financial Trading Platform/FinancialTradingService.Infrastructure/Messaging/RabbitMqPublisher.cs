using FinancialTradingService.Server.Dto.Response;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FinancialTradingService.Infrastructure.Messaging
{
    public class RabbitMqPublisher : IMessagePublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqPublisher(RabbitMqOptions options)
        {
            var factory = new ConnectionFactory()
            {
                HostName = options.HostName,
                Port = options.Port,
                UserName = options.UserName,
                Password = options.Password,
                Ssl = new SslOption
                {
                    Enabled = options.SslEnabled,
                    ServerName = options.ServerName,
                    CertPath = options.CertPath,
                    CertPassphrase = options.CertPassphrase
                }
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "MarketDataExchange", type: ExchangeType.Topic);
            _channel.QueueDeclare(queue: "MarketDataQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "MarketDataQueue", exchange: "MarketDataExchange", routingKey: "market.data");
        }

        public void PublishMarketData(MarketAnalysisResponse marketData)
        {
            var message = JsonSerializer.Serialize(marketData);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "MarketDataExchange",
                                  routingKey: "market.data",
                                  basicProperties: null,
                                  body: body);

            Console.WriteLine(" [x] Sent {0}", message);
        }

        public void Close()
        {
            _connection.Close();
        }
    }
}
