using System.Collections.Generic;
using NexTech_Assessment_API.Models;
using NexTech_Assessment_API.UnitTests;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using NexTech_Assessment_API.Repositories;
using System.Net.Http;
using NexTech_Assessment_API.Services;

namespace NextTech_Assessment_Xunit
{
    public class StoryUnitTests
    {
        [Fact]
        public void TestGetAllStoriesThenBySearch()
        {
            //Arrange
            var dbContext = DbContextMocker.GetTestDatabaseContext(nameof(TestGetAllStoriesThenBySearch));
            var mockSet = new Mock<DbSet<Story>>();
            var repository = new StoryRepository(dbContext);
            var testService = new StoryTestService(repository);
            var mockClient = new HttpClient();
            var service = new StoryService(mockClient);

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
