using OnlineCourseSystem.ViewModels.StudyMaterialCategory;

namespace OnlineCourseSystem.ViewModels.StudyMaterialTopic
{
    public class StudyMaterialTopicTabViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TabId { get; set; }
        public string TabClass { get; set; }
        public string TabContentClass { get; set; }
        public string DataTarget { get; set; }
        public string AreaControl { get; set; }
        public List<StudyMaterialCategoryViewModel> Categories { get; set; }
    }


}
