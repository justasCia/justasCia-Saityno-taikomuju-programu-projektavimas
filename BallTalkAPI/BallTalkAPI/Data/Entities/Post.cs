using BallTalkAPI.Auth;
using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Entities
{
    public class Post : IUserOwnedResource
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Approved { get; set; } = false;
        public DateTime Posted { get; set; } = DateTime.UtcNow;

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string UserId { get; set; }
    }
}
