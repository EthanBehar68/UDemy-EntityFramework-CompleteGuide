using System;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            // LINQ Syntax
            // This is a subset of the Extension Methods
            var query =
                from c in context.Courses
                where c.Name.Contains("c#")
                orderby c.Name
                select c;

            foreach(var course in query)
                Console.WriteLine(course.Name);

            Console.WriteLine(); // Space Provider

            // Extension Methods
            // This is more powerful.
            var courses = context.Courses
                .Where(c => c.Name.Contains("c#"))
                .OrderBy(c => c.Name);

            foreach (var course in courses)
                Console.WriteLine(course.Name);


            // Restriction
            var query1 = // Not at all a good variable name but for examples it's accept
                from c in context.Courses
                where c.Level == 1 /*&& c.Author.Id == 1*/
                select c;

            // Ordering
            var query2 = // Not at all a good variable name but for examples it's accept
                from c in context.Courses
                where c.Author.Id == 1
                orderby c.Level descending, c.Name
                select c;

            // Projection
            var query3 = // Not at all a good variable name but for examples it's accept
                from c in context.Courses
                where c.Author.Id == 1
                orderby c.Level descending, c.Name
                select new { Name = c.Name, Author = c.Author.Name };
        }
    }
}
