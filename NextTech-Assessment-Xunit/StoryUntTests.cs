using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NexTech_Assessment_API.Controllers;
using NexTech_Assessment_API.IntegrationTests;
using NexTech_Assessment_API.UnitTests;
using Xunit;

namespace NexTech_Assessment_API.API.IntegrationTests
{
    /*
    public class StoryUnitTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public StoryUnitTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task TestGetStoryIdsAsync()
        {
            // Arrange
            var request = "https://localhost:5001/GetAllIdsSync";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetStoryById()
        {
            // Arrange
            var request = "https://localhost:5001//Story/22140";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetNewStoryIds()
        {
            // Arrange
            var dbContext = DbContextMocker.GetTestDatabaseContext(nameof(TestGetNewStoryIds));
            var controller = new StoryController();

            // Act
            var response = await controller.GetNewStoryIds() as ObjectResult;
            var value = response.Value as IEnumerable<string>;

            dbContext.Dispose();

            // Assert
            Assert.NotNull(value);
        }

    }*/
}