using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingService.Server.Dto.Response;
using FinancialTradingService.Services.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Server.Service
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
            var smaResults = _smaService.CalculateSMA(request.MarketDataPoints, 10);  // Exemplo com período 10 para SMA
            var macdResults = _macdService.CalculateMACD(request.MarketDataPoints);

            return new MarketAnalysisResponse
            {
                Symbol = request.MarketDataPoints.FirstOrDefault()?.Symbol ?? "N/A",
                SMAResults = smaResults,
                MACDResults = macdResults
            };
        }
    }
}
