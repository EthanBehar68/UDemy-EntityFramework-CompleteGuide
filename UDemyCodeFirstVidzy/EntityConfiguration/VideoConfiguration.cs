using System.Data.Entity.ModelConfiguration;

namespace UDemyCodeFirstVidzy.Configuration
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
        }
    }
}
