using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourseSystem.ViewModels.StudyMaterial
{
    public class AddStudyMaterialFileViewModel
    {
        public int StudyMaterialId { get; set; }
        public string FileType { get; set; }

        [Required]
        [DisplayName("File")]
        public IFormFile UploadedFile { get; set; }
    }
}
