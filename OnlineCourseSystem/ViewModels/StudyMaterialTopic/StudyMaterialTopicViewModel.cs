using OnlineCourseSystem.ViewModels.StudyMaterialCategory;

namespace OnlineCourseSystem.ViewModels.StudyMaterialTopic
{
    public class StudyMaterialTopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public List<StudyMaterialCategoryViewModel> StudyMaterialCategories { get; set; }
    }
}
