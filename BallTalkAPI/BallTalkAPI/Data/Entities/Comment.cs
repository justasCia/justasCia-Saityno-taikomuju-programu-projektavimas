using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Posted { get; set; } = DateTime.UtcNow;
        public int PostId { get; set; }
        public Post Post { get; set; }
        //public User User { get; set; }
    }
}
