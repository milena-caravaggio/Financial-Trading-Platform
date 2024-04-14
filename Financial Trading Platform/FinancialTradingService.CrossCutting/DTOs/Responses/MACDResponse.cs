namespace FinancialTradingService.CrossCutting.DTOs.Responses
{
    public class MACDResponse
    {
        public DateTime Timestamp { get; set; }

        public double Line { get; set; }

        public double SignalLine { get; set; }

        public double Histogram { get; set; }
    }
}
