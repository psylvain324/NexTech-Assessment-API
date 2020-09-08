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
                    Url = "",
                    Title = ""
                },  
                new Story()
                {
                    Url = "",
                    Title = ""
                },  
                new Story()
                {
                    Url = "",
                    Title = ""
                },  
                new Story()
                {
                    Url = "",
                    Title = ""
                },  
                new Story()
                {
                    Url = "",
                    Title = ""
                }
            };

            return stories;
        }
    }
}
