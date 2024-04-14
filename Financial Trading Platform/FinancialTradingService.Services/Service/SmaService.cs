using FinancialTradingService.Server.Dto.Request;
using FinancialTradingService.Server.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Server.Service
{
    public class SmaService
    {
        public List<SmaResult> CalculateSma(List<MarketDataPoint> data, int period)
        {
            var smaResults = new List<SmaResult>();
            var prices = data.Select(x => x.ClosePrice).ToList();
            for (int i = 0; i <= prices.Count - period; i++)
            {
                double sum = prices.Skip(i).Take(period).Sum();
                smaResults.Add(new SmaResult { Timestamp = data[i + period - 1].Timestamp, SmaValue = sum / period });
            }
            return smaResults;
        }

    }
}
