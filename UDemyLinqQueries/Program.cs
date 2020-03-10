using System;
using System.Collections.Generic;
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

            foreach (var course in query)
                Console.WriteLine(course.Name);
            Console.WriteLine(); // Space Provider

            // Extension Methods
            // This is more powerful.
            var courses = context.Courses
                .Where(c => c.Name.Contains("c#"))
                .OrderBy(c => c.Name);

            foreach (var course in courses)
                Console.WriteLine(course.Name);
            Console.WriteLine(); // Space Provider


            ///
            ///
            /// LINQ SYNTAX IN-DEPTH
            ///
            ///

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

            // Grouping
            var query4 = // Not at all a good variable name but for examples it's accept
                from c in context.Courses
                group c by c.Level
                into g
                select g;

            foreach (var group in query4)
            {
                Console.WriteLine(group.Key);

                foreach (var course in group)
                    Console.WriteLine("\t{0}", course.Name);
            }
            Console.WriteLine(); // Space Provider

            // Aggregate Count
            foreach (var group in query4)
            {
                Console.WriteLine("{0} {1}", group.Key, group.Count());
            }
            Console.WriteLine(); // Space Provider

            // Joining
            // Joining via navigation properties - Converts into Inner Join
            var query5 =
                from c in context.Courses
                select new { CourseName = c.Name, AuthorName = c.Author.Name };

            // Inner Join - Used when navigation properties do not exist / when no relationships between objects
            var query6 =
                from c in context.Courses
                join a in context.Authors on c.AuthorId equals a.Id
                select new { CourseName = c.Name, AuthorName = a.Name };

            // Group Join
            // Used when left join would be used in SQL
            var query7 =
                 from a in context.Authors
                 join c in context.Courses on a.Id equals c.AuthorId into g
                 select new { AuthorName = a.Name, Courses = g.Count() };

            foreach (var group in query7)
                Console.WriteLine("{0} ({1})", group.AuthorName, group.Courses);
            Console.WriteLine(); // Space Provider

            // Cross Join
            var query8 =
                from a in context.Authors
                from c in context.Courses
                select new { AuthorName = a.Name, CourseName = c.Name };

            foreach (var group in query8)
                Console.WriteLine("{0} - {1}", group.AuthorName, group.CourseName);
            Console.WriteLine(); // Space Provider

            ///
            ///
            /// LINQ EXTENSION IN-DEPTH
            ///
            ///

            // Restrction
            var query9 =
                context.Courses
                .Where(c => c.Level == 1);

            // Ordering 
            var query10 =
                context.Courses
                .Where(c => c.Level == 1)
                .OrderByDescending(c => c.Name)
                .ThenBy(c => c.Level);

            // Projection
            var query11 =
                context.Courses
                .Where(c => c.Level == 1)
                .OrderByDescending(c => c.Name)
                .ThenBy(c => c.Level)
                .Select(c => c.Tags);

            // Select of list becomes lists of list - ie hierarchical object
            // In this case you must likely want to flatten like below example
            foreach (var course in query11)
            {
                foreach (var tag in course)
                {
                    Console.WriteLine(tag.Name);
                }
            }
            Console.WriteLine(); // Space Provider


            var query12 =
                context.Courses
                .Where(c => c.Level == 1)
                .OrderByDescending(c => c.Name)
                .ThenBy(c => c.Level)
                .SelectMany(c => c.Tags); // Flattens the list of lists

            foreach (var tag in query12)
            {
                Console.WriteLine(tag.Name);
            }
            Console.WriteLine(); // Space Provider

            // Set Operators
            var query13 =
                context.Courses
                .Where(c => c.Level == 1)
                .OrderByDescending(c => c.Name)
                .ThenBy(c => c.Level)
                .SelectMany(c => c.Tags)// Flattens the list of lists
                .Distinct();

            foreach (var tag in query13)
            {
                Console.WriteLine(tag.Name);
            }
            Console.WriteLine(); // Space Provider

            // Grouping
            var query14 = context.Courses
                .GroupBy(c => c.Level);

            foreach (var group in query14)
            {
                Console.WriteLine("Key: " + group.Key);
                foreach (var course in group)
                    Console.WriteLine("\t" + course.Name);
            }
            Console.WriteLine(); // Space Provider

            // Joining
            // Inner Join - when no relationship between objects
            var query15 = context.Courses
                .Join(context.Authors,
                    c => c.AuthorId,
                    a => a.Id,
                    (course, author) => new
                    {
                        CourseName = course.Name,
                        AuthorName = author.Name
                    });

            // Group Join - Useful when left join in SQL is required and use Count or GroupBy
            var query16 = context.Authors
                .GroupJoin(context.Courses,
                a => a.Id,
                c => c.AuthorId,
                (author, courses1) => new
                {
                    AuthorName = author.Name, // string
                    Courses = courses1 // list of courses
                    //Author = author, // Author
                    //Courses = courses.Count() // int
                });

            // Cross Join - Every combo of two lists
            var query17 = context.Authors
                .SelectMany(a => context.Courses,
                (author, course) => new
                {
                    AuthorName = author.Name,
                    CourseName = course.Name
                });

            // Partitioning - good for pagination 
            var query18 = context.Courses.Skip(10).Take(10);

            // Element Operators - Single/First elements of list
            // Similarly there is Last/LastOrDefault - can't be applied to Sql Database 
            // Similaryl there is Single/SingleOfDefault - multiple records return an exception
            var query19 = context.Courses.OrderBy(c => c.Level).First(); // returns exception if list is empty

            var query20 = context.Courses.FirstOrDefault(c => c.FullPrice > 100); // returns default value if list is empty

            //Quantifying - returns bool
            var query21 = context.Courses.All(c => c.FullPrice > 10);
            var query22 = context.Courses.Any(c => c.Level == 1);

            //Aggrgating
            var query23 = context.Courses.Count();
            var query24 = context.Courses.Max(c => c.FullPrice);
            var query25 = context.Courses.Min(c => c.FullPrice);
            var query26 = context.Courses.Average(c => c.FullPrice);
            var query27 = context.Courses
                .Where(c => c.Level == 1).Count();

            ///
            ///
            /// Deferred Execution
            /// Query Executions aren't ran on code that creates them
            /// They are ran on the following
            /// 1 - Iterating over query variable
            /// 2 - Calling ToList, ToArray, ToDictionary
            /// 3 - First, Last, Single, Count, Max, Min, Average

            var query28 = context.Courses; // QUERY IS NOT EXECUTED HERE!

            var filtered = courses.Where(c => c.Level == 1); // QUERY IS NOT EXECUTED HERE!
            
            var sorted = filtered.OrderBy(c => c.Name); // QUERY IS NOT EXECUTED HERE!

            foreach (var c in query28)
                Console.WriteLine(c); // QUERY IS EXECUTED HERE!
            Console.WriteLine(); // Space Provider

            // Immediately Execution Query

            // throws NotSupportedException
            //var query29 = context.Courses.Where(c => c.IsBeginnerCourse == true);

            // Not best for performance b/c loads whole Db just to filter
            var query29 = context.Courses.ToList().Where(c => c.IsBeginnerCourse == true); 

            foreach (var c in query28)
                Console.WriteLine(c); // QUERY IS EXECUTED HERE!
            Console.WriteLine(); // Space Provider

            // IQueryable
            IQueryable<Course> query30 = context.Courses;
            var filtered1 = query30.Where(c => c.Level == 1); // filter is part of the query
            // Loads only filtered courses into memory! This allows you to extend queries

            foreach (var c in filtered1)
                Console.WriteLine(c.Name);
            Console.WriteLine(); // Space Provider

            // IEnumerable 
            IEnumerable<Course> query31 = context.Courses;
            var filtered2 = query30.Where(c => c.Level == 1); // filter is NOT part of the query
            // Loads all courses into memory and then filters them!

            foreach (var c in filtered1)
                Console.WriteLine(c.Name);
            Console.WriteLine(); // Space Provider
        }
    }
}
