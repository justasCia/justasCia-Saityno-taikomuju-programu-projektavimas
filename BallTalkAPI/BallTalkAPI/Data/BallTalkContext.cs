using BallTalkAPI.Auth.Entities;
using BallTalkAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BallTalkAPI.Data
{
    public class BallTalkContext : IdentityDbContext<User>
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BallTalkContext(DbContextOptions<BallTalkContext> options) : base(options)
        {
        }
    }
}
