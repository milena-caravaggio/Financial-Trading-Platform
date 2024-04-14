namespace FinancialTradingPlatform.CrossCutting.DTOs.Requests
{
    public class MarketDataRequest
    {
        public List<MarketDataPointRequest> MarketDataPoints { get; set; } = new List<MarketDataPointRequest>();
    }
}
