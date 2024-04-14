using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.CrossCutting.DTOs.Responses;

namespace FinancialTradingPlatform.Services.Interfaces
{
    public interface ISMAService
    {
        List<SMAResponse> CalculateSMA(List<MarketDataPointRequest> data, int period);
    }
}