using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NexTech_Assessment_API.Interfaces;
using NexTech_Assessment_API.Models;
using NexTechAssessmentAPI.Interfaces;
using NexTechAssessmentAPI.Models;

namespace NexTech_Assessment_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class CommentController: Controller
    {
        private readonly IStoryService _storyService;
        private readonly ICommentService _commentService;

        public CommentController(IStoryService storyService, ICommentService commentService)
        {
            _storyService = storyService;
            _commentService = commentService;
        }

        /// <summary>
        /// This returns all comments for a given Story.
        /// </summary>
        /// <returns>IActionResult of List of Comments</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/CommentsById/{id}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> GetStoryComments(string id)
        {
            List<Comment> comments = new List<Comment>();
            try
            {
                Story story = await _storyService.GetStoryById(id).ConfigureAwait(false);
                comments = await _commentService.GetCommentsByStoryId(story.Id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message); ;
            }
            return Ok(comments);
        }

        /// <summary>
        /// This returns Comments and Stories as single View Model
        /// </summary>
        /// <returns>IActionResult of List of StoryCommentViewModel</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("/CommentSoryViewModel")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> GetStoryCommentsViewModel(string id)
        {
            List<StoryCommentViewModel> results = new List<StoryCommentViewModel>();
            try
            {
                results = await _commentService.GetStoryCommentsViewModel();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message); ;
            }
            return Ok(results);
        }
    }
}
