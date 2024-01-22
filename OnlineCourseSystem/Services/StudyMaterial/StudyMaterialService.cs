namespace OnlineCourseSystem.Services.StudyMaterial;

using Microsoft.EntityFrameworkCore;
using OnlineCourseSystem.Data;
using OnlineCourseSystem.Entities;
public class StudyMaterialService : IStudyMaterialService
{
    private ApplicationDbContext _context;
    public StudyMaterialService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<StudyMaterial?> GetById(int id)
    {
        return await _context.StudyMaterials.Include(r => r.StudyMaterialType).SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<StudyMaterialType>> GetAllTypes()
    {
        return await _context.StudyMaterialTypes.ToListAsync();
    }

    public async Task<IEnumerable<StudyMaterial>> GetAll()
    {
        return await _context.StudyMaterials
            .Include(r => r.StudyMaterialType)
            .Include(r => r.StudyMaterialCategory).DefaultIfEmpty()
            .OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<IEnumerable<StudyMaterial>> GetAllPublished()
    {
        return await _context.StudyMaterials
            .Include(r => r.StudyMaterialType)
            .Where(r => r.IsPublished)
            .OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<bool> Add(StudyMaterial material)
    {
        _context.StudyMaterials.Add(material);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var material = await _context.StudyMaterials.FindAsync(id);
        if (material == null)
        {
            return false;
        }
        _context.StudyMaterials.Remove(material);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.StudyMaterials.FirstOrDefaultAsync(r => r.Title == name) != null)
                return true;
        }
        else
        {
            if (await _context.StudyMaterials.FirstOrDefaultAsync(r => r.Title == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(StudyMaterial studyMaterial)
    {
        _context.Update(studyMaterial);
        await _context.SaveChangesAsync();
        return true;
    }
}
