using System;
using System.Linq;

namespace VidzyExercise
{
    class Program
    {
        static void Main(string[] args)
        {

            var context = new VidzyContext();

            /*
            Task 1 Action movies sorted by name
            Expected:
            Die Hard
            Terminator 2: Judgment Day 
            The Dark Knight 
            */
            var query = context.Videos
                .Where(v => v.Genre.Name == "Action")
                .OrderBy(v => v.Name);

            foreach (var v in query)
                Console.WriteLine(v.Name);
            Console.WriteLine();

            /* Task 2
            Gold drama movies sorted by release date(newest first)
            Expected:
            The Shawshank Redemption
            Schindlre's List
            */
            var query2 = context.Videos
                .Where(v => v.Classification == Classification.Gold)
                .OrderByDescending(v => v.ReleaseDate);

            foreach (var v in query2)
                Console.WriteLine(v.Name);
            Console.WriteLine();

            /* Task 3 
            All movies projected into an anonymous type with two properties: MovieName and Genre
            Expected:
            The Godfather Drama
            The Shawshank Redemption Drama
            Schindlre’s List Drama
            The Hangover Comedy
            Anchorman Comedy
            Die Hard Action
            The Dark Knight Action
            Terminator 2: Judgment Day Action
            */
            var query3 = context.Videos
                .Select(v => new { MovieName = v.Name, Genre = v.Genre.Name });

            foreach (var v in query3)
                Console.WriteLine("{0} {1}", v.MovieName, v.Genre);
            Console.WriteLine();

            /* Task 4
            All movies grouped by their classification: 
            Project the group into a new anonymous type with two properties: 
            Classification(string), Movies(IEnumerable<Video>).
            For each group, display the classification and list of videos in that class sorted alphabetically.
            Expected: 
            Classification: Silver 
                Anchorman
            Classification: Gold 
                Die Hard 
                Schindlre’s List    
                The Dark Knight 
                The Hangover 
                The Shawshank Redemption
            Classification: Platinum 
                Terminator 2: Judgment Day    
                The Godfather
            */
            // My Solution
            //var query4 = context.Videos
            //.Select(v => new 
            //{ 
            //  Classification = v.Classification,
            //  Name = v.Name
            //})
            //.OrderBy(v => v.Name) //AO for Anonymous Object / Type
            //.GroupBy(v => v.Classification); // Must order before grouping
            //foreach (var aoGroup in query4)
            //{
            //    Console.WriteLine("Classification: " + aoGroup.Key);
            //    foreach (var ao in aoGroup)
            //        Console.WriteLine("\t" + ao.Name);
            //}
            //Console.WriteLine();

            // Mosh's Solution
            var query4 = context.Videos
                .GroupBy(v => v.Classification)
                .Select(g => new
                {
                    Classification = g.Key.ToString(),
                    Videos = g.OrderBy(v => v.Name)
                }); ;

            foreach (var ao in query4)
            {
                Console.WriteLine("Classification: " + ao.Classification);
                foreach (var v in ao.Videos)
                    Console.WriteLine("\t" + v.Name);
            }
            Console.WriteLine();

            /* Task 5
            List of classifications sorted alphabetically and count of videos in them. 
            Expected: 
            Gold (5)
            Platinum (2)
            Silver (1)
            */
            var query5 = context.Videos
                .GroupBy(v => v.Classification)
                .Select(g => new
                {
                    ClassificationName = g.Key.ToString(),
                    VideoCount = g.Count()
                })
                .OrderBy(c => c.ClassificationName);

            foreach (var group in query5)
            {
                Console.WriteLine("{0}({1})", group.ClassificationName, group.VideoCount);
            }
            Console.WriteLine();

            /* Task 6
            List of genres and number of videos they include, sorted by the number of videos. 
            Genres with the highest number of videos come first.
            Expected: 
            Action (3)
            Drama (3)
            Comedy (2)
            Horror (0)
            Thriller (0)
            Family (0)
            Romance (0)
            */
            // My Solution - Missing 0 Count genres
            //var query6 = context.Videos
            //    .GroupBy(v => v.Genre.Name)
            //    .Select(g => new
            //    {
            //        GenreName = g.Key.ToString(),
            //        VideosCount = g.Count()
            //    })
            //    .OrderByDescending(g => g.VideosCount);
            //foreach (var group in query6)
            //{
            //    Console.WriteLine("{0}({1})", group.GenreName, group.VideosCount);
            //}
            //Console.WriteLine();
            // Mosh's Solution
            var query6 = context.Genres
                .GroupJoin(context.Videos, g => g.Id, v => v.GenreId, (genre, videos) => new
                {
                    GenreName = genre.Name,
                    VideosCount = videos.Count()
                })
                .OrderByDescending(g => g.VideosCount);

            foreach (var group in query6)
            {
                Console.WriteLine("{0}({1})", group.GenreName, group.VideosCount);
            }
        }
    }
}
