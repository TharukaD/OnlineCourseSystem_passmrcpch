namespace OnlineCourseSystem.Services.StudyMaterialTopic;
using OnlineCourseSystem.Entities;
public interface IStudyMaterialTopicService
{
    Task<StudyMaterialTopic?> GetById(int id);
    Task<IEnumerable<StudyMaterialTopic>> GetAll();
    Task<IEnumerable<StudyMaterialTopic>> GetAllWithCategories();
    Task<bool> Add(StudyMaterialTopic category);
    Task<bool> Update(StudyMaterialTopic category);
    Task<bool> Delete(int id);
    Task<bool> IsDublicate(int id, string name);
    Task<bool> HasAssignedToStudyMaterial(int categoryId);
}
