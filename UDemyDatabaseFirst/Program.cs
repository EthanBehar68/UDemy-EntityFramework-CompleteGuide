using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDemyDatabaseFirst
{

    public enum Level : byte
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3
    }

    class Program
    {
        static void Main(string[] args)
        {

            ///
            /// Section 3 Examples
            ///
            //var context = new PlutoDbContext();

            //var courses = context.GetCourses();
            //foreach(var c in courses)
            //{
            //    Console.WriteLine(c.Title);
            //}

            //var course = new Course();
            //course.Level = Level.Beginner; // 1

            // Section 3 Exercise '1st Iteration'
            // See '_Extras' for documents regarding this Exercise
            //var context = new VidzyDbContext();

            //var comedyGenre = context.Genres.SingleOrDefault(g => g.Name == "Comedy");
            //var actionGenre = context.Genres.SingleOrDefault(g => g.Name == "Action");

            //context.AddVideo("Hang Over", new DateTime(2009, 6, 2), comedyGenre.Name);
            //context.AddVideo("Die Hard", new DateTime(1988, 7, 15), actionGenre.Name);
            //context.AddVideo("Matrix", new DateTime(1999, 3, 31), actionGenre.Name);
            //context.AddVideo("Deadpool 2", new DateTime(2018, 5, 10), comedyGenre.Name);

            //var familyGenre = context.Genres.SingleOrDefault(g => g.Name == "Family");

            //context.AddVideo("Toy Story", new DateTime(1995, 11, 22), familyGenre.Name);

            //context.SaveChanges();
        }
    }
}
