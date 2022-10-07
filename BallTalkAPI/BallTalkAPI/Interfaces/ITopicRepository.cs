using BallTalkAPI.Entities;

namespace BallTalkAPI.Interfaces
{
    public interface ITopicRepository
    {
        public Task<IEnumerable<Topic>> GetTopicsAsync();
        public Task<Topic> GetTopicAsync(int id);
        public Task<Topic> GetTopicByNameAsync(string name, bool includePosts = false);
        public Task<Topic> AddTopicAsync(Topic topic);
        public Task<Topic> PutTopicAsync(Topic topic);
        public Task DeleteTopicAsync(Topic topic);
    }
}
