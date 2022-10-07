using BallTalkAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BallTalkAPI.Data
{
    public class BallTalkContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BallTalk;User ID=sa;Password=BallTalk123!@#");
        }
    }
}
