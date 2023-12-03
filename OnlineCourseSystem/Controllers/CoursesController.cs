using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseSystem.Entities;
using OnlineCourseSystem.Services.Course;
using OnlineCourseSystem.ViewModels;
using OnlineCourseSystem.ViewModels.Course;

namespace OnlineCourseSystem.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class CoursesController : Controller
    {
        private IMapper _mapper;
        private ICourseService _courseService;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(
           IMapper mapper,
           ILogger<CoursesController> logger,
           ICourseService courseService
           )
        {
            _mapper = mapper;
            _logger = logger;
            _courseService = courseService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CourseViewModel>? output = new();

            var courses = await _courseService.GetAll();
            output = _mapper.Map<List<CourseViewModel>>(courses);

            return View(output);
        }
        #endregion


        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetById(id);
            if (course == null)
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Course not found."));

            var viewModel = _mapper.Map<CourseViewModel>(course);

            return View(viewModel);
        }
        #endregion


        #region Add [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddEditCourseViewModel();
            return PartialView("_AddEdit", viewModel);
        }
        #endregion

        #region Add [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Add(AddEditCourseViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _courseService.IsDublicate(0, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Course name already exist"));
                }

                string currentUser = User.Identity.Name;
                if (string.IsNullOrEmpty(currentUser))
                {
                    currentUser = "UnAuthorized";
                }

                var course = _mapper.Map<AddEditCourseViewModel, Course>(viewModel);
                course.CreatedBy = currentUser;
                course.CreatedOn = DateTime.Now;

                await _courseService.Add(course);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully added.", "", true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to add."));
            }
        }
        #endregion


        #region Edit [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var course = await _courseService.GetById(id);

                if (course == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Course not found."));
                }

                var viewModel = _mapper.Map<AddEditCourseViewModel>(course);
                return PartialView("_AddEdit", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, ex.Message));
            }
        }
        #endregion

        #region Edit [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Edit(AddEditCourseViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                if (await _courseService.IsDublicate(viewModel.Id.Value, viewModel.Name.Trim()) == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Course name already exist"));
                }

                var courseInDb = await _courseService.GetById(viewModel.Id.Value);
                if (courseInDb == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Course not found"));
                }

                string currentUser = User.Identity.Name;
                if (string.IsNullOrEmpty(currentUser))
                {
                    currentUser = "UnAuthorized";
                }

                var course = _mapper.Map(viewModel, courseInDb);
                course.LastUpdatedOn = DateTime.Now;
                course.LastUpdatedBy = currentUser;

                await _courseService.Update(course);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully saved.", "", true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to save."));
            }
        }
        #endregion


        #region Delete [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetById(id);
            if (course == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Course not found."));
            }
            var viewModel = _mapper.Map<CourseViewModel>(course);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(CourseViewModel viewModel)
        {
            try
            {
                var result = await _courseService.Delete(viewModel.Id);
                if (result == true)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully deleted.", "", true));
                }
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to delete."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to delete."));
            }
        }
        #endregion


        #region Upload Course Image [ HttpGet ]
        [HttpGet]
        public IActionResult UploadCourseImage(int id)
        {
            var viewModel = new AddCourseImageViewModel();
            viewModel.CourseId = id;
            return PartialView("_UploadCourseImage", viewModel);
        }
        #endregion

        #region Upload Course Image [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> UploadCourseImage(AddCourseImageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var course = await _courseService.GetById(viewModel.CourseId);
                if (course == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Course not found."));
                }

                var file = viewModel.UploadedFile;

                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = DateTime.Now.Ticks.ToString() + "." + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\CourseImages");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\CourseImages", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                course.Image = filename;
                await _courseService.Update(course);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully uploaded.", "", true));
            }
            catch (Exception ex)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to upload."));
            }
        }
        #endregion

    }
}
