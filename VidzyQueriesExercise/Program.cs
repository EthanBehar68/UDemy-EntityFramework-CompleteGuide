using System;
using System.Linq;
using System.Data.Entity;

namespace VidzyQueriesExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lazy and Eager Loading Solution
            //var context = new VidzyContext();

            //var videos = context.Videos.Include(v => v.Genre).ToList();

            //foreach (var video in videos)
            //{
            //    Console.WriteLine("{0} - {1}", video.Name, video.Genre.Name); // Causes null ref exception b/c Lazy Loading is not implemented
            //    // Adding virtual to Genre and ToList() enables lazy loading but creates N + 1 situation
            //    // Adding .Include(v => v.Genre) enables eager loading and elminates N + 1 sitatuion
            //}

            // Explicit Loading Solution
            var context = new VidzyContext();

            var videos = context.Videos.ToList();
            context.Genres.Load();

            foreach (var video in videos)
            {
                Console.WriteLine("{0} - {1}", video.Name, video.Genre.Name);
            }
        }
    }
}
