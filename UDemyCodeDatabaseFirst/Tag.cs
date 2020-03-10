using System.Collections.Generic;

namespace UDemyCodeFirstDatabase
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }

        public Tag()
        {
            Courses = new Collection<Course>();
        }
    }
}
