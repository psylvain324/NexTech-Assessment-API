using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NexTech_Assessment_API.Interfaces;
using NexTech_Assessment_API.Models;
using NexTechAssessmentAPI.Interfaces;
using NexTechAssessmentAPI.Models;

namespace NexTech_Assessment_API.Services
{
    public class CommentService: ICommentService
    {
        private const string BaseUrl = "https://hacker-news.firebaseio.com/v0/";
        private readonly HttpClient _client;
        private readonly IStoryService _storyService;

        public CommentService(HttpClient client, IStoryService storyService)
        {
            _client = client;
            _storyService = storyService;
        }

        public async Task<List<Comment>> GetCommentsByStoryId(int id)
        {
            List<Comment> comments = new List<Comment>();
            HttpResponseMessage httpResponse = await _client.GetAsync(BaseUrl + "item/" + id + ".json?print=pretty").ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to retrieve Story!");
            }

            string content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            Story story = JsonConvert.DeserializeObject<Story>(content);

            if (!string.IsNullOrEmpty(story.Url) && story.Url != null)
            {
                if (story.Kids.Count > 0)
                {
                    foreach(string commentId in story.Kids)
                    {
                        HttpResponseMessage childResponse = await _client.GetAsync(BaseUrl + "item/" + commentId + ".json?print=pretty").ConfigureAwait(false);
                        string childContent = await childResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                        Comment comment = JsonConvert.DeserializeObject<Comment>(childContent);

                        comments.Add(comment);
                    }

                    return comments;
                }
            }

            return null;
        }

        public async Task<List<StoryCommentViewModel>> GetStoryCommentsViewModel()
        {
            List<StoryCommentViewModel> storyComments = new List<StoryCommentViewModel>();
            IEnumerable<Story> stories = await _storyService.GetStoriesInParallelFixed();
            foreach (Story story in stories)
            {
                StoryCommentViewModel viewModel = new StoryCommentViewModel();
                viewModel.Id = story.Id;
                viewModel.Url = story.Url;
                viewModel.Time = story.Time;
                viewModel.Text = string.Empty;
                viewModel.Type = story.Type;

                if (story.Kids.Count > 0)
                {
                    foreach (string commentId in story.Kids)
                    {
                        HttpResponseMessage childResponse = await _client.GetAsync(BaseUrl + "item/" + commentId + ".json?print=pretty").ConfigureAwait(false);
                        string childContent = await childResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                        Comment comment = JsonConvert.DeserializeObject<Comment>(childContent);

                        //Overwrite everything but Url for now.
                        //Not a longterm solution just to use for UI. 
                        viewModel.Id = comment.Id;
                        viewModel.Time = comment.Time;
                        viewModel.Type = comment.Type;
                        viewModel.Text = comment.Text;
                        storyComments.Add(viewModel);
                    }
                }
                else
                {
                    storyComments.Add(viewModel);
                }
            }

            return storyComments;
        }
    }
}
