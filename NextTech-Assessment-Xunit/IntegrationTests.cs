/*using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NexTech_Assessment_API.IntegrationTests;
using NexTech_Assessment_API.Models;
using NexTech_Assessment_API.Services;
using NextTech_Assessment_Xunit;
using Xunit;

namespace NexTech_Assessment_API.API.IntegrationTests
{
    public class StoryUnitTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient _client;
        private StoryTestService _testService;
        private StoryService _service;

        public StoryUnitTests(TestFixture<Startup> fixture, StoryTestService testService, StoryService service)
        {
            _client = fixture.Client;
            _testService = testService;
            _service = service;
        }

        [Fact]
        public void TestGetStoryFieldSearch()
        {
            // Arrange
            var stories = _testService.GetAllTestStories();
            List<Story> storyList = (List<Story>)_testService.GetAllTestStories();

            // Act
            var response = _service.GetStoriesByFieldSearch("Title", "Learning", stories);

            // Assert
            Assert.Equal(3, storyList.Count);
        }
     }
}
*/