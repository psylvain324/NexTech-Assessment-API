using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace NexTech_Assessment_NUnit
{
    [TestFixture]
    public class IntegrationTests
    {
        private const string BaseUrl = "https://hacker-news.firebaseio.com/v0/";
        private HttpClient _client;
        private TestServer _server;

        [OneTimeSetUp]
        public void Setup()
        {
            _client = new HttpClient();
        }

        [Test]
        public async Task ReturnStoryIdsFromExternalApi()
        {
            // Act
            var response = await _client.GetAsync(BaseUrl + "newstories.json?print=pretty");
            response.EnsureSuccessStatusCode();

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
