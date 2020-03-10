using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UDemyCodeFirstDatabase
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }

        public Author()
        {
            Courses = new Collection<Course>();
        }
    }
}
