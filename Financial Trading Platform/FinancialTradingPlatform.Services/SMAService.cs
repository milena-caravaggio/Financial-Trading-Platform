using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.CrossCutting.DTOs.Responses;
using FinancialTradingPlatform.Services.Interfaces;

namespace FinancialTradingPlatform.Services
{
    public class SMAService : ISMAService
    {
        public List<SMAResponse> CalculateSMA(List<MarketDataPointRequest> data, int period)
        {
            var smas = new List<SMAResponse>();
            var prices = data.Select(x => x.ClosePrice).ToList();
            for (int i = 0; i <= prices.Count - period; i++)
            {
                double sum = prices.Skip(i).Take(period).Sum();
                smas.Add(new SMAResponse { Timestamp = data[i + period - 1].Timestamp, SMAValue = sum / period });
            }
            return smas;
        }

    }
}
