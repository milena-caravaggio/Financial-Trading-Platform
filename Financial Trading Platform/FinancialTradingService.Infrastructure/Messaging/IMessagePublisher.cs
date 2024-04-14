using FinancialTradingService.Server.Dto.Response;

namespace FinancialTradingService.Infrastructure.Messaging
{
    public interface IMessagePublisher
    {
        void PublishMarketData(MarketAnalysisResponse marketData);

        void Close();
    }
}
