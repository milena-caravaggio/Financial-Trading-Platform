using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.Services;

namespace FinancialTradingPlatform.Tests.Services
{
    internal class MACDServiceTests
    {
        private MACDService _macdService;

        [SetUp]
        public void Setup()
        {
            _macdService = new MACDService();
        }

        [Test]
        public void CalculateMACD_Should_Return_Correct_Values()
        {
            // Arrange
            var marketData = GenerateMarketData(40); 

            var expectedMACDValue = 0.0; 
            var expectedSignalValue = 0.0;
            var expectedHistogramValue = 0.0;

            // Act
            var result = _macdService.CalculateMACD(marketData);

            // Assert
            Assert.That(result.Count, Is.AtLeast(1), "Should calculate at least one MACD result.");

            Assert.That(result[0].Line, Is.EqualTo(expectedMACDValue), "The MACD line value is incorrect for the first result.");
            Assert.That(result[0].SignalLine, Is.EqualTo(expectedSignalValue), "The Signal line value is incorrect for the first result.");
            Assert.That(result[0].Histogram, Is.EqualTo(expectedHistogramValue), "The Histogram value is incorrect for the first result.");
        }

        private List<MarketDataPointRequest> GenerateMarketData(int count)
        {
            var data = new List<MarketDataPointRequest>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                data.Add(new MarketDataPointRequest
                {
                    Timestamp = DateTime.Now.AddDays(-count + i),
                    ClosePrice = random.NextDouble() * 100
                });
            }
            return data;
        }
    }
}
