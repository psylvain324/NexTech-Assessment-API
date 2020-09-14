using System.Collections.Generic;

namespace NexTech_Assessment_API.Models
{
    public class Story
    {

        public string By { get; set; }

        public int Descendants { get; set; }

        public int Id { get; set; }

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
