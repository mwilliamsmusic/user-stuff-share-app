
using Microsoft.EntityFrameworkCore;
using ssa_database.Models.Bookmark_Models;
using ssa_database.Models.Collect_Models;
using ssa_database.Models.Cool_Models;
using ssa_database.Models.Tag_Models;
using ssa_database.Models.User_Models;
using System.Reflection;

namespace user_stuff_share_app
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // scans a given assembly for all types that implement IEntityTypeConfiguration, and registers each one automatically. 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Collect> Collect { get; set; }

        public DbSet<Item> Item { get; set; }
        public DbSet<CoolCollect> CoolCollect{ get; set; }
        public DbSet<CoolCollectJoin> CoolCollectJoin { get; set; }
        public DbSet<CoolItem> CoolItem { get; set; }
        public DbSet<CoolItemJoin> CoolItemJoin { get; set; }
        public DbSet<BookmarkCollect> BookmarkCollect { get; set; }
       public  DbSet<BookmarkItem> BookmarkItem { get; set; }
        public DbSet<FollowUser> FollowUser { get; set; }
        public DbSet<TagCollectJoin> TagCollectJoin { get; set; }
        public DbSet<TagItemJoin> TagItemJoin { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<User> User { get; set; }
      
    }
}

