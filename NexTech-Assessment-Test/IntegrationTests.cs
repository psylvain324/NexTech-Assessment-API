using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NexTech_Assessment_API;
using NUnit.Framework;
using Xunit;

namespace NexTech_Assessment_NUnit
{
   [Collection("Integration Tests")]
   [TestFixture]
    public class IntgrationTests
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IntgrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetRoot_ReturnsSuccessAndStatusUp()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode();
            Xunit.Assert.NotNull(response.Content);
            var responseObject = JsonSerializer.Deserialize<ResponseType>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Xunit.Assert.Equal("Up", responseObject?.Status);
        }

        private class ResponseType
        {
            public string Status { get; set; }
        }
    }
}
