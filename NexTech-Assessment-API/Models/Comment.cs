using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexTechAssessmentAPI.Models
{
    public class Comment
    {
        [Key]
        [MaxLength(10)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        public int Id { get; set; }

        public string By { get; set; }

        [NotMapped]
        public List<string> Kids { get; }

        public string Text { get; set; }

        public string Time { get; set; }

        public string Type { get; set; }

        public int Parent { get; set; }

        public Comment() { }
    }
}
