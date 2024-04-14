using FinancialTradingPlatform.CrossCutting.DTOs.Requests;
using FinancialTradingPlatform.CrossCutting.DTOs.Responses;
using FinancialTradingPlatform.Services.Interfaces;

namespace FinancialTradingPlatform.Services
{
    public class MACDService : IMACDService
    {
        private const int ShortPeriod = 12;
        private const int LongPeriod = 26;
        private const int SignalPeriod = 9;

        public List<MACDResponse> CalculateMACD(List<MarketDataPointRequest> data)
        {
            if (data.Count < LongPeriod)
                throw new InvalidOperationException("Insufficient data for MACD calculation.");

            var prices = data.Select(x => x.ClosePrice).ToList();
            var emaShort = CalculateEma(prices, ShortPeriod);
            var emaLong = CalculateEma(prices, LongPeriod);
            var macdLine = emaShort.Zip(emaLong, (shortEma, longEma) => shortEma - longEma).ToList();
            var signalLine = CalculateEma(macdLine, SignalPeriod);
            var histogram = macdLine.Zip(signalLine, (macd, signal) => macd - signal).ToList();

            var results = new List<MACDResponse>();
            for (int i = SignalPeriod - 1; i < macdLine.Count; i++)
            {
                results.Add(new MACDResponse
                {
                    Timestamp = data[i].Timestamp,
                    Line = macdLine[i],
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
            ema.Add(prices.Take(period).Average());

            for (int i = period; i < prices.Count; i++)
            {
                double currentEma = (prices[i] - ema.Last()) * multiplier + ema.Last();
                ema.Add(currentEma);
            }

            return ema;
        }
    }
}
