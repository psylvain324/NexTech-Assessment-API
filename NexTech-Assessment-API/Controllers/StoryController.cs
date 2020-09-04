using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechAssessment.Interfaces;
using TechAssessment.Models;

namespace TechAssessment.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
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
            return (IEnumerable<Story>)_service.GetNewestStories();
        }
	}
}
