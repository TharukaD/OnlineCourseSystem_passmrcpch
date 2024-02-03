using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseSystem.Entities;
using OnlineCourseSystem.Services.StudyMaterialTopic;
using OnlineCourseSystem.ViewModels;
using OnlineCourseSystem.ViewModels.StudyMaterialTopic;

namespace OnlineCourseSystem.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class StudyMaterialTopicsController : Controller
    {
        private IMapper _mapper;
        private IStudyMaterialTopicService _studyMaterialTopicService;
        private readonly ILogger<StudyMaterialTopicsController> _logger;
        public StudyMaterialTopicsController(
           IMapper mapper,
           ILogger<StudyMaterialTopicsController> logger,
           IStudyMaterialTopicService studyMaterialTopicService
           )
        {
            _mapper = mapper;
            _logger = logger;
            _studyMaterialTopicService = studyMaterialTopicService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StudyMaterialTopicViewModel>? output = new();

            var materialTopics = await _studyMaterialTopicService.GetAll();
            output = _mapper.Map<List<StudyMaterialTopicViewModel>>(materialTopics);

            return View(output);
        }
        #endregion


        #region Add [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddEditStudyMaterialTopicViewModel();
            return PartialView("_AddEdit", viewModel);
        }
        #endregion

        #region Add [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Add(AddEditStudyMaterialTopicViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                string currentUser = User.Identity.Name;
                if (string.IsNullOrEmpty(currentUser))
                {
                    currentUser = "UnAuthorized";
                }

                var topic = _mapper.Map<AddEditStudyMaterialTopicViewModel, StudyMaterialTopic>(viewModel);

                await _studyMaterialTopicService.Add(topic);
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
                var materialTopic = await _studyMaterialTopicService.GetById(id);

                if (materialTopic == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material category not found."));
                }

                var viewModel = _mapper.Map<AddEditStudyMaterialTopicViewModel>(materialTopic);

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
        public async Task<IActionResult> Edit(AddEditStudyMaterialTopicViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var materialTopicInDb = await _studyMaterialTopicService.GetById(viewModel.Id.Value);
                if (materialTopicInDb == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material topic not found"));
                }

                string currentUser = User.Identity.Name;
                if (string.IsNullOrEmpty(currentUser))
                {
                    currentUser = "UnAuthorized";
                }

                var materialTopic = _mapper.Map(viewModel, materialTopicInDb);

                await _studyMaterialTopicService.Update(materialTopic);
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
            var materialTopic = await _studyMaterialTopicService.GetById(id);
            if (materialTopic == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material topic not found."));
            }
            var viewModel = _mapper.Map<StudyMaterialTopicViewModel>(materialTopic);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(StudyMaterialTopicViewModel viewModel)
        {
            try
            {
                var hasStudyMaterial = await _studyMaterialTopicService.HasAssignedToStudyMaterial(viewModel.Id);
                if (hasStudyMaterial)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Can not delete. Study material category already assigned."));
                }

                var result = await _studyMaterialTopicService.Delete(viewModel.Id);
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
    }
}
