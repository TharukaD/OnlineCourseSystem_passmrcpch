using OnlineCourseSystem.ViewModels.Article;
using OnlineCourseSystem.ViewModels.CounterRecord;
using OnlineCourseSystem.ViewModels.HomePageBanner;
using OnlineCourseSystem.ViewModels.Service;

namespace OnlineCourseSystem.ViewModels
{
    public class HomePageViewModel
    {
        public List<ServiceViewModel> Services { get; set; }
        public List<HomePageBannerViewModel> Banners { get; set; }
        public List<CounterRecordViewModel> CounterRecords { get; set; }
        public List<ArticleViewModel> Articles { get; set; }
        public HomePageViewModel()
        {
            Services = new List<ServiceViewModel>();
            Banners = new List<HomePageBannerViewModel>();
            CounterRecords = new List<CounterRecordViewModel>();
            Articles = new List<ArticleViewModel>();
        }
    }
}
