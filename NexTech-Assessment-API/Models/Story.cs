using System.Collections.Generic;

namespace TechAssessment.Models
{
    /*
     *   "by" : "krnbatra",
          "descendants" : 0,
          "id" : 24375946,
          "score" : 1,
          "time" : 1599234128,
          "title" : "I created my side project in a day and landed on first page on ProductHunt",
          "type" : "story",
          "url" : "https://medium.com/@karandeepbatra/how-i-created-a-telegram-bot-in-a-day-and-landed-on-the-first-page-on-producthunt-2155ed8b0a67"
     */
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
