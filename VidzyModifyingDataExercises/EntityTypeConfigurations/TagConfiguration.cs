using System.Data.Entity.ModelConfiguration;

namespace VidzyModifyingDataExercises.EntityTypeConfigurations
{
    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
