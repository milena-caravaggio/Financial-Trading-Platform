using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.CrossCutting.DTOs.Responses;

namespace FinancialTradingPlatform.Services.Interfaces
{
    public interface IMarketAnalysisService
    {
        MarketAnalysisResponse AnalyzeMarketData(MarketDataRequest request);
    }
}