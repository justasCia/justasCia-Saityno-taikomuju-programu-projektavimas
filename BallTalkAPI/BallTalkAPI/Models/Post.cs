using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Approved { get; set; } = false;
        public Topic Topic { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
