using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;

namespace CodeFirstExistingDatabase.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirstExistingDatabase.PlutoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CodeFirstExistingDatabase.PlutoContext context)
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
