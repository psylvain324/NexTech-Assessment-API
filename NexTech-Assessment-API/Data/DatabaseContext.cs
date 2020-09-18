using Microsoft.EntityFrameworkCore;
using NexTechAssessmentAPI.Models;

namespace NexTechAssessmentAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
          : base(options)
        {
        }

        public virtual DbSet<Story> TestStories { get; set; }
        public virtual DbSet<Comment> TestComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Story>()
                .HasKey(s => s.StoryId)
                .HasName("PrimaryKey_StoryId");

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.CommentId)
                .HasName("PrimaryKey_CommentId");
        }

    }
}
