using FinancialTradingPlatform.CrossCutting.DTOs.Responses;

namespace FinancialTradingPlatform.Infrastructure.Messaging.Interfaces
{
    public interface IMessagePublisher
    {
        void PublishMarketData(MarketAnalysisResponse marketData);

        void Close();
    }
}
