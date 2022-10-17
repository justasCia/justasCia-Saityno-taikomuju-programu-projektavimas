using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Data.DTOs.Topic
{
    public class AddOrUpdateTopicDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
