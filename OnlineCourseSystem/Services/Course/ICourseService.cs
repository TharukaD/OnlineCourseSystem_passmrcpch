namespace OnlineCourseSystem.Services.Course;
using OnlineCourseSystem.Entities;
public interface ICourseService
{
    Task<Course?> GetById(int id);
    Task<IEnumerable<Course>> GetAll();
    Task<IEnumerable<Course>> GetAllPublished();
    Task<bool> Add(Course course);
    Task<bool> Update(Course course);
    Task<bool> Delete(int id);
    Task<bool> IsDublicate(int id, string name);
}
