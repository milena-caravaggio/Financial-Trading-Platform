using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Server.Dto.Response
{
    public class MacdResponse
    {
        public List<double> MacdLine { get; set; }
        public List<double> SignalLine { get; set; }
        public List<double> Histogram { get; set; }
    }
}
