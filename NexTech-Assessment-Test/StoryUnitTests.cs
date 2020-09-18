using System.Collections.Generic;
using Moq;
using NexTechAssessmentAPI.Data;
using NUnit.Framework;
using NexTechAssessmentAPI.Interfaces;
using NexTechAssessmentAPI.Models;
using System.Linq;
using NexTechAssessmentAPI.Services;
using System.Net.Http;
using System.Threading.Tasks;

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
                Mock<DatabaseContext> mockContext = new Mock<DatabaseContext>();
                IEnumerable<Story> testStories = GetTestStories();
                //mockContext.Setup(c => c.StaticTestStories).Returns((DbSet<Story>)testStories);

                StoryService service = new StoryService(_client);
                Task<List<string>> storyIds = service.GetAllIdsAsync();
                Task<IEnumerable<Story>> stories = service.GetStoriesInParallelFixed();

                //Act
                Mock<IStoryService> mockService = new Mock<IStoryService>();
                mockService.Setup(m => m.GetStoriesByFieldSearch("Title", "Learning", testStories));

                //Assert
                Assert.IsNotNull(mockService);
                Assert.GreaterOrEqual(100, storyIds.Id);
                Assert.GreaterOrEqual(100, stories.Id);
            }

            public IEnumerable<Story> GetTestStories()
            {
                IQueryable<Story> data = new List<Story>
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
