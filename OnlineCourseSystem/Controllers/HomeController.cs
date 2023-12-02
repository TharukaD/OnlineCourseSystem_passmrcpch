using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseSystem.Services.Article;
using OnlineCourseSystem.Services.ArticleCategory;
using OnlineCourseSystem.Services.CounterRecord;
using OnlineCourseSystem.Services.HomePageBanner;
using OnlineCourseSystem.Services.Serivice;
using OnlineCourseSystem.Services.Tag;
using OnlineCourseSystem.ViewModels;
using OnlineCourseSystem.ViewModels.Article;
using OnlineCourseSystem.ViewModels.ArticleCategory;
using OnlineCourseSystem.ViewModels.CounterRecord;
using OnlineCourseSystem.ViewModels.HomePageBanner;
using OnlineCourseSystem.ViewModels.Service;
using OnlineCourseSystem.ViewModels.Tag;

namespace OnlineCourseSystem.Controllers
{
    public class HomeController : Controller
    {
        private IMapper _mapper;
        private readonly ILogger<HomeController> _logger;
        private IServiceService _service;
        private IHomePageBannerService _homePageBannerService;
        private ICounterRecordService _counterRecordService;
        private IArticleService _articleService;
        private ITagService _tagService;
        private IArticleCategoryService _articleCategoryService;

        public HomeController(
            IMapper mapper,
            ILogger<HomeController> logger,
            IServiceService service,
            IHomePageBannerService homePageBannerService,
            ICounterRecordService counterRecordService,
            IArticleService articleService,
            ITagService tagService,
            IArticleCategoryService articleCategoryService
        )
        {
            _mapper = mapper;
            _logger = logger;
            _service = service;
            _homePageBannerService = homePageBannerService;
            _counterRecordService = counterRecordService;
            _articleService = articleService;
            _tagService = tagService;
            _articleCategoryService = articleCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomePageViewModel();

            var services = await _service.GetAll();
            viewModel.Services = _mapper.Map<List<ServiceViewModel>>(services);

            var banners = await _homePageBannerService.GetAll();
            viewModel.Banners = _mapper.Map<List<HomePageBannerViewModel>>(banners);

            var counterRecords = await _counterRecordService.GetAll();
            viewModel.CounterRecords = _mapper.Map<List<CounterRecordViewModel>>(counterRecords);

            var articles = await _articleService.GetRandomArticles();
            viewModel.Articles = _mapper.Map<List<ArticleViewModel>>(articles);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Services()
        {
            var services = await _service.GetAll();
            var servicesList = _mapper.Map<List<ServiceViewModel>>(services);
            return View(servicesList);
        }

        [HttpGet]
        public async Task<IActionResult> Service(int id)
        {
            try
            {
                var service = await _service.GetById(id);

                if (service == null)
                {
                    return View("_ServiceNotFound");
                }

                var viewModel = _mapper.Map<ServiceViewModel>(service);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return PartialView("_ServiceLoadingError");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Articles()
        {
            var articles = await _articleService.GetAll();
            var articleList = _mapper.Map<List<ArticleViewModel>>(articles);
            var latestArticleList = articleList.OrderByDescending(r => r.IsPublished).Take(4).ToList();

            var tags = _mapper.Map<List<TagViewModel>>(await _tagService.GetAll());
            var categories = _mapper.Map<List<ArticleCategoryViewModel>>(await _articleCategoryService.GetAll());

            var viewModel = new ArticlesPageViewModel();
            viewModel.AllArticles = articleList;
            viewModel.LatestArticles = latestArticleList;
            viewModel.Tags = tags;
            viewModel.Categories = categories;

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Article(int id)
        {
            var articles = await _articleService.GetAll();
            var articleList = _mapper.Map<List<ArticleViewModel>>(articles);
            var latestArticleList = articleList.OrderByDescending(r => r.IsPublished).Take(4).ToList();

            var tags = _mapper.Map<List<TagViewModel>>(await _tagService.GetAll());
            var categories = _mapper.Map<List<ArticleCategoryViewModel>>(await _articleCategoryService.GetAll());

            var viewModel = new ArticlesPageViewModel();
            viewModel.AllArticles = articleList;
            viewModel.LatestArticles = latestArticleList;
            viewModel.Tags = tags;
            viewModel.Categories = categories;
            viewModel.Article = articleList.FirstOrDefault(r => r.Id == id);

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

    }
}
