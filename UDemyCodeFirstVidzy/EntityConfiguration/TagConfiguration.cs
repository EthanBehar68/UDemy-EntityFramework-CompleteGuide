using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDemyCodeFirstVidzy.EntityConfiguration
{
    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            // Convention for organization (Alphabetical)

            // Table overrides at beginning (Alphabetical)

            // Primary Key overrides (Alphabetical)

            // Property Configurations (Alphabetical)
            Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(255);

            // Relationship Configurations (Alphabetical)
        }
    }
}
