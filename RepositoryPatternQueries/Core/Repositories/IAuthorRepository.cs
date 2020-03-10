using RepositoryPatternQueries.Core.Domain;

namespace RepositoryPatternQueries.Core.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author GetAuthorWithCourses(int id);
    }
}