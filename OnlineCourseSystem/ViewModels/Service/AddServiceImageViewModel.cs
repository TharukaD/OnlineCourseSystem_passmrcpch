﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourseSystem.ViewModels.Service
{
    public class AddServiceImageViewModel
    {
        public int ServiceId { get; set; }

        [Required]
        [DisplayName("File")]
        public IFormFile UploadedFile { get; set; }
    }
}
