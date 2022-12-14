using BallTalkAPI.Data;
using BallTalkAPI.Entities;
using BallTalkAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BallTalkAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BallTalkContext _context;

        public PostRepository(BallTalkContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByTopicAsync(int topicId)
        {
            return await _context.Posts
                .Include(post => post.Topic)
                .Where(post => post.Topic.Id == topicId)
                .ToListAsync();
        }

        public async Task<Post> GetPostAsync(int id, bool includeComments = false)
        {
            if (includeComments)
            {
                return await _context.Posts.Include(post => post.Comments).FirstOrDefaultAsync(post => post.Id == id);
            }
            return await _context.Posts.FindAsync(id);
        }

        public async Task<Post> AddPostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<Post> PutPostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}
