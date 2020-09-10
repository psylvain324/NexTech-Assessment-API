using System;
using System.Net.Http;
using Moq;
using NexTech_Assessment_API.Data;
using NUnit.Framework;
using TechAssessment.Interfaces;
using TechAssessment.Models;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace NexTech_Assessment_NUnit
{
    public class StoryTests
    {
        [TestFixture]
        public class ServiceTests
        {
            private Mock<IStoryService> _mockService;
            private HttpClient _client;
            private WireMockServer _stubServer;
            private DatabaseContext _context;

            [OneTimeSetUp]
            public void SetUp()
            {
                _client = new HttpClient();
                _mockService = new Mock<IStoryService>(MockBehavior.Strict);
                _stubServer = WireMockServer.Start();
            }

            [Test]
            public void GetStoriesInParallelFixedReturnsValue()
            {
                var result = _mockService.Setup(t => t.GetStoriesInParallelFixed());
                Assert.IsNotNull(result);
            }

            [Test]
            public void GetStoryIdsAsyncReturnsValue()
            {
                var result = _mockService.Setup(t => t.GetAllIdsAsync());
                Assert.IsNotNull(result);
            }

            [Test]
            public void GetStoryByNumberAndSizeeturnsValue()
            {
                var result = _mockService.Setup(t => t.GetStoryByNumberAndSize(1, 10));
                Assert.IsNotNull(result);
            }

            [Test]
            public void GetStoryByIdReturnsCorrectValue()
            {
                var result = _mockService.Setup(t => t.GetStoryById("24402410"));
                Assert.IsNotNull(result);
            }

            [Test]
            public void GetStoryByFieldSearchReturnsCorrectValue()
            {
                //TODO - Implement rest of this Unit Test
                //var result = _service.Setup(t => t.GetStoriesByFieldSearch("Title", "Learning", _context.Stories));
                //Assert.IsNotNull(result);
            }

            [Test]
            public void GetStoryByIdReturnsOne()
            {
                //Arrange 
                _stubServer
                    .Given(
                        Request.Create().WithPath("/story/24402410").UsingGet())
                    .RespondWith(
                        Response.Create()
                        .WithStatusCode(200)
                        .WithHeader("Content-Type", "application/json"));
            }

            [TearDown]
            public void TearDown()
            {
                try
                {
                    _stubServer.Stop();
                }
                catch
                {
                    Console.WriteLine("There was a problem!");
                }
            }

            /*
             *  "by": "pseudolus",
                "descendants": 0,
                "id": 24402410,
                "kids": null,
                "score": 1,
                "time": "1599509824",
                "title": "China to launch initiative to set global data-security rules",
                "type": "story",
                "url": "https://www.reuters.com/article/us-china-usa-data/china-to-launch-initiative-to-set-global-data-security-rules-wsj-idUSKBN25Y1WK"
             */

        }

    }

}
