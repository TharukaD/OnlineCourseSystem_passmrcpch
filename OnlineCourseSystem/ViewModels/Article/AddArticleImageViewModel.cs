﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourseSystem.ViewModels.Article
{
    public class AddArticleImageViewModel
    {
        public int ArticleId { get; set; }

        [Required]
        [DisplayName("File")]
        public IFormFile UploadedFile { get; set; }
    }
}
