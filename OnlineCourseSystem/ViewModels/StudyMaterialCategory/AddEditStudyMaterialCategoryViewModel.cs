using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCourseSystem.ViewModels.StudyMaterialTopic;
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


        [Display(Name = "Topic")]
        public int? TopicId { get; set; }
        public SelectList StudyMaterialTopicSelectList { get; set; }

        public void Initialize(List<StudyMaterialTopicViewModel> topicList)
        {
            StudyMaterialTopicSelectList = new SelectList(topicList, "Id", "Name");
        }

    }
}
