using System.Collections.Generic;
using NexTechAssessmentAPI.Models;
using NexTechAssessmentAPI.UnitTests;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using NexTechAssessmentAPI.Repositories;
using System.Net.Http;
using NexTechAssessmentAPI.Services;
using NexTechAssessmentAPI.Data;

namespace NextTech_Assessment_Xunit
{
    public class StoryUnitTests
    {
        [Fact]
        public void TestGetAllStoriesThenBySearch()
        {
            //Arrange
            using DatabaseContext dbContext = DbContextMocker.GetTestDatabaseContext(nameof(TestGetAllStoriesThenBySearch));
            Mock<DbSet<Story>> mockSet = new Mock<DbSet<Story>>();
            StoryRepository repository = new StoryRepository(dbContext);
            StoryTestService testService = new StoryTestService(repository);
            HttpClient mockClient = new HttpClient();
            StoryService service = new StoryService(mockClient);

            //Act
            List<Story> resultsList = (List<Story>)testService.GetAllTestStories();
            IEnumerable<Story> results = testService.GetAllTestStories();
            IEnumerable<Story> searchResults = service.GetStoriesByFieldSearch("Title", "Learning", results);
            dbContext.Dispose();

            //Assert
            Assert.Equal(5, resultsList.Count);
            Assert.Single(searchResults);
        }

    }
}
