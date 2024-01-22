using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseSystem.Entities;
using OnlineCourseSystem.Services.Article;
using OnlineCourseSystem.Services.ArticleCategory;
using OnlineCourseSystem.Services.CounterRecord;
using OnlineCourseSystem.Services.Course;
using OnlineCourseSystem.Services.EmailService;
using OnlineCourseSystem.Services.HomePageBanner;
using OnlineCourseSystem.Services.Inquiry;
using OnlineCourseSystem.Services.Serivice;
using OnlineCourseSystem.Services.StudyMaterial;
using OnlineCourseSystem.Services.StudyMaterialCategory;
using OnlineCourseSystem.Services.Tag;
using OnlineCourseSystem.ViewModels;
using OnlineCourseSystem.ViewModels.Article;
using OnlineCourseSystem.ViewModels.ArticleCategory;
using OnlineCourseSystem.ViewModels.CounterRecord;
using OnlineCourseSystem.ViewModels.Course;
using OnlineCourseSystem.ViewModels.HomePageBanner;
using OnlineCourseSystem.ViewModels.Inquiry;
using OnlineCourseSystem.ViewModels.Service;
using OnlineCourseSystem.ViewModels.StudyMaterial;
using OnlineCourseSystem.ViewModels.StudyMaterialCategory;
using OnlineCourseSystem.ViewModels.Tag;

namespace OnlineCourseSystem.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		private IMapper _mapper;
		private readonly ILogger<HomeController> _logger;
		private ICourseService _courseService;
		private IServiceService _service;
		private IHomePageBannerService _homePageBannerService;
		private ICounterRecordService _counterRecordService;
		private IArticleService _articleService;
		private ITagService _tagService;
		private IArticleCategoryService _articleCategoryService;
		private IInquiryService _inquiryService;
		private IStudyMaterialService _studyMaterialService;
		private IStudyMaterialCategoryService _studyMaterialCategoryService;
		private IEmailService _emailService;
		private readonly IConfiguration _configuration;

		public HomeController(
			IMapper mapper,
			ILogger<HomeController> logger,
			ICourseService courseService,
			IServiceService service,
			IHomePageBannerService homePageBannerService,
			ICounterRecordService counterRecordService,
			IArticleService articleService,
			ITagService tagService,
			IArticleCategoryService articleCategoryService,
			IInquiryService inquiryService,
			IStudyMaterialService studyMaterialService,
			IStudyMaterialCategoryService studyMaterialCategoryService,
			IEmailService emailService,
			IConfiguration configuration
		)
		{
			_mapper = mapper;
			_logger = logger;
			_courseService = courseService;
			_service = service;
			_homePageBannerService = homePageBannerService;
			_counterRecordService = counterRecordService;
			_articleService = articleService;
			_tagService = tagService;
			_articleCategoryService = articleCategoryService;
			_inquiryService = inquiryService;
			_studyMaterialService = studyMaterialService;
			_studyMaterialCategoryService = studyMaterialCategoryService;
			_emailService = emailService;
			_configuration = configuration;
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
		public async Task<IActionResult> Courses()
		{
			var courses = await _courseService.GetAllPublished();
			var coursesList = _mapper.Map<List<CourseViewModel>>(courses);
			return View(coursesList);
		}

		[HttpGet]
		public async Task<IActionResult> Course(int id)
		{
			try
			{
				var course = await _courseService.GetById(id);
				if (course == null)
				{
					return View("_CourseNotFound");
				}

				var viewModel = _mapper.Map<CourseViewModel>(course);
				return View(viewModel);
			}
			catch (Exception ex)
			{
				return PartialView("_CourseLoadingError");
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
		public async Task<IActionResult> StudyMaterials()
		{
			var categories = await _studyMaterialCategoryService.GetAll();
			var categoriesList = _mapper.Map<List<StudyMaterialCategoryViewModel>>(categories);

			return View(categoriesList);
		}

		[HttpGet]
		public async Task<IActionResult> StudyMaterialsInCategory(int categoryId)
		{
			var category = await _studyMaterialCategoryService.GetById(categoryId);
			if (category == null)
			{
				return RedirectToAction("StudyMaterials");
			}

			ViewBag.CategoryName = category.Name;
			var materials = await _studyMaterialService.GetAllPublished(categoryId);
			var materialsList = _mapper.Map<List<StudyMaterialViewModel>>(materials);

			return View(materialsList);
		}


		[HttpGet]
		public async Task<IActionResult> StudyMaterial(int id)
		{
			var material = await _studyMaterialService.GetById(id);
			if (material == null)
			{
				return RedirectToAction("StudyMaterials");
			}

			var viewModel = _mapper.Map<StudyMaterialViewModel>(material);
			return View(viewModel);
		}


		[HttpGet]
		public IActionResult ContactUs()
		{
			return View();
		}

		#region Inquiry
		[HttpGet]
		public IActionResult InquirySuccess(int id)
		{
			return View();
		}

		[HttpGet]
		public IActionResult InquiryFailed()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateInquery(CreateInquiryViewModel viewModel)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return RedirectToAction("ContactUs");
				}

				var inquiry = _mapper.Map<Inquiry>(viewModel);
				inquiry.CreatedOn = DateTime.Now;

				await _inquiryService.Add(inquiry);
				var inquiryViewModel = _mapper.Map<InquiryViewModel>(inquiry);

				SendInquiryMailWhenCreated(inquiryViewModel);

				return RedirectToAction("InquirySuccess");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return RedirectToAction("AppoinmentFailed");
			}
		}


		private void SendInquiryMailWhenCreated(InquiryViewModel viewModel)
		{
			var emailBody =
				$"<table style=\"border: 1px solid; border-collapse: collapse; width:100%\">" +
					$"<tr style=\"border: 1px solid\">" +
						 $"<td style=\"border: 1px solid; padding: 7px\">Name</td>" +
						 $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.Name}</td>" +
					$"</tr>" +
					$"<tr style=\"border: 1px solid\">" +
						 $"<td style=\"border: 1px solid; padding: 7px\">Email Address</td>" +
						 $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.EmailAddress}</td>" +
					$"</tr>" +
					$"<tr style=\"border: 1px solid\">" +
						 $"<td style=\"border: 1px solid; padding: 7px\">Phone Number</td>" +
						 $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.PhoneNo}</td>" +
					$"</tr>" +
					$"<tr style=\"border: 1px solid\">" +
						 $"<td style=\"border: 1px solid; padding: 7px\">Message</td>" +
						 $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.Message}</td>" +
					$"</tr>" +
					$"<tr style=\"border: 1px solid\">" +
						 $"<td style=\"border: 1px solid; padding: 7px\">Created Date</td>" +
						 $"<td style=\"border: 1px solid; padding: 7px\">{viewModel.CreatedOnString}</td>" +
					$"</tr>" +
				$"</table>";

			var emailHTML = _emailService.GetHTMLEmailContent(
				"Inquiry Request",
				emailBody
			);

			string emailAddress = _configuration["RedirectMailAddress"];

			var isEmailSent = _emailService.SendEmail(
				new EmailDto
				{
					To = emailAddress,
					Subject = "Inquiry Request",
					Body = emailHTML
				});
		}


		#endregion

	}
}
