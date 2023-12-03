using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourseSystem.ViewModels.Course
{
    public class AddCourseImageViewModel
    {
        public int CourseId { get; set; }

        [Required]
        [DisplayName("File")]
        public IFormFile UploadedFile { get; set; }
    }
}
