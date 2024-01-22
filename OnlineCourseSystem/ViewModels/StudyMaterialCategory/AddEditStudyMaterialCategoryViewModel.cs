using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourseSystem.ViewModels.StudyMaterialCategory
{
    public class AddEditStudyMaterialCategoryViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Priority")]
        public int Priority { get; set; }
    }
}
