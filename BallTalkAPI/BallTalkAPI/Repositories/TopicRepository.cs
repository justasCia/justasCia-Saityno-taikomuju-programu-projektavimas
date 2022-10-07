using BallTalkAPI.Data;
using BallTalkAPI.Entities;
using BallTalkAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BallTalkAPI.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly BallTalkContext _context;

        public TopicRepository(BallTalkContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<Topic> GetTopicAsync(int id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public async Task<Topic> GetTopicByNameAsync(string name, bool includePosts = false)
        {
            if (includePosts)
            {
                await _context.Topics.Include(topic => topic.Posts).FirstOrDefaultAsync(topic => topic.Name == name);
            }
            return await _context.Topics.FirstOrDefaultAsync(topic => topic.Name == name);
        }

        public async Task<Topic> AddTopicAsync(Topic topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return topic;
        }

        public async Task<Topic> PutTopicAsync(Topic topic)
        {
            _context.Topics.Update(topic);
            await _context.SaveChangesAsync();
            
            return topic;
        }

        public async Task DeleteTopicAsync(Topic topic)
        {
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
        }
    }
}
