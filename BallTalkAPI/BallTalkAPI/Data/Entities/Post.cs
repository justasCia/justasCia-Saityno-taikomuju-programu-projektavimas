using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Approved { get; set; } = false;
        public DateTime Posted { get; set; } = DateTime.Now;

        public Topic Topic { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
