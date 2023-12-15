using Microsoft.EntityFrameworkCore;

namespace ApiIntro.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired().HasMaxLength(25);
            base.OnModelCreating(modelBuilder);
        }
    }
}
