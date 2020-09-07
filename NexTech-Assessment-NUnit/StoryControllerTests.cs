using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Xunit;

namespace NexTech_Assessment_NUnit
{ 
    [TestFixture]
    public class StoryControllerTests
    {
        public StoryControllerTests()
        {
        }

        [Fact]
        public async Task BasicEndPointTest()
        {
            // Arrange
            var factory = new WebApplicationFactory<NexTech_Assessment_API.Startup>();

            // Create an HttpClient which is setup for the test host
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Home/Test");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotEmpty(responseString);
        }
    }
}
