using System.Collections.Generic;
using System.Linq;
using NexTech_Assessment_API.Data;
using TechAssessment.Models;

namespace NexTech_Assessment_NUnit.Data
{
    public class DataInitializer
    {
        public static void Initialize(DatabaseContext databaseContext)
        {
            databaseContext.Database.EnsureCreated();
            if (databaseContext.Stories.Any())
            {
                return;
            }
        }

        public static List<Story> GetAllStories()
        {
            var stories = new List<Story>
            {
                new Story()
                {
                    Url = "www.test234.com",
                    Title = "123 - Test1",
                    By = "Phillip Sylvain",
                    Id = 12345,
                    Descendants = 0,
                    Kids = {},
                    Score = 100,
                    Time = "01051994",
                    Type = "Test"
                },
                new Story()
                {
                    Url = "www.test234.com",
                    Title = "123 - Test2",
                    By = "Phillip Sylvain",
                    Id = 12345,
                    Descendants = 0,
                    Kids = {},
                    Score = 100,
                    Time = "01051994",
                    Type = "Test"
                },
                new Story()
                {
                    Url = "www.test345.com",
                    Title = "123 - Test3",
                    By = "Phillip Sylvain",
                    Id = 12345,
                    Descendants = 0,
                    Kids = {},
                    Score = 100,
                    Time = "01051994",
                    Type = "Test"
                },
                new Story()
                {
                    Url = "www.test456.com",
                    Title = "123 - Test4",
                    By = "Phillip Sylvain",
                    Id = 12345,
                    Descendants = 0,
                    Kids = {},
                    Score = 100,
                    Time = "01051994",
                    Type = "Test"
                },
                new Story()
                {
                    Url = "www.test567.com",
                    Title = "Learning - Test5",
                    By = "Phillip Sylvain",
                    Id = 12345,
                    Descendants = 0,
                    Kids = {},
                    Score = 100,
                    Time = "01051994",
                    Type = "Test"
                }
            };

            return stories;
        }
    }
}
