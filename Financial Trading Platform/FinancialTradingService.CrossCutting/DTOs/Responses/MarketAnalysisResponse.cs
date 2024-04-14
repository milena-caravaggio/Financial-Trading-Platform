using FinancialTradingService.CrossCutting.DTOs.Responses;

namespace FinancialTradingService.Server.Dto.Response
{
    public class MarketAnalysisResponse
    {
        public string Symbol { get; set; } = string.Empty;

        public List<SMAResponse> SMAResults { get; set; } = new List<SMAResponse>();
        public List<MACDResponse> MACDResults { get; set; } = new List<MACDResponse>();
    }
}
