using System;
using System.Linq;
using System.Data.Entity;

namespace FluentAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            /* Lazy Loading Example
            var course = context.Courses.Single(c => c.Id == 2); // Courses are queried/loaded here b/c of Single
        
            foreach (var tag in course.Tags) // Tags are queried/loaded - this is lazy loading
                Console.WriteLine(tag.Name); // Loading is deferred until the data is used
            */

            /* N + 1 Problem Example

            var courses = context.Courses.ToList(); // Gets all courses in Db - 1 query

            foreach (var course in courses)
            {
                Console.WriteLine("{0} by {1}", course.Name, course.Author.Name); 
                // B/c Author isn't loaded in initial query
                // We end up making N (where n is count of courses)
                // number of queries to get Author.Name
                // Thus N + 1 queries
                // Although this example causes 5 queries b/c
                // There are 4 authors in the Db and they are stored in the cache of PlutoContext
                // But if there was a unique relationship among Authors and Courses we would have 1 author per course
                // Truly making it N + 1
                // In the end we need to be careful with lazy loading
            }
            */

            ///
            /// Eager Loading - Helps prevent N + 1 problem and opposite of Lazy Loading
            /// 1 query - one round trip to Db/

            /* 
            var courses = context.Courses.Include(c => c.Author).ToList();
            // .Include is Eager Loading
            // This includes the Author query with the Courses query - InnerJoin in this example

            foreach (var course in courses)
            {
                Console.WriteLine("{0} by {1}", course.Name, course.Author.Name);
            }
            */

            /* Multiple Level Eager Loading Examples
            // For Single Properties
            context.Courses.Include(c => c.Author);
            context.Courses.Include(c => c.Author.Address);

            // For Collection Properties
            context.Courses.Include(c => c.Tags);
            context.Courses.Include(c => c.Tags.Select(t => t.Moderator));
            */

            // Similarly with Lazy Loading there are down falls to Eager Loading
            // Too many include statement makes a heavy bloated Db query
            // And storing a lot of objects in memory

            ///
            /// Explicit Loading - Separate queries - Multiple round trips to Db
            /// 

            //var author = context.Authors.Include(a => a.Courses).Single(a => a.Id == 1); // Eager Loading

            var author = context.Authors.Single(a => a.Id == 1);

            // Explicity Loading - MSDN Way - Only works for 1 entry - Both result in 2 queries
            context.Entry(author).Collection(a => a.Courses).Query().Where(c => c.FullPrice == 0).Load();

            // Explicity Loading - Mosh's way - Both result in 2 queries
            context.Courses.Where(c => c.AuthorId == author.Id && c.FullPrice == 0).Load();
            // Mosh's way is cleaner and makes code easier to read / Don't need to know all the extra API calls

            foreach (var course in author.Courses)
            {
                Console.WriteLine("{0}", course.Name);
            }

            // Free courses for all authors
            var authors = context.Authors.ToList();
            var authorIds = authors.Select(a => a.Id);

            context.Courses.Where(c => authorIds.Contains(c.AuthorId) && c.FullPrice == 0).Load();

        }
    }
}
