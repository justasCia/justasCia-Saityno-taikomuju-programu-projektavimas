using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Topic
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
