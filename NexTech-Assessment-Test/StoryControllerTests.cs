using System;
using System.Net.Http;
using Moq;
using NexTech_Assessment_API.Data;
using NUnit.Framework;
using TechAssessment.Controllers;

namespace NexTech_Assessment_NUnit
{
    [TestFixture]
    public class StoriesControllerTests
    {
        private readonly DatabaseContext databaseContext;
        private const string ServiceBaseURL = "http://localhost:5001/";
        private HttpClient _client;
        private HttpResponseMessage _response;
        private Mock<StoryController> _controller;

        [OneTimeSetUp]
        public void Setup()
        {
            _controller = new Mock<StoryController>(MockBehavior.Strict);
            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
        }

        [SetUp]
        public void ReInitializeTest()
        {
            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
        }

        [Test]
        public void GetStoriesByBatchTest()
        {
            
        }
    }
}