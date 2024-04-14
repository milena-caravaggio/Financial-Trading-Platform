using FinancialTradingService.Server.Dto.Request;
using FinancialTradingService.Server.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Server.Service
{
    public class MarketAnalysisService
    {
        private readonly SmaService _smaService;
        private readonly MacdService _macdService;

        public MarketAnalysisService(SmaService smaService, MacdService macdService)
        {
            _smaService = smaService;
            _macdService = macdService;
        }

        public MarketAnalysisResponse AnalyzeMarketData(MarketDataRequest request)
        {
            var smaResults = _smaService.CalculateSma(request.Data, 10);  // Exemplo com período 10 para SMA
            var macdResults = _macdService.CalculateMacd(request.Data);

            return new MarketAnalysisResponse
            {
                Symbol = request.Data.FirstOrDefault()?.Symbol ?? "N/A",
                SmaResults = smaResults,
                MacdResults = macdResults
            };
        }
    }
}
