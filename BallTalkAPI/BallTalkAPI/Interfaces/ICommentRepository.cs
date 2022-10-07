using BallTalkAPI.Entities;

namespace BallTalkAPI.Interfaces
{
    public interface ICommentRepository
    {
        public Task<IEnumerable<Comment>> GetCommentsByPostAsync(int postId);
        public Task<Comment> GetCommentAsync(int id);
        public Task<Comment> AddCommentAsync(Comment Comment);
        public Task<Comment> PutCommentAsync(Comment Comment);
        public Task DeleteCommentAsync(Comment Comment);
    }
}
