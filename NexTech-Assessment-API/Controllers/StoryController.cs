using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechAssessment.Interfaces;
using TechAssessment.Models;

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
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public IEnumerable<Story> GetNewStories()
		{
            List<Story> stories = new List<Story>();
            try
            {
                stories = _service.GetNewestStories().Result;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: " + ex.Message);
                return null;
            }
            return stories;
        }

        /// <summary>
        /// This returns all the Newest Stories by field and text specified.
        /// </summary>
        /// <returns>IEnumerable of Stories</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/NewStories/{field}/{search}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "field", "search" })]
        public IEnumerable<Story> GetNewStoriesSearch(string field, string search)
        {
            try
            {
                var stories = _service.GetNewestStories().Result;
                switch (field)
                {
                    case "Title":
                        stories = (from story in stories
                                   where story.Title.Contains(search)
                                   select story).ToList();
                        break;
                    default:
                        _logger.Log(LogLevel.Information, "Stories list unchanged!", stories);
                        return stories;
                }

                return stories;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: " + ex.Message);
                return null;
            }
        }
    }
}
