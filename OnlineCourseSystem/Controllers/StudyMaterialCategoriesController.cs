using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseSystem.Entities;
using OnlineCourseSystem.Services.StudyMaterialCategory;
using OnlineCourseSystem.ViewModels;
using OnlineCourseSystem.ViewModels.StudyMaterialCategory;

namespace OnlineCourseSystem.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class StudyMaterialCategoriesController : Controller
    {
        private IMapper _mapper;
        private IStudyMaterialCategoryService _studyMaterialCategoryService;
        private readonly ILogger<StudyMaterialCategoriesController> _logger;
        public StudyMaterialCategoriesController(
           IMapper mapper,
           ILogger<StudyMaterialCategoriesController> logger,
           IStudyMaterialCategoryService studyMaterialCategoryService
           )
        {
            _mapper = mapper;
            _logger = logger;
            _studyMaterialCategoryService = studyMaterialCategoryService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StudyMaterialCategoryViewModel>? output = new();

            var materialCategories = await _studyMaterialCategoryService.GetAll();
            output = _mapper.Map<List<StudyMaterialCategoryViewModel>>(materialCategories);

            return View(output);
        }
        #endregion


        #region Add [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddEditStudyMaterialCategoryViewModel();
            return PartialView("_AddEdit", viewModel);
        }
        #endregion

        #region Add [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Add(AddEditStudyMaterialCategoryViewModel viewModel)
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

                var material = _mapper.Map<AddEditStudyMaterialCategoryViewModel, StudyMaterialCategory>(viewModel);

                await _studyMaterialCategoryService.Add(material);
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
                var materialCategory = await _studyMaterialCategoryService.GetById(id);

                if (materialCategory == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material category not found."));
                }

                var viewModel = _mapper.Map<AddEditStudyMaterialCategoryViewModel>(materialCategory);

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
        public async Task<IActionResult> Edit(AddEditStudyMaterialCategoryViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var materialCategoryInDb = await _studyMaterialCategoryService.GetById(viewModel.Id.Value);
                if (materialCategoryInDb == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material category not found"));
                }

                string currentUser = User.Identity.Name;
                if (string.IsNullOrEmpty(currentUser))
                {
                    currentUser = "UnAuthorized";
                }

                var materialCategory = _mapper.Map(viewModel, materialCategoryInDb);

                await _studyMaterialCategoryService.Update(materialCategory);
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
            var materialCategory = await _studyMaterialCategoryService.GetById(id);
            if (materialCategory == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material category not found."));
            }
            var viewModel = _mapper.Map<StudyMaterialCategoryViewModel>(materialCategory);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(StudyMaterialCategoryViewModel viewModel)
        {
            try
            {
                var hasStudyMaterial = await _studyMaterialCategoryService.HasAssignedToStudyMaterial(viewModel.Id);
                if (hasStudyMaterial)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Can not delete. Study material already assigned."));
                }

                var result = await _studyMaterialCategoryService.Delete(viewModel.Id);
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
