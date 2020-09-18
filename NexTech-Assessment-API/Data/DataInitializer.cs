using System.Linq;
using NexTechAssessmentAPI.Models;

namespace NexTechAssessmentAPI.Data
{
    public static class DataInitializer
    {
        public static void Initialize(DatabaseContext databaseContext)
        {
            databaseContext.Database.EnsureCreated();
            if (databaseContext.TestStories.Any())
            {
                return;
            }

            var story1 = new Story
            {
                Url = "www.test234.com",
                Title = "123 - Test1",
                By = "Phillip Sylvain",
                Id = 12345,
                Descendants = 0,
                Score = 100,
                Time = "01051994",
                Type = "Test"
            };

            var story2 = new Story
            {
                Url = "www.test234.com",
                Title = "123 - Test2",
                By = "Phillip Sylvain",
                Id = 12345,
                Descendants = 0,
                Score = 100,
                Time = "01051994",
                Type = "Test"
            };

            var story3 = new Story
            {
                Url = "www.test345.com",
                Title = "123 - Test3",
                By = "Phillip Sylvain",
                Id = 12345,
                Descendants = 0,
                Score = 100,
                Time = "01051994",
                Type = "Test"
            };

            var story4 = new Story
            {
                Url = "www.test456.com",
                Title = "123 - Test4",
                By = "Phillip Sylvain",
                Id = 12345,
                Descendants = 0,
                Score = 100,
                Time = "01051994",
                Type = "Test"
            };

            var story5 = new Story
            {
                Url = "www.test567.com",
                Title = "Learning - Test5",
                By = "Phillip Sylvain",
                Id = 12345,
                Descendants = 0,
                Score = 100,
                Time = "01051994",
                Type = "Test"
            };

            var stories = new Story[]
            {
                story1,
                story2,
                story3,
                story4,
                story5
            };

            foreach (Story story in stories)
            {
                databaseContext.TestStories.Add(story);
            }
            databaseContext.SaveChanges();
        }

        public static void Seed(this DatabaseContext dbContext)
        {
            dbContext.TestStories.Add(new Story
            {
                Url = "www.test234.com",
                Title = "123 - Test1",
                By = "Phillip Sylvain",
                Id = 12345,
                Descendants = 0,
                Score = 100,
                Time = "01051994",
                Type = "Test"
            });

            dbContext.TestStories.Add(new Story
            {
                Url = "www.test234.com",
                Title = "123 - Test2",
                By = "Phillip Sylvain",
                Id = 12345,
                Descendants = 0,
                Score = 100,
                Time = "01051994",
                Type = "Test"
            });

            dbContext.TestStories.Add(new Story
            {
                Url = "www.test345.com",
                Title = "123 - Learning",
                By = "Phillip Sylvain",
                Id = 12345,
                Descendants = 0,
                Score = 100,
                Time = "01051994",
                Type = "Test"
            });

            dbContext.TestStories.Add(new Story
            {
                Url = "www.test456.com",
                Title = "123 - Test4",
                By = "Phillip Sylvain",
                Id = 12345,
                Descendants = 0,
                Score = 100,
                Time = "01051994",
                Type = "Test"
            });

            dbContext.TestStories.Add(new Story
            {
                Url = "www.test567.com",
                Title = "Learn - Test5",
                By = "Phillip Sylvain",
                Id = 12345,
                Descendants = 0,
                Score = 100,
                Time = "01051994",
                Type = "Test"
            });

            dbContext.SaveChanges();
        }

    }
}
