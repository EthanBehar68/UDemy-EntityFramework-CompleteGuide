using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstVidzy.EntityConfiguration
{
    class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            // Convention for organization (Alphabetical)

            // Table overrides at beginning (Alphabetical)

            // Primary Key overrides (Alphabetical)

            // Property Configurations (Alphabetical)
            Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(255);

            // Relationship Configurations (Alphabetical)
        }
    }
}
