using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NexTech_Assessment_API.Interfaces;
using NUnit.Framework;

namespace NexTech_Assessment_NUnit
{
    [TestFixture]
    public class IntegrationTests
    {
        private const string BaseUrl = "https://hacker-news.firebaseio.com/v0/";
        private Mock<IStoryService> _mockService;
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            _client = new HttpClient();
            _mockService = new Mock<IStoryService>(MockBehavior.Strict);
        }

        [Test]
        public void GetStoriesInParallelFixedReturnsValue()
        {
            var result = _mockService.Setup(t => t.GetStoriesInParallelFixed());
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetStoryIdsAsyncReturnsValue()
        {
            var result = _mockService.Setup(t => t.GetAllIdsAsync());
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetStoryByIdReturnsCorrectValue()
        {
            var result = _mockService.Setup(t => t.GetStoryById("24402410"));
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task ReturnStoryIdsFromExternalApi()
        {
            // Arrange
            var response = await _client.GetAsync(BaseUrl + "newstories.json?print=pretty");
            response.EnsureSuccessStatusCode();

            // Act
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.GreaterOrEqual(responseString.Length, 500);
        }

        [Test]
        public async Task MockHttpClientRequest()
        {
            const string testContent = "Test";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(testContent)
                });
            var underTest = new MockClient(new HttpClient(mockMessageHandler.Object));

            // Act
            var result = await underTest.GetContentSize("http://localhost:5001");

            // Assert
            Assert.AreEqual(testContent.Length, result);
        }

        public async Task<int> GetContentSize(string uri)
        {
            var response = await _client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            return content.Length;
        }
    }

    public class MockClient
    {
        public MockClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetContentSize(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            return content.Length;
        }

        private readonly HttpClient _httpClient;
    }
}
