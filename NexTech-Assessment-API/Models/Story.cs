using System.Collections.Generic;

namespace TechAssessment.Models
{
    public class Story
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public int Descendants { get; set; }

        public List<int> Kids { get; set; }

        public int Score { get; set; }

        public string Time { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public Story()
        {


        }
    }
}
