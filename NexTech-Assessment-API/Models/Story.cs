using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexTechAssessmentAPI.Models
{
    public class Story
    {
        [Key]
        [MaxLength(10)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoryId { get; set; }

        public int Id { get; set; }

        public string By { get; set; }

        public int Descendants { get; set; }

        [NotMapped]
        public List<string> Kids { get; set; }

        public int Score { get; set; }

        public string Time { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public string Parent { get; set; }

        [NotMapped]
        public virtual Comment Comment { get; set; }

        public Story()
        {


        }
    }

}
