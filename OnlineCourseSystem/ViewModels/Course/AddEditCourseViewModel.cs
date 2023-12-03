using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourseSystem.ViewModels.Course
{
    public class AddEditCourseViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [DisplayName("Long Description")]
        public string LongDescription { get; set; }

        [Required]
        [DisplayName("Priority")]
        public int Priority { get; set; }

        [Required]
        [DisplayName("Price")]
        public int Price { get; set; }

        [Required]
        [DisplayName("OldPrice")]
        public int OldPrice { get; set; }

        [DisplayName("Published")]
        public bool IsPublished { get; set; }

        public string? Image { get; set; }
    }
}
