﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using NexTechAssessmentAPI.Models;
using NexTechAssessmentAPI.Repositories;

namespace NextTech_Assessment_Xunit
{
    public class StoryTestService: IStoryTestService
    {
        private readonly StoryRepository _repository;
        private readonly ILogger<StoryTestService> _logger;

        public StoryTestService(StoryRepository repository, ILogger<StoryTestService> logger = null)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Story> GetAllTestStories()
        {
            List<Story> stories = new List<Story>();
            try
            {
                stories = _repository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return stories;
        }

        public Story GetTestStoryById(int id)
        {
            Story story = new Story();

            try
            {
                story = _repository.GetAll().FirstOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return story;
        }

        public void CreateTestStory(Story story)
        {
            try
            {
                _repository.Add(story);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void UpdateTestStory(Story story)
        {
            try
            {
                _repository.Edit(story);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex.Message);
            }
        }

        public void DeleteTestStory(int id)
        {
            try
            {
                Story story = _repository.GetById(id);
                _repository.Delete(story);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

    }
}
