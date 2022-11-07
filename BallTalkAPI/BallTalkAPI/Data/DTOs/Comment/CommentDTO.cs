namespace BallTalkAPI.Data.DTOs.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Posted { get; set; }
        public string UserId { get; set; }
    }
}
