using System.Collections.Generic;

namespace FluentAPI
{
    public class Course
    {
        public Course()
        {
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public float FullPrice { get; set; }

        public virtual Author Author { get; set; }
        public int AuthorId { get; set; }
        
        public virtual ICollection<Tag> Tags { get; set; } // virtual enables lazy loading
        // Proxy is created under the hood and mutates the getter - System.Data.Entity.DynamicProxies
        // Useful when loading will be costly
        // Use in desktop applications
        // Avoid in web applications - causes unnecessary roundtrips between server and client

        public Cover Cover { get; set; }
    }
}