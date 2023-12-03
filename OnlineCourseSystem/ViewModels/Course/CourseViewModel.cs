namespace OnlineCourseSystem.ViewModels.Course
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Priority { get; set; }
        public int Price { get; set; }
        public int OldPrice { get; set; }
        public bool IsPublished { get; set; }
        public string ImageUrl { get; set; }
    }
}
