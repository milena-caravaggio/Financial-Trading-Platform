using FinancialTradingService.Server.Dto.Request;
using FinancialTradingService.Server.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Server.Service
{
    public class MacdService
    {
        private const int ShortPeriod = 12;
        private const int LongPeriod = 26;
        private const int SignalPeriod = 9;

        public List<MacdResult> CalculateMacd(List<MarketDataPoint> data)
        {
            if (data.Count < LongPeriod)
                throw new InvalidOperationException("Insufficient data for MACD calculation.");

            var prices = data.Select(x => x.ClosePrice).ToList();
            var emaShort = CalculateEma(prices, ShortPeriod);
            var emaLong = CalculateEma(prices, LongPeriod);
            var macdLine = emaShort.Zip(emaLong, (shortEma, longEma) => shortEma - longEma).ToList();
            var signalLine = CalculateEma(macdLine, SignalPeriod);
            var histogram = macdLine.Zip(signalLine, (macd, signal) => macd - signal).ToList();

            var results = new List<MacdResult>();
            for (int i = SignalPeriod - 1; i < macdLine.Count; i++)
            {
                results.Add(new MacdResult
                {
                    Timestamp = data[i].Timestamp,
                    MacdLine = macdLine[i],
                    SignalLine = signalLine[i - SignalPeriod + 1],
                    Histogram = histogram[i - SignalPeriod + 1]
                });
            }

            return results;
        }

        private List<double> CalculateEma(List<double> prices, int period)
        {
            if (prices.Count < period)
                throw new InvalidOperationException("Insufficient data for EMA calculation.");

            var ema = new List<double>();
            double multiplier = 2.0 / (period + 1);
            ema.Add(prices.Take(period).Average());  // Initial SMA as the first EMA

            for (int i = period; i < prices.Count; i++)
            {
                double currentEma = (prices[i] - ema.Last()) * multiplier + ema.Last();
                ema.Add(currentEma);
            }

            return ema;
        }
    }
}
