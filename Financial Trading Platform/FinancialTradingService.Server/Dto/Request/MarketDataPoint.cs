using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Server.Dto.Request
{
    public class MarketDataPoint
    {
        public DateTime Timestamp { get; set; }
        public string Interval { get; set; }
        public string Symbol { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public long Volume { get; set; }
    }
}

