using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechAssessment.Interfaces;
using TechAssessment.Models;
using TechAssessment.Services;

namespace TechAssessment.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/Story")]
    public class StoryController : Controller
    {
        private readonly ILogger<StoryController> _logger;
        private readonly IStoryService _service;

        public StoryController(ILogger<StoryController> logger, IStoryService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// This returns all the Newest Stories.
        /// </summary>
        /// <returns>IEnumerable of Stories</returns>
        [HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
        [Route("/NewStories")]
        public IEnumerable<Story> GetNewStories()
		{
            IEnumerable<Story> stories = _service.GetNewestStories().Result;
            return null;
        }
	}
}
