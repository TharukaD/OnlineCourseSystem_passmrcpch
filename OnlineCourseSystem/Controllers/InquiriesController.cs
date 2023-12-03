using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseSystem.Services.Inquiry;
using OnlineCourseSystem.ViewModels.Inquiry;

namespace OnlineCourseSystem.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class InquiriesController : Controller
    {
        private IMapper _mapper;
        private IInquiryService _inquiryService;
        private readonly ILogger<InquiriesController> _logger;

        public InquiriesController(
           IMapper mapper,
           ILogger<InquiriesController> logger,
           IInquiryService inquiryService)
        {
            _mapper = mapper;
            _logger = logger;
            _inquiryService = inquiryService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InquiryViewModel>? output = new();

            var inquiries = await _inquiryService.GetAll();
            output = _mapper.Map<List<InquiryViewModel>>(inquiries);

            return View(output);
        }
    }
}
