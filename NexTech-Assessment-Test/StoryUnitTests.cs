using System.Collections.Generic;
using Moq;
using NexTech_Assessment_API.Data;
using NUnit.Framework;
using NexTech_Assessment_API.Interfaces;
using NexTech_Assessment_API.Models;
using System.Linq;
using NexTech_Assessment_API.Services;
using System.Net.Http;

namespace NexTech_Assessment_NUnit
{
    public class StoryUnitTests
    {
        [TestFixture]
        public class ServiceTests
        {
            private HttpClient _client;

            [OneTimeSetUp]
            public void SetUp()
            {
                _client = new HttpClient();
            }

            [Test]
            public void GetStoryByFieldSearchReturnsCorrectValue()
            {
                //Arrange
                var mockContext = new Mock<DatabaseContext>();
                var testStories = GetTestStories();
                //mockContext.Setup(c => c.StaticTestStories).Returns((DbSet<Story>)testStories);

                var service = new StoryService(_client);
                var storyIds = service.GetAllIdsAsync();
                var stories = service.GetStoriesInParallelFixed();

                //Act
                var mockService = new Mock<IStoryService>();
                mockService.Setup(m => m.GetStoriesByFieldSearch("Title", "Learning", testStories));

                //Assert
                Assert.IsNotNull(mockService);
                Assert.GreaterOrEqual(100, storyIds.Id);
                Assert.GreaterOrEqual(100, stories.Id);
            }

            public IEnumerable<Story> GetTestStories()
            {
                var data = new List<Story>
                {
                new Story {
                    Url = "www.test234.com",
                    Title = "123 - Test1",
                    By = "Phillip Sylvain",
                    Id = 12345,
                    Descendants = 0,
                    Kids = { },
                    Score = 100,
                    Time = "01051994",
                    Type = "Test"
                },
                new Story {
                    Url = "www.test234.com",
                    Title = "123 - Test1",
                    By = "Phillip Sylvain",
                    Id = 12345,
                    Descendants = 0,
                    Kids = { },
                    Score = 100,
                    Time = "01051994",
                    Type = "Test"
                },
                new Story {
                    Url = "www.test234.com",
                    Title = "123 - Test1",
                    By = "Phillip Sylvain",
                    Id = 12345,
                    Descendants = 0,
                    Kids = { },
                    Score = 100,
                    Time = "01051994",
                    Type = "Test"
                }}.AsQueryable();

                return data;
            }

        }

    }

}
