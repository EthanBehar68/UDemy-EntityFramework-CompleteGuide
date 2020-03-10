using System.Data.Entity;
using CodeFirstVidzy.EntityConfiguration;

namespace CodeFirstVidzy
{
    public class VidzyDbContext: DbContext
    {
        public VidzyDbContext()
            :base("Name=DefaultConnection")
        {

        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new VideoConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
