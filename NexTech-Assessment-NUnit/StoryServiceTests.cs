using System.Net.Http;
using NUnit.Framework;
using TechAssessment.Services;

namespace NexTech_Assessment_NUnit
{
    public class StoryTests
    {
        [TestFixture]
        public class ServiceTests
        {
            private StoryService _service;
            private readonly HttpClient _client;

            [SetUp]
            public void SetUp()
            {
                _service = new StoryService(_client);
            }

            [Test]
            public void NewestStoriesReturnsValue()
            {
                var result = _service.GetNewestStories();

                Assert.IsNotNull(result);
            }
        }
    }

}
