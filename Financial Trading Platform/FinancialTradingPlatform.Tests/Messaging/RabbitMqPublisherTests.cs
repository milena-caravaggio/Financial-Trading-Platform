using FinancialTradingPlatform.CrossCutting.DTOs.Responses;
using FinancialTradingPlatform.CrossCutting.Language;
using FinancialTradingPlatform.Infrastructure.Messaging;
using FinancialTradingPlatform.Infrastructure.Messaging.Options;
using Moq;
using RabbitMQ.Client;


namespace FinancialTradingPlatform.Tests.Messaging
{
    public class RabbitMqPublisherTests
    {
        private const string SYMBOL = "Stock Market Value Test";
        private RabbitMqPublisher _publisher;
        private Mock<IModel>? _mockChannel;
        private Mock<IConnection>? _mockConnection;


        [SetUp]
        public void Setup()
        {
            var mockConnectionFactory = new Mock<IConnectionFactory>();
            _mockConnection = new Mock<IConnection>();
            _mockChannel = new Mock<IModel>();

            mockConnectionFactory.Setup(x => x.CreateConnection()).Returns(_mockConnection.Object);
            _mockConnection.Setup(x => x.CreateModel()).Returns(_mockChannel.Object);

            _publisher = new RabbitMqPublisher(mockConnectionFactory.Object); 
        }

        [Test]
        public void PublishMarketData_Should_Publish_Correct_Data()
        {
            // Arrange
            var marketData = new MarketAnalysisResponse(SYMBOL);

            // Act
            _publisher.PublishMarketData(marketData);

            // Assert
            _mockChannel?.Verify(m => m.BasicPublish(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<IBasicProperties>(),
                It.IsAny<ReadOnlyMemory<Byte>>()), Times.Once);
        }
    }
}
