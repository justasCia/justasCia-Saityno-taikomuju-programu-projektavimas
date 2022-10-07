using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Data.DTOs
{
    public class TopicDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
