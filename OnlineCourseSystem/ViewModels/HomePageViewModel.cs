using OnlineCourseSystem.ViewModels.Service;

namespace OnlineCourseSystem.ViewModels
{
    public class HomePageViewModel
    {
        public List<ServiceViewModel> Services { get; set; }
        public HomePageViewModel()
        {
            Services = new List<ServiceViewModel>();
        }
    }
}
