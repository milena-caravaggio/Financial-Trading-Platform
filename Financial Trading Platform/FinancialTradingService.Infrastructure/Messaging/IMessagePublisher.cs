using FinancialTradingService.Server.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Infrastructure.Messaging
{
    public interface IMessagePublisher
    {
        void PublishMarketData(MarketAnalysisResponse marketData);
        void Close();
    }
}
