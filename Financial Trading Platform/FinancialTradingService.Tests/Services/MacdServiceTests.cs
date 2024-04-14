using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using FinancialTradingService.Server.Service;
using FinancialTradingService.Server.Dto.Request;

namespace FinancialTradingService.Tests.Services
{
    internal class MacdServiceTests
    {
        private MacdService _macdService;

        [SetUp]
        public void Setup()
        {
            _macdService = new MacdService();
        }

        [Test]
        public void CalculateMacd_Should_Return_Correct_Values()
        {
            // Arrange
            var marketData = GenerateMarketData(40); // Gera dados de mercado suficientes para o cálculo do MACD
            
            // Valores esperados conhecidos ou calculados para o conjunto de dados fornecido
            var expectedMacdValue = 0.0; // Substitua por um valor real esperado
            var expectedSignalValue = 0.0; // Substitua por um valor real esperado
            var expectedHistogramValue = 0.0; // Substitua por um valor real esperado

            // Act
            var result = _macdService.CalculateMacd(marketData);

            // Assert
            Assert.That(result.Count, Is.AtLeast(1), "Should calculate at least one MACD result.");

            // A seguir, asserções específicas para valores de MACDLine, SignalLine e Histogram
            // Os valores devem ser determinados com base na lógica real do seu MACD e dados de entrada
            // Por exemplo:
            Assert.That(result[0].MacdLine, Is.EqualTo(expectedMacdValue), "The MACD line value is incorrect for the first result.");
            Assert.That(result[0].SignalLine, Is.EqualTo(expectedSignalValue), "The Signal line value is incorrect for the first result.");
            Assert.That(result[0].Histogram, Is.EqualTo(expectedHistogramValue), "The Histogram value is incorrect for the first result.");

            // Continue com outras verificações para o restante dos resultados
        }

        private List<MarketDataPoint> GenerateMarketData(int count)
        {
            var data = new List<MarketDataPoint>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                // Use uma sequência lógica ou dados simulados de fechamento de preço
                data.Add(new MarketDataPoint
                {
                    Timestamp = DateTime.Now.AddDays(-count + i),
                    ClosePrice = random.NextDouble() * 100  // Geração de preço aleatório para exemplo
                });
            }
            return data;
        }
    }
}
