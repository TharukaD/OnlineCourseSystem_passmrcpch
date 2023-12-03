namespace OnlineCourseSystem.Services.Course;

using Microsoft.EntityFrameworkCore;
using OnlineCourseSystem.Data;
using OnlineCourseSystem.Entities;
public class CourseService : ICourseService
{
    private ApplicationDbContext _context;
    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Course?> GetById(int id)
    {
        return await _context.Courses.SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        return await _context.Courses
            .OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetAllPublished()
    {
        return await _context.Courses
            .Where(r => r.IsPublished)
            .OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<bool> Add(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return false;
        }
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.Courses.FirstOrDefaultAsync(r => r.Name == name) != null)
                return true;
        }
        else
        {
            if (await _context.Courses.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(Course course)
    {
        _context.Update(course);
        await _context.SaveChangesAsync();
        return true;
    }
}
