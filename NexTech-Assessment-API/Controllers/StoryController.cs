using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechAssessment.Interfaces;
using TechAssessment.Models;

namespace TechAssessment.Controllers
{
    [Produces("application/json")]
    [ApiController]
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
        /// Depracated - This returns all the Newest Stories.
        /// </summary>
        /// <returns>IEnumerable of Stories</returns>
        [HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
        [Route("/NewStories")]
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        public IEnumerable<Story> GetNewStories()
		{
            List<Story> stories;
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
        /// This returns all the Newest Story Ids.
        /// </summary>
        /// <returns>IEnumerable of Strings</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/StoryIds")]
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        public async Task<IEnumerable<string>> GetNewStoryIdsAsync()
        {
            List<string> storyIds;
            try
            {
                storyIds = await _service.GetAllIdsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: " + ex.Message);
                return null;
            }
            return storyIds;
        }

        /// <summary>
        /// This returns all the Newest Stories.
        /// More Efficient then GetStories()
        /// And is the API used in the UI project.
        /// </summary>
        /// <returns>IEnumerable of Stories</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/NewStoriesByBatch")]
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        public async Task<IEnumerable<Story>> GetNewStoriesByBatchAsync()
        {
            List<Story> stories;
            //List<string> storyIds;
            try
            {
                stories = (List<Story>)await _service.GetStoriesInParallelFixed();
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: " + ex.Message);
                return null;
            }
            return stories;
        }

        /// <summary>
        /// This returns all the Newest Story Ids.
        /// </summary>
        /// <returns>IEnumerable of Strings</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/Story/{id}")]
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        public async Task<Story> GetStoryById(string id)
        {
            Story story;
            try
            {
                story = await _service.GetStoryById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: " + ex.Message);
                return null;
            }
            return story;
        }

        /// <summary>
        /// This returns the Newest Stories by Size and Number
        /// </summary>
        /// <returns>IEnumerable of Stories</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/NewStoriesBy/{pageNumber}/{pageSize}")]
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        public IEnumerable<Story> GetNewStoriesByPage(int pageNumber, int pageSize)
        {
            List<Story> stories;
            try
            {
                stories = _service.GetStoryByNumberAndSize(pageNumber, pageSize).Result;
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
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "field", "search" })]
        [EnableCors("CorsPolicy")]
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
