using C971.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Repositories;

public interface ICourseRepository : IRepository<Course>
{
    Task<List<Course>> GetByTermAsync(int termId);
}
