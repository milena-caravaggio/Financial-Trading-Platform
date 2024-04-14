using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Server.Dto.Response
{
    public class MarketAnalysisResponse
    {
        public string Symbol { get; set; }
        public List<SmaResult> SmaResults { get; set; }
        public List<MacdResult> MacdResults { get; set; }
    }

    public class SmaResult
    {
        public DateTime Timestamp { get; set; }
        public double SmaValue { get; set; }
    }

    public class MacdResult
    {
        public DateTime Timestamp { get; set; }
        public double MacdLine { get; set; }
        public double SignalLine { get; set; }
        public double Histogram { get; set; }
    }
}
