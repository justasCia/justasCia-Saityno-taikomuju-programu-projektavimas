using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
