using BallTalkAPI.Data;
using BallTalkAPI.Entities;
using BallTalkAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BallTalkAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BallTalkContext _context;

        public CommentRepository(BallTalkContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetCommentAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostAsync(int postId)
        {
            return await _context.Comments
                .Include(comment => comment.Post)
                .Where(comment => comment.Post.Id == postId)
                .ToListAsync();
        }
        public async Task<Comment> AddCommentAsync(Comment Comment)
        {
            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return Comment;
        }

        public async Task<Comment> PutCommentAsync(Comment Comment)
        {
            _context.Comments.Update(Comment);
            await _context.SaveChangesAsync();

            return Comment;
        }

        public async Task DeleteCommentAsync(Comment Comment)
        {
            _context.Comments.Remove(Comment);
            await _context.SaveChangesAsync();
        }
    }
}
