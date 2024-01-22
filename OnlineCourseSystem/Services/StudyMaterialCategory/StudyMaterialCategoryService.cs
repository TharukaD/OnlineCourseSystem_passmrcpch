namespace OnlineCourseSystem.Services.StudyMaterialCategory;

using Microsoft.EntityFrameworkCore;
using OnlineCourseSystem.Data;
using OnlineCourseSystem.Entities;
public class StudyMaterialCategoryService : IStudyMaterialCategoryService
{
    private ApplicationDbContext _context;
    public StudyMaterialCategoryService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<StudyMaterialCategory?> GetById(int id)
    {
        return await _context.StudyMaterialCategories.SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<StudyMaterialCategory>> GetAll()
    {
        return await _context.StudyMaterialCategories
            .OrderBy(r => r.Priority).ToListAsync();
    }

    public async Task<bool> Add(StudyMaterialCategory materialCategory)
    {
        _context.StudyMaterialCategories.Add(materialCategory);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var materialCategory = await _context.StudyMaterialCategories.FindAsync(id);
        if (materialCategory == null)
        {
            return false;
        }
        _context.StudyMaterialCategories.Remove(materialCategory);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.StudyMaterialCategories.FirstOrDefaultAsync(r => r.Name == name) != null)
                return true;
        }
        else
        {
            if (await _context.StudyMaterialCategories.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(StudyMaterialCategory category)
    {
        _context.Update(category);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HasAssignedToStudyMaterial(int categoryId)
    {
        var firstStudyMaterial = await _context.StudyMaterials.FirstOrDefaultAsync(r => r.StudyMaterialCategoryId == categoryId);
        if (firstStudyMaterial != null)
            return true;
        return false;
    }
}

