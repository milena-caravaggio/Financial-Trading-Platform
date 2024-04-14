using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Server.Dto.Request
{
    public  class MarketDataRequest
    {
        public List<MarketDataPoint> Data { get; set; }
    }
}
