namespace FinancialTradingService.CrossCutting.DTOs.Requests
{
    public class MarketDataPointRequest
    {
        public DateTime Timestamp { get; set; }

        public string Interval { get; set; } = string.Empty;

        public string Symbol { get; set; } = string.Empty;

        public double OpenPrice { get; set; }

        public double ClosePrice { get; set; }

        public long Volume { get; set; }
    }
}

