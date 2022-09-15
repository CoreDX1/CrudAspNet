using Microsoft.EntityFrameworkCore;

namespace aspnetServer.Data
{
    internal sealed class AppDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) => dbContextOptionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");
        
        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            Post[] postToSeed = new Post[6];
            for(int i = 1; i <= 6; i++)
            {
                postToSeed[i - 1] = new Post
                {
                    PostId = i,
                    Title = $"Post {i}",
                    Content = $"This is post {i} and it has some very interesting content"
                };
            }

            modelBuider.Entity<Post>().HasData(postToSeed);
        }
    }
}
