using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NexTechAssessmentAPI.Interfaces;
using NexTechAssessmentAPI.Models;

namespace NexTechAssessmentAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class StoryController : Controller
    {
        private readonly ILogger<StoryController> _logger;
        private readonly IStoryService _service;

        public StoryController(IStoryService service, ILogger<StoryController> logger = null)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// This returns all the Newest Story Ids.
        /// </summary>
        /// <returns>IActionResult of IEnumerable Strings</returns>
        /// Explicit Header:
        /// public async Task<IEnumerable<string>> GetNewStoryIdsAsync()
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/NewStoryIds")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> GetNewStoryIds()
        {
            List<string> storyIds;
            try
            {
                storyIds = await _service.GetAllIdsAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: " + ex.Message);
                return StatusCode(500, "Internal server error"); ;
            }
            return Ok(storyIds);
        }

        /// <summary>
        /// This returns all the Newest Stories.
        /// </summary>
        /// <returns>IActionResult of IEnumerable Stories</returns>
        /// Explicit Header:
        /// public async Task<IEnumerable<Story>> GetNewStories()
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/NewStories")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> GetNewStories()
        {
            List<Story> stories;
            try
            {
                stories = (List<Story>)await _service.GetStoriesInParallelFixed().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: " + ex.Message);
                return StatusCode(500, "Internal server error"); ;
            }
            return Ok(stories);
        }

        /// <summary>
        /// This returns all the Newest Stories Paginated.
        /// </summary>
        /// <returns>IActionResult of IEnumerable Stories</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/NewStoriesPaginated")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        //TODO - This requires a change to work.
        public async Task<IActionResult> GetNewStoriesPaginated([FromQuery] PagingParams pagingParams)
        {
            PagedList<Story> stories;
            try
            {
                stories = await _service.GetNewestStoriesPagedList(pagingParams).ConfigureAwait(false);

                var metadata = new
                {
                    stories.TotalCount,
                    stories.PageSize,
                    stories.CurrentPage,
                    stories.TotalPages,
                    stories.HasNext,
                    stories.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                _logger.LogInformation($"Returned {stories.TotalCount} total Stories.");
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: " + ex.Message);
                return StatusCode(500, "Internal server error"); ;
            }
            return Ok(stories);
        }

        /// <summary>
        /// This returns all the Newest Story Ids.
        /// </summary>
        /// <returns>IActionResult of Story</returns>
        /// Explicit Header:
        /// public async Task<Story> GetStoryById(string id)
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/Story/{id}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> GetStoryById(string id)
        {
            Story story;
            try
            {
                story = await _service.GetStoryById(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: " + ex.Message);
                return StatusCode(500, "Internal server error"); ;
            }
            return Ok(story);
        }

        /// <summary>
        /// This returns all the Newest Stories by field and text specified.
        /// </summary>
        /// <returns>IActionResult ofIEnumerable Stories</returns>
        /// Explicit Header:
        /// public IEnumerable<Story> GetNewStoriesSearch(string field, string search)
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [Route("/NewStories/{field}/{search}")]
        [EnableCors("CorsPolicy")]
        public IActionResult GetNewStoriesSearch(string field, string search)
        {
            try
            {
                var stories = _service.GetStoriesInParallelFixed().Result;
                return Ok(_service.GetStoriesByFieldSearch(field, search, stories));
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error: " + ex.Message);
                return StatusCode(500, "Internal server error"); ;
            }
        }

    }
}
