using C971.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Repositories;

public interface IAssessmentRepository : IRepository<Assessment>
{
    Task<List<Assessment>> GetByCourseAsync(int courseId);
}
