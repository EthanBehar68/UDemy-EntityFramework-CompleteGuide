using System.Data.Entity;
using VidzyQueriesExercise.EntityTypeConfigurations;

namespace VidzyQueriesExercise
{
    public class VidzyContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }    
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public VidzyContext()
            : base ("Name = VidzyContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new VideoConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}