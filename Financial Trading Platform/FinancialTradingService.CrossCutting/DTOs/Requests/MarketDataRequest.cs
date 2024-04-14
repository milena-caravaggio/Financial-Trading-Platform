using FinancialTradingService.CrossCutting.DTOs.Requests;

namespace FinancialTradingService.Server.Dto.Request
{
    public  class MarketDataRequest
    {
        public List<MarketDataPointRequest> MarketDataPoints { get; set; } = new List<MarketDataPointRequest>();
    }
}
