using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using NUnit.Framework;
using TechAssessment.Controllers;
using TechAssessment.Interfaces;
using TechAssessment.Models;
using Xunit;

namespace NexTech_Assessment_NUnit
{
    public class StoryTests
    {
        [TestFixture]
        public class ServiceTests
        {
            private IStoryService _service;
            private readonly HttpClient _client;

            public ServiceTests(IStoryService service)
            {
                _service = service;
            }

            [SetUp]
            public void SetUp()
            {

            }

            [Test]
            public void NewestStoriesReturnsValue()
            {
                var result = _service.GetNewestStories();

                NUnit.Framework.Assert.IsNotNull(result);
            }

            [Test]
            [Fact]
            public async Task CanGetStories()
            {
                // The endpoint or route of the controller action.
                var httpResponse = await _client.GetAsync("/api/players");

                // Must be successful.
                httpResponse.EnsureSuccessStatusCode();

                // Deserialize and examine results.
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var stories = JsonConvert.DeserializeObject<IEnumerable<Story>>(stringResponse);
                Xunit.Assert.Contains(stories, s => s.Title == "Wayne");
                Xunit.Assert.Contains(stories, s => s.Url == "Mario");
            }

            /*[Test]
            [Fact]
            public async Task GivenResultAlreadyRetrieved_ShouldNotCallServiceAgain()
            {
                // Arrange
                var expected = new Story();

                var cache = new MemoryCache(new MemoryCacheOptions());
                var searchService = new Mock<IStoryService>();

                searchService
                    .SetupSequence(s => s.FindAsync(It.IsAny<SearchRequestViewModel>()))
                    .Returns(Task.FromResult(expected))
                    .Returns(Task.FromResult(new MyViewModel()));

                var sut = new StoryController(cache, searchService.Object);

                // Act
                var resultFromFirstCall = await sut.Search(input);
                var resultFromSecondCall = await sut.Search(input);

                // Assert
                Xunit.Assert.Same(expected, resultFromFirstCall);
                Xunit.Assert.Same(expected, resultFromSecondCall);
            }*/
        }
    }

}
