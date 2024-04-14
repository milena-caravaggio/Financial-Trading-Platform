using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.CrossCutting.DTOs.Responses;
using FinancialTradingPlatform.Services.Interfaces;

namespace FinancialTradingPlatform.Services
{
    public class MarketAnalysisService : IMarketAnalysisService
    {
        private readonly ISMAService _smaService;
        private readonly IMACDService _macdService;

        public MarketAnalysisService(ISMAService smaService, IMACDService macdService)
        {
            _smaService = smaService;
            _macdService = macdService;
        }

        public MarketAnalysisResponse AnalyzeMarketData(MarketDataRequest request)
        {
            var smaResults = _smaService.CalculateSMA(request.MarketDataPoints, 10);
            var macdResults = _macdService.CalculateMACD(request.MarketDataPoints);

            return new MarketAnalysisResponse(request.MarketDataPoints.FirstOrDefault()?.Symbol ?? "N/A")
            {
                SMAResults = smaResults,
                MACDResults = macdResults
            };
        }
    }
}
