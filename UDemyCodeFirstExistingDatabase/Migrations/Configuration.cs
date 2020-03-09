using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;

namespace UDemyCodeFirstExistingDatabase.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UDemyCodeFirstExistingDatabase.PlutoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UDemyCodeFirstExistingDatabase.PlutoContext context)
        {
            //Seeding 1 Author into DB
            //context.Authors.AddOrUpdate(a => a.Name,
            //    new Author
            //    {
            //        Name = "Author 1",
            //        Courses = new Collection<Course>()
            //        {
            //            new Course() {
            //                Name = "Course for Author 1",
            //                Description = "Description"
            //            }
            //        }
            //    });
        }
    }
}
