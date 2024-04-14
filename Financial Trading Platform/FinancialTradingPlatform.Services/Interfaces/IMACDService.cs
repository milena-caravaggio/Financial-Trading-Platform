using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.CrossCutting.DTOs.Responses;

namespace FinancialTradingPlatform.Services.Interfaces
{
    public interface IMACDService
    {
        List<MACDResponse> CalculateMACD(List<MarketDataPointRequest> data);
    }
}