using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Data.DTOs.Topic
{
    public class TopicDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
