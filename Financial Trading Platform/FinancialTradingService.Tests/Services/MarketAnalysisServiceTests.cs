using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using FinancialTradingService.Server.Service;
using FinancialTradingService.Server.Dto.Request;

namespace FinancialTradingService.Tests.Services
{
    public class MarketAnalysisServiceTests
    {
        private MarketAnalysisService _service;
        private Mock<SmaService> _mockSmaService;
        private Mock<MacdService> _mockMacdService;

        [SetUp]
        public void Setup()
        {
            _mockSmaService = new Mock<SmaService>();
            _mockMacdService = new Mock<MacdService>();
            _service = new MarketAnalysisService(_mockSmaService.Object, _mockMacdService.Object);
        }

        [Test]
        public void AnalyzeMarketData_Should_Call_SmaAndMacdServices()
        {
            // Arrange
            var requestData = new MarketDataRequest { Data = new List<MarketDataPoint>() };

            // Act
            var result = _service.AnalyzeMarketData(requestData);

            // Assert
            _mockSmaService.Verify(s => s.CalculateSma(It.IsAny<List<MarketDataPoint>>(), It.IsAny<int>()), Times.Once);
            _mockMacdService.Verify(m => m.CalculateMacd(It.IsAny<List<MarketDataPoint>>()), Times.Once);
        }
    }
}
