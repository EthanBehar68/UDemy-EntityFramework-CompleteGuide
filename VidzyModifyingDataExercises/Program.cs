using System;
using System.Linq;
using System.Data.Entity;

namespace VidzyModifyingDataExercises
{
    class Program
    {
        // TASKS
        /*
         * 1- Add a new video called “Terminator 1” with genre Action, release date 26 Oct, 1984, and Silver classification. 
         * Ensure the Action genre is not duplicated in the Genres table.
         * 
         * 2- Add two tags “classics” and “drama” to the database. 
         * Ensure if your method is called twice, these tags are not duplicated.
         * 
         * 3- Add three tags “classics”, “drama” and “comedy” to the video with Id 1 (The Godfather). 
         * Ensure the “classics” and “drama” tags are not duplicated in the Tags table. 
         * Also, ensure that if your method is called twice, these tags are not duplicated in VideoTags table.
         * 
         * 4- Remove the “comedy” tag from the the video with Id 1 (The Godfather).
         * 
         * 5- Remove the video with Id 1 (The Godfather). 
         * 
         * 6- Remove the genre with Id 2 (Action). Ensure all courses with this genre are deleted from the database. 
         */
        static void Main(string[] args)
        {
            // #1
            //AddVideo("Terminator 1", "Action", new DateTime(1984, 10, 26), Classification.Silver);

            // #2
            //AddTag("Classics");
            //AddTag("Drama");
            //AddTags("Classics", "Drama");

            // #3 - assumes video exists already
            //AddTagToExistingVideo(1, "Classics");
            //AddTagToExistingVideo(1, "Drama");
            //AddTagToExistingVideo(1, "Comedy");

            // #4 - assumes video exists already
            //RemoveTagFromExistingVideo(1, "Comedy");

            // #5
            //RemoveVideo(1);

            // #6
            RemoveGenre(2);
        }

        // #1
        private static void AddVideo(string name, string genre, DateTime releaseDate, Classification classification)
        {
            using (var context = new VidzyContext())
            {

                var existingGenre = context.Genres.Single(g => g.Name == genre);

                var newVideo = new Video
                {
                    Name = name,
                    GenreId = existingGenre.Id, // Using Foreign Key to ensure Genre isn't duplicated in Db
                    ReleaseDate = releaseDate,
                    Classification = classification
                };

                context.Videos.Add(newVideo);

                context.SaveChanges();
            }
        }

        // #2 Ethan Behar
        private static void AddTag(string newTag)
        {
            using (var context = new VidzyContext())
            {
                var existingTag = context.Tags.Any(t => t.Name == newTag);
                
                if(existingTag)
                {
                    Console.WriteLine(newTag + " already exists as a Tag in the Db.");
                    return;
                }

                var tag = new Tag
                {
                    Name = newTag
                };

                context.Tags.Add(tag);

                context.SaveChanges();

            }
        }
        // #2 Mosh Hamedani
        public static void AddTags(params string[] tagNames)
        {
            using (var context = new VidzyContext())
            {
                // We load the tags with the given names first, to prevent adding duplicates.
                var tags = context.Tags.Where(t => tagNames.Contains(t.Name)).ToList();

                foreach (var name in tagNames)
                {
                    if (!tags.Any(t => t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
                        context.Tags.Add(new Tag { Name = name });
                }

                context.SaveChanges();
            }
        }

        // #3 Ethan Behar
        public static void AddTagToExistingVideo(int videoId, string tagName)
        {
            using (var context = new VidzyContext())
            {
                var video = context.Videos.Find(videoId);

                var existingTag = context.Tags.FirstOrDefault(t => t.Name == tagName);
                if (existingTag != null)
                {
                    if (video.Tags.Contains(existingTag))
                    {
                        Console.WriteLine("Video already contains tag {0}", existingTag.Name);
                        return;
                    }

                    video.Tags.Add(existingTag);
                }
                else
                {
                    var newTag = new Tag
                    {
                        Name = tagName
                    };

                    context.Tags.Add(newTag);

                    video.AddTag(newTag);
                }

                context.SaveChanges();
            }
        }
        // #3 Mosh Hamedani
        public static void AddTagsToVideo(int videoId, params string[] tagNames)
        {
            using (var context = new VidzyContext())
            {
                // This technique with LINQ leads to 
                // 
                // SELECT FROM Tags WHERE Name IN ('classics', 'drama')
                var tags = context.Tags.Where(t => tagNames.Contains(t.Name)).ToList();

                // So, first we load tags with the given names from the database 
                // to ensure we won't duplicate them. Now, we loop through the list of
                // tag names, and if we don't have such a tag in the database, we add
                // them to the list.
                foreach (var tagName in tagNames)
                {
                    if (!tags.Any(t => t.Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase)))
                        tags.Add(new Tag { Name = tagName });
                }

                var video = context.Videos.Single(v => v.Id == videoId);

                tags.ForEach(t => video.AddTag(t));

                context.SaveChanges();
            }
        }

        // #4 Ethan Behar
        public static void RemoveTagFromExistingVideo(int videoId, string tagName)
        {
            using (var context = new VidzyContext())
            {
                // explicit load tag into context
                context.Tags.Where(t => t.Name == tagName).Load();

                var video = context.Videos.Find(videoId);

                video.RemoveTag(tagName);

                context.SaveChanges();
            }
        }
        // #4 Mosh Hamedani
        public static void RemoveTagsFromVideo(int videoId, params string[] tagNames)
        {
            using (var context = new VidzyContext())
            {
                // We can use explicit loading to only load tags that we're going to delete.
                context.Tags.Where(t => tagNames.Contains(t.Name)).Load();

                var video = context.Videos.Single(v => v.Id == videoId);

                foreach (var tagName in tagNames)
                {
                    // I've encapsulated the concept of removing a tag inside the Video class. 
                    // This is the object-oriented way to implement this. The Video class
                    // should be responsible for adding/removing objects to its Tags collection. 
                    video.RemoveTag(tagName);
                }

                context.SaveChanges();
            }
        }

        // #5 Ethan Behar
        public static void RemoveVideo(int videoId)
        {
            using (var context = new VidzyContext())
            {
                var video = context.Videos.Find(videoId);
                if (video == null) return;

                context.Videos.Remove(video);

                context.SaveChanges();
            }
        }
        // #5 Mosh Hamedani
        public static void RemoveVideoMosh(int videoId)
        {
            using (var context = new VidzyContext())
            {
                var video = context.Videos.SingleOrDefault(v => v.Id == videoId);
                if (video == null) return;

                context.Videos.Remove(video);
                context.SaveChanges();
            }
        }

        // #6 Ethan Behar
        public static void RemoveGenre(int genreId)
        {
            using(var context = new VidzyContext())
            {
                var genre = context.Genres.SingleOrDefault(g => g.Id == genreId);
                if (genre == null) return;

                var videosWithGenre = context.Videos.Where(v => v.GenreId == genre.Id);

                context.Videos.RemoveRange(videosWithGenre);
                context.Genres.Remove(genre);

                context.SaveChanges();
            }
        }
        // #6 Mosh Hamedani
        public static void RemoveGenreMosh(int genreId, bool enforceDeletingVideos)
        {
            using (var context = new VidzyContext())
            {
                var genre = context.Genres.Include(g => g.Videos).SingleOrDefault(g => g.Id == genreId);
                if (genre == null) return;

                if (enforceDeletingVideos)
                    context.Videos.RemoveRange(genre.Videos);

                context.Genres.Remove(genre);
                context.SaveChanges();
            }
        }

    }
}
