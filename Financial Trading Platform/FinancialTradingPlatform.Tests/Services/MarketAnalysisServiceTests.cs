using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.Services;
using FinancialTradingPlatform.Services.Interfaces;
using Moq;

namespace FinancialTradingPlatform.Tests.Services
{
    public class MarketAnalysisServiceTests
    {
        private MarketAnalysisService _service;
        private Mock<ISMAService> _mockSMAService;
        private Mock<IMACDService> _mockMACDService;

        [SetUp]
        public void Setup()
        {
            _mockSMAService = new Mock<ISMAService>();
            _mockMACDService = new Mock<IMACDService>();
            _service = new MarketAnalysisService(_mockSMAService.Object, _mockMACDService.Object);
        }

        [Test]
        public void AnalyzeMarketData_Should_Call_SMAAndMACDServices()
        {
            // Arrange
            var marketDataPoints = new List<MarketDataPointRequest>();
            for (int i = 0; i < 40; i++) 
            {
                marketDataPoints.Add(new MarketDataPointRequest { ClosePrice = 100 + i });
            }
            var requestData = new MarketDataRequest { MarketDataPoints = marketDataPoints };

            // Act
            var result = _service.AnalyzeMarketData(requestData);

            // Assert
            _mockSMAService.Verify(s => s.CalculateSMA(It.IsAny<List<MarketDataPointRequest>>(), It.IsAny<int>()), Times.Once);
            _mockMACDService.Verify(m => m.CalculateMACD(It.IsAny<List<MarketDataPointRequest>>()), Times.Once);
        }
    }
}
