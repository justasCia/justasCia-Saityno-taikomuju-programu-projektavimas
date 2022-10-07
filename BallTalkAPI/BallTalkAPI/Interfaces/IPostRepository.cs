using BallTalkAPI.Entities;

namespace BallTalkAPI.Interfaces
{
    public interface IPostRepository
    {
        public Task<IEnumerable<Post>> GetPostsAsync();
        public Task<IEnumerable<Post>> GetPostsByTopicAsync(string topicName);
        public Task<Post> GetPostAsync(int id, bool includeComments = false);
        public Task<Post> AddPostAsync(Post post);
        public Task<Post> PutPostAsync(Post post);
        public Task DeletePostAsync(Post post);
    }
}
