using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Models
{
    public class Topic
    {
        [Key]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
