using OnlineCourseSystem.ViewModels.Article;
using OnlineCourseSystem.ViewModels.ArticleCategory;
using OnlineCourseSystem.ViewModels.Tag;

namespace OnlineCourseSystem.ViewModels
{
    public class ArticlesPageViewModel
    {
        public List<ArticleViewModel> AllArticles { get; set; }
        public List<ArticleViewModel> LatestArticles { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<ArticleCategoryViewModel> Categories { get; set; }
        public ArticleViewModel Article { get; set; }
    }
}
