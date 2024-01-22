namespace OnlineCourseSystem.Services.StudyMaterialCategory;
using OnlineCourseSystem.Entities;
public interface IStudyMaterialCategoryService
{
    Task<StudyMaterialCategory?> GetById(int id);
    Task<IEnumerable<StudyMaterialCategory>> GetAll();
    Task<bool> Add(StudyMaterialCategory category);
    Task<bool> Update(StudyMaterialCategory category);
    Task<bool> Delete(int id);
    Task<bool> IsDublicate(int id, string name);
    Task<bool> HasAssignedToStudyMaterial(int categoryId);
}
