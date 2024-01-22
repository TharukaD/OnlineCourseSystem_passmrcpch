namespace OnlineCourseSystem.ViewModels.StudyMaterial
{
    public class StudyMaterialViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public int Priority { get; set; }
        public bool IsPublished { get; set; }
        public string ImageUrl { get; set; }
        public int StudyMaterialTypeId { get; set; }
        public string StudyMaterialTypeName { get; set; }
        public int? StudyMaterialCategoryId { get; set; }
        public string StudyMaterialCategoryName { get; set; }
    }
}
