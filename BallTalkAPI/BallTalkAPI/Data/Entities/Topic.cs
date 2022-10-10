using Microsoft.EntityFrameworkCore;

namespace BallTalkAPI.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
