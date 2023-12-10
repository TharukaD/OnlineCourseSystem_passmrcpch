using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCourseSystem.ViewModels.StudyMaterialType;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourseSystem.ViewModels.StudyMaterial
{
    public class AddEditStudyMaterialViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Short Description")]
        public string? ShortDescription { get; set; }

        [DisplayName("Long Description")]
        public string? LongDescription { get; set; }

        [Required]
        [DisplayName("Priority")]
        public int Priority { get; set; }

        [DisplayName("Published")]
        public bool IsPublished { get; set; }

        public string? Image { get; set; }

        [Required]
        [Display(Name = "Material Type")]
        public int StudyMaterialTypeId { get; set; }
        public SelectList StudyMaterialTypeSelectList { get; set; }

        public void Initialize(List<StudyMaterialTypeViewModel> materialTypeList)
        {
            StudyMaterialTypeSelectList = new SelectList(materialTypeList, "Id", "Name");
        }
    }
}
