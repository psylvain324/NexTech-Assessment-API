using System;
using System.Collections.Generic;
using System.Linq;
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
            var httpResponse = await _client.GetAsync(BaseUrl + "newstories.json?print=pretty");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to retrieve Storie IDs!");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var idList = JsonConvert.DeserializeObject<List<string>>(content);

            return idList;
        }

        public async Task<IEnumerable<Story>> GetStoriesInParallelFixed()
        {
            var batchSize = 100;
            var idList = await GetAllIdsAsync();
            var stories = new List<Story>();
            var validStories = new List<Story>();
            int numberOfBatches = (int)Math.Ceiling((double)idList.Count() / batchSize);

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentIds = idList.Skip(i * batchSize).Take(batchSize);
                var tasks = currentIds.Select(id => GetStoryById(id));
                stories.AddRange(await Task.WhenAll(tasks));
            }

            foreach (Story story in stories)
            {
                if (story != null)
                {
                    validStories.Add(story);
                }
            }

            return validStories;
        }

        public async Task<Story> GetStoryById(string id)
        {
            var httpResponse = await _client.GetAsync(BaseUrl + "item/" + id + ".json?print=pretty");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to retrieve Story!");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var story = JsonConvert.DeserializeObject<Story>(content);
            if(story.Url == "" || story.Url == null)
            {
                return null;
            }
            return story;
        }

        public async Task<List<Story>> GetNewestStories()
        {
            var idList = await GetAllIdsAsync();
            var stories = new List<Story>();

            if (idList != null)
            {
                foreach (string id in idList)
                {
                    var httpResponse = await _client.GetAsync(BaseUrl + "item/" + id + ".json?print=pretty");

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("Unable to retrieve Story!");
                    }

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    var story = JsonConvert.DeserializeObject<Story>(content);
                    if(story.Url != string.Empty || story.Url != null)
                    {
                        stories.Add(story);
                    }
                }
            }

            return stories;
        }


        public async Task<List<Story>> GetStoryByNumberAndSize(int pageNumber, int pageSize)
        {
            var idList = await GetAllIdsAsync();
            var stories = new List<Story>();
            int collectionSize = pageNumber * pageSize;

            for (int i = collectionSize; i < collectionSize + pageSize; i++)
            {
                var httpResponse = await _client.GetAsync(BaseUrl + "item/" + idList.ElementAt(i) + ".json?print=pretty");

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Unable to retrieve Story!");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                var story = JsonConvert.DeserializeObject<Story>(content);
                stories.Add(story);
            }

            return stories;
        }

        public IEnumerable<Story> GetStoriesByFieldSearch(string field, string search, IEnumerable<Story> stories)
        {
            switch (field)
            {
                case "Title":
                    stories = from story in stories
                              where story.Title.Contains(search)
                              select story;
                    break;
                //Easily add more search fields
                default:
                    return stories;
            }

            return stories;
        }

    }
}
