namespace BallTalkAPI.Data.DTOs.Post
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Approved { get; set; }
        public DateTime Posted { get; set; }
    }
}
