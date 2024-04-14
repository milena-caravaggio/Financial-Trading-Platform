using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.CrossCutting.DTOs.Responses;

namespace FinancialTradingService.Services.Service.Interfaces
{
    public interface IMACDService
    {
        List<MACDResponse> CalculateMACD(List<MarketDataPointRequest> data);
    }
}