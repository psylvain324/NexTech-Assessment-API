using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TechAssessment.Interfaces;
using TechAssessment.Models;

namespace TechAssessment.Services
{
    public class StoryService : IStoryService
    {
        private const string BaseUrl = "https://hacker-news.firebaseio.com/v0/";
        private readonly HttpClient _client;

        public StoryService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<string>> GetAllIdsAsync()
        {
            var httpResponse = await _client.GetAsync(BaseUrl + "newest");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to retrieve Storie IDs!");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var idList = JsonConvert.DeserializeObject<List<string>>(content);

            return idList;
        }

        public async Task<List<Story>> GetNewestStories()
        {
            var idList = await GetAllIdsAsync();
            var stories = new List<Story>();

            if (idList != null)
            {
                foreach (string id in idList)
                {
                    var httpResponse = await _client.GetAsync(BaseUrl + id);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("Unable to retrieve Story!");
                    }

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    var story = JsonConvert.DeserializeObject<Story>(content);
                    stories.Add(story);
                }
            }

            return stories;
        }

        public async Task<Story> GetStoryAsync(int id)
        {
            var httpResponse = await _client.GetAsync($"{BaseUrl}{id}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var todoItem = JsonConvert.DeserializeObject<Story>(content);

            return todoItem;
        }

        public Task<PagedList<Story>> GetPaginatedNewestStories(PagingParams pagingParams)
        {
            throw new System.NotImplementedException();
        }

    }
}
