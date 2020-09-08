using System;
using System.Collections.Generic;
using TechAssessment.Models;

namespace NexTech_Assessment_NUnit.Data
{
    public class DataInitializer
    {
        public static List<Story> GetAllStories()
        {
            var stories = new List<Story>
            {
                new Story()
                {
                    Url = "www.test234.com",
                    Title = "123 - Test1"
                },  
                new Story()
                {
                    Url = "www.test234.com",
                    Title = "123 - Test2"
                },  
                new Story()
                {
                    Url = "www.test345.com",
                    Title = "123 - Test3"
                },  
                new Story()
                {
                    Url = "www.test456.com",
                    Title = "123 - Test4"
                },  
                new Story()
                {
                    Url = "www.test567.com",
                    Title = "Learning - Test5"
                }
            };

            return stories;
        }
    }
}
