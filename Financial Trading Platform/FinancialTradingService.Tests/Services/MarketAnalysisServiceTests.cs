using FinancialTradingService.CrossCutting.DTOs.Requests;
using FinancialTradingService.Server.Dto.Request;
using FinancialTradingService.Server.Service;
using Moq;

namespace FinancialTradingService.Tests.Services
{
    public class MarketAnalysisServiceTests
    {
        private MarketAnalysisService _service;
        private Mock<SMAService> _mockSMAService;
        private Mock<MACDService> _mockMACDService;

        [SetUp]
        public void Setup()
        {
            _mockSMAService = new Mock<SMAService>();
            _mockMACDService = new Mock<MACDService>();
            _service = new MarketAnalysisService(_mockSMAService.Object, _mockMACDService.Object);
        }

        [Test]
        public void AnalyzeMarketData_Should_Call_SMAAndMACDServices()
        {
            // Arrange
            var requestData = new MarketDataRequest { MarketDataPoints = new List<MarketDataPointRequest>() };

            // Act
            var result = _service.AnalyzeMarketData(requestData);

            // Assert
            _mockSMAService.Verify(s => s.CalculateSMA(It.IsAny<List<MarketDataPointRequest>>(), It.IsAny<int>()), Times.Once);
            _mockMACDService.Verify(m => m.CalculateMACD(It.IsAny<List<MarketDataPointRequest>>()), Times.Once);
        }
    }
}
