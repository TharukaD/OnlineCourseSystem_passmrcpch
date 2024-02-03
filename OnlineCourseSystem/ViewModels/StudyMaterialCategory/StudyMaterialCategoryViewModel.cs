namespace OnlineCourseSystem.ViewModels.StudyMaterialCategory
{
    public class StudyMaterialCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TopicId { get; set; }
        public string TopicName { get; set; }
        public int Priority { get; set; }
    }
}
