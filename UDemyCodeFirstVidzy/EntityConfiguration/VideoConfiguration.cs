using System.Data.Entity.ModelConfiguration;

namespace CodeFirstVidzy.EntityConfiguration
{
    public class VideoConfiguration : EntityTypeConfiguration<Video>
    {
        public VideoConfiguration()
        {
            // Convention for organization (Alphabetical)

            // Table overrides at beginning (Alphabetical)

            // Primary Key overrides (Alphabetical)

            // Property Configurations (Alphabetical)
            Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(255);

            HasRequired(v => v.Genre)
            .WithMany(g => g.Videos)
            .HasForeignKey(v => v.GenreId)
            .WillCascadeOnDelete(false);

            // Relationship Configurations (Alphabetical)
            HasMany(v => v.Tags)
            .WithMany(t => t.Videos)
            .Map(m =>
            {
                m.ToTable("VideoTags");
                m.MapLeftKey("VideoId");
                m.MapRightKey("TagId");
            });
        }
    }
}
