using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using RabbitMQ.Client;
using FinancialTradingService.Infrastructure.Messaging;
using System.Text;
using System.Text.Json;
using FinancialTradingService.Server.Dto.Response;


namespace FinancialTradingService.Tests.Messaging
{
    public class RabbitMqPublisherTests
    {
        private RabbitMqPublisher _publisher;
        private Mock<IModel> _mockChannel;
        private Mock<IConnection> _mockConnection;

        [SetUp]
        public void Setup()
        {
            var mockConnectionFactory = new Mock<IConnectionFactory>();
            _mockConnection = new Mock<IConnection>();
            _mockChannel = new Mock<IModel>();

            mockConnectionFactory.Setup(x => x.CreateConnection()).Returns(_mockConnection.Object);
            _mockConnection.Setup(x => x.CreateModel()).Returns(_mockChannel.Object);

            var options = new RabbitMqOptions
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "user",
                Password = "password"
            };

            _publisher = new RabbitMqPublisher(options);
        }

        [Test]
        public void PublishMarketData_Should_Publish_Correct_Data()
        {
            // Arrange
            var marketData = new MarketAnalysisResponse { Symbol = "TEST" };

            // Act
            _publisher.PublishMarketData(marketData);

            // Assert
            _mockChannel.Verify(m => m.BasicPublish(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<IBasicProperties>(),
                It.IsAny<byte[]>()), Times.Once);
        }

        [Test]
        public void PublishMarketData_WhenConnectionFails_ShouldRetryOrLogError()
        {
            // Arrange
            _mockConnection.Setup(x => x.CreateModel()).Throws(new Exception("Connection failed"));

            // Act & Assert
            Assert.Throws<Exception>(() => _publisher.PublishMarketData(new MarketAnalysisResponse()));
            // Verificar se o erro foi logado ou se houve tentativa de reconexão, conforme a lógica implementada
        }
    }
}
