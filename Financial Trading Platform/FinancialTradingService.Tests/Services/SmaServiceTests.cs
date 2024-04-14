using NUnit.Framework;
using System.Collections.Generic;
using System;
using FinancialTradingService.Server;
using FinancialTradingService.Server.Service;
using FinancialTradingService.Server.Dto.Request;

namespace FinancialTradingService.Tests.Services
{
    public class SmaServiceTests
    {
        private SmaService _smaService;

        [SetUp]
        public void Setup()
        {
            _smaService = new SmaService();
        }

        [Test]
        public void CalculateSma_Should_Return_Correct_Values()
        {
            // Arrange
            var marketData = new List<MarketDataPoint>
        {
            new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-9), ClosePrice = 1 },
            new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-8), ClosePrice = 2 },
            new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-7), ClosePrice = 3 },
            new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-6), ClosePrice = 4 },
            new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-5), ClosePrice = 5 },
            new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-4), ClosePrice = 6 },
            new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-3), ClosePrice = 7 },
            new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-2), ClosePrice = 8 },
            new MarketDataPoint { Timestamp = DateTime.Now.AddDays(-1), ClosePrice = 9 },
            new MarketDataPoint { Timestamp = DateTime.Now, ClosePrice = 10 }
        };
            int period = 5;

            // Act
            var result = _smaService.CalculateSma(marketData, period);

            // Assert
            Assert.AreEqual(3, result.Count, "Should calculate correct number of SMA values");
            Assert.AreEqual(3.0, result[0].SmaValue, "The first SMA value is incorrect");
            Assert.AreEqual(4.0, result[1].SmaValue, "The second SMA value is incorrect");
            Assert.AreEqual(5.0, result[2].SmaValue, "The third SMA value is incorrect");
        }

        [Test]
        public void CalculateSma_WithEmptyData_Should_Return_EmptyResult()
        {
            // Arrange
            var marketData = new List<MarketDataPoint>();
            int period = 5;

            // Act
            var result = _smaService.CalculateSma(marketData, period);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsEmpty(result, "Result should be an empty list for empty input data");
        }
    }
}

