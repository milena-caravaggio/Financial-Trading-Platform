namespace FinancialTradingPlatform.CrossCutting.DTOs.Responses
{
    public class MarketAnalysisResponse
    {
        public MarketAnalysisResponse(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; set; }

        public List<SMAResponse> SMAResults { get; set; } = new List<SMAResponse>();
        public List<MACDResponse> MACDResults { get; set; } = new List<MACDResponse>();
    }
}
