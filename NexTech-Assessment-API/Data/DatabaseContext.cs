using Microsoft.EntityFrameworkCore;
using NexTech_Assessment_API.Models;

namespace NexTech_Assessment_API.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
          : base(options)
        {
        }

        public virtual DbSet<Story> StaticTestStories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
