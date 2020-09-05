using System.Net.Http;
using NUnit.Framework;
using TechAssessment.Interfaces;
using TechAssessment.Services;

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

                Assert.IsNotNull(result);
            }
        }
    }

}
