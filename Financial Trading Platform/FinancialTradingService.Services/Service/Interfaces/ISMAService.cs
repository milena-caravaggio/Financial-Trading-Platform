using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.CrossCutting.DTOs.Responses;

namespace FinancialTradingService.Services.Service.Interfaces
{
    public interface ISMAService
    {
        List<SMAResponse> CalculateSMA(List<MarketDataPointRequest> data, int period);
    }
}