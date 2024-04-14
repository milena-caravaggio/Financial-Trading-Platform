using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.CrossCutting.Language;
using FinancialTradingPlatform.Services;

namespace FinancialTradingPlatform.Tests.Services
{
    public class SMAServiceTests
    {
        private SMAService _smaService;

        [SetUp]
        public void Setup()
        {
            _smaService = new SMAService();
        }

        [Test]
        public void CalculateSMA_Should_Return_Correct_Values()
        {
            // Arrange
            var marketData = new List<MarketDataPointRequest>
        {
            new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-9), ClosePrice = 1 },
            new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-8), ClosePrice = 2 },
            new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-7), ClosePrice = 3 },
            new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-6), ClosePrice = 4 },
            new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-5), ClosePrice = 5 },
            new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-4), ClosePrice = 6 },
            new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-3), ClosePrice = 7 },
            new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-2), ClosePrice = 8 },
            new MarketDataPointRequest { Timestamp = DateTime.Now.AddDays(-1), ClosePrice = 9 },
            new MarketDataPointRequest { Timestamp = DateTime.Now, ClosePrice = 10 }
        };
            int period = 5;

            // Act
            var result = _smaService.CalculateSMA(marketData, period);

            // Assert
            Assert.AreEqual(6, result.Count, LanguageResource.CalculateCorrectSMA);
            Assert.AreEqual(3.0, result[0].SMAValue, LanguageResource.FirstSMAIncorrect);
            Assert.AreEqual(4.0, result[1].SMAValue, LanguageResource.SecondSMAIncorrec);
            Assert.AreEqual(5.0, result[2].SMAValue, LanguageResource.ThirdSMAIncorrect);
        }

        [Test]
        public void CalculateSMA_WithEmptyData_Should_Return_EmptyResult()
        {
            // Arrange
            var marketData = new List<MarketDataPointRequest>();
            int period = 5;

            // Act
            var result = _smaService.CalculateSMA(marketData, period);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsEmpty(result, "Result should be an empty list for empty input data");
        }
    }
}

