﻿using System;
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
            var storyIds = await GetAllIdsAsync();
            var users = new List<Story>();
            var batchSize = 100;
            int numberOfBatches = (int)Math.Ceiling((double)storyIds.Count() / batchSize);

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentIds = storyIds.Skip(i * batchSize).Take(batchSize);
                var tasks = currentIds.Select(id => GetStoryById(id));
                users.AddRange(await Task.WhenAll(tasks));
            }

            return users;
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

        public async Task<List<Story>> GetPaginatedNewestStories(int pageNumber, int pageSize)
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
    }
}
