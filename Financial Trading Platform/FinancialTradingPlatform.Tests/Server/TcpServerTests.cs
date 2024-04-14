using FinancialTradingPlatform.WorkerService;
using Moq;
using System.Net.Sockets;
using System.Text;

namespace FinancialTradingPlatform.Tests.Server
{
    [TestFixture]
    public class TcpServerTests
    {
        private Mock<NetworkStream> mockNetworkStream;
        private Mock<TcpClient> mockTcpClient;
        private TCPWorker server;
        private byte[] responseData;

        [SetUp]
        public void Setup()
        {
            mockNetworkStream = new Mock<NetworkStream>();
            mockTcpClient = new Mock<TcpClient>();
            server = new TCPWorker(12345);

            mockTcpClient.Setup(m => m.GetStream()).Returns(mockNetworkStream.Object);

            string inputJson = "{\"action\":\"getData\"}";
            byte[] requestData = Encoding.UTF8.GetBytes(inputJson);
            mockNetworkStream.Setup(s => s.ReadAsync(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(requestData.Length)
                .Callback((byte[] buffer, int offset, int count, CancellationToken token) =>
                {
                    Array.Copy(requestData, 0, buffer, offset, requestData.Length);
                });

            mockNetworkStream.SetupSequence(s => s.ReadAsync(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(requestData.Length)
                .ReturnsAsync(0);

            responseData = null;
            mockNetworkStream.Setup(s => s.WriteAsync(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Callback((byte[] buffer, int offset, int count, CancellationToken token) =>
                {
                    responseData = new byte[count];
                    Array.Copy(buffer, offset, responseData, 0, count);
                });
        }

        [TearDown]
        public void TearDown()
        {
            server?.Dispose();
        }

        [Test]
        public async Task HandleClient_Should_ProcessDataCorrectly()
        {
            // Act
            await server.HandleClient(mockTcpClient.Object, CancellationToken.None);

            // Assert
            mockNetworkStream.Verify(s => s.WriteAsync(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsNotNull(responseData, "No response was sent back to the client.");

            // Converter a resposta para string e verificar se está correta
            string responseString = Encoding.UTF8.GetString(responseData);
            Assert.IsTrue(responseString.Contains("Data processed"), "The response does not contain the expected text.");
        }
    }
}
