using RepositoryPatternQueries.Core.Repositories;
using System;

namespace RepositoryPatternQueries.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        IAuthorRepository Authors { get; }
        int Complete();
    }
}