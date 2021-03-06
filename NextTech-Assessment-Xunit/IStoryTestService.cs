﻿using System.Collections.Generic;
using NexTechAssessmentAPI.Models;

namespace NextTech_Assessment_Xunit
{
    public interface IStoryTestService
    {
        IEnumerable<Story> GetAllTestStories();
        Story GetTestStoryById(int id);
        void CreateTestStory(Story story);
        void UpdateTestStory(Story story);
        void DeleteTestStory(int id);
    }
}
