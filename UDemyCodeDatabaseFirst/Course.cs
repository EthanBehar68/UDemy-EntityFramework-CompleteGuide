using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UDemyCodeFirstDatabase
{
    public class Course
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CourseLevel Level { get; set; }
        public float FullPrice { get; set; }
        public Author Authot { get; set; }
        public IList<Tag> Tags { get; set; }

        public Course()
        {
            Tags = new Collection<Tag>();
        }
    }
}
