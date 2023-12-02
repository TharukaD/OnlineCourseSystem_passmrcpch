using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseSystem.Services;
using OnlineCourseSystem.ViewModels;
using OnlineCourseSystem.ViewModels.Service;

namespace OnlineCourseSystem.Controllers
{
    public class HomeController : Controller
    {
        private IMapper _mapper;
        private readonly ILogger<HomeController> _logger;
        private IServiceService _service;

        public HomeController(
            IMapper mapper,
            ILogger<HomeController> logger,
            IServiceService service
        )
        {
            _mapper = mapper;
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomePageViewModel();

            var services = await _service.GetAll();
            viewModel.Services = _mapper.Map<List<ServiceViewModel>>(services);

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
        public IActionResult Articles()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

    }
}
