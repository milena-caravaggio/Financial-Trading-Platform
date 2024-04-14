using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.CrossCutting.Language;
using FinancialTradingPlatform.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
            var marketData = GenerateMarketData();

            // Ensure there is enough data
            Assert.IsTrue(marketData.Count >= 26, LanguageResource.InsufficientData);

            var expectedMACDValue = -7;
            var expectedSignalValue = -7;
            var expectedHistogramValue = 0;

            // Act
            var result = _macdService.CalculateMACD(marketData);

            // Assert
            Assert.That(result.Count, Is.AtLeast(7), LanguageResource.ShouldCalculate);
            Assert.That(result[0].Line, Is.EqualTo(expectedMACDValue), LanguageResource.MACDValueIncorrect);
            Assert.That(result[0].SignalLine, Is.EqualTo(expectedSignalValue), LanguageResource.SignalValueIncorrect);
            Assert.That(result[0].Histogram, Is.EqualTo(expectedHistogramValue), LanguageResource.HistogramValueIncorrect);
        }

        private List<MarketDataPointRequest> GenerateMarketData()
        {
            var data = new List<MarketDataPointRequest>();
            // Add sufficient data points
            for (int i = 0; i < 40; i++)
            {
                data.Add(new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-i), ClosePrice = 50 + i });
            }
            return data;
        }
    }
}
