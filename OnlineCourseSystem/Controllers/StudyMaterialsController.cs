using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseSystem.Entities;
using OnlineCourseSystem.Services.StudyMaterial;
using OnlineCourseSystem.Services.StudyMaterialCategory;
using OnlineCourseSystem.ViewModels;
using OnlineCourseSystem.ViewModels.StudyMaterial;
using OnlineCourseSystem.ViewModels.StudyMaterialCategory;
using OnlineCourseSystem.ViewModels.StudyMaterialType;
using static OnlineCourseSystem.Constants.ModelConstants;

namespace OnlineCourseSystem.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class StudyMaterialsController : Controller
    {
        private IMapper _mapper;
        private IStudyMaterialService _studyMaterialService;
        private IStudyMaterialCategoryService _studyMaterialCategoryService;
        private readonly ILogger<StudyMaterialsController> _logger;

        public StudyMaterialsController(
           IMapper mapper,
           ILogger<StudyMaterialsController> logger,
           IStudyMaterialService studyMaterialService,
           IStudyMaterialCategoryService studyMaterialCategoryService
        )
        {
            _mapper = mapper;
            _logger = logger;
            _studyMaterialService = studyMaterialService;
            _studyMaterialCategoryService = studyMaterialCategoryService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StudyMaterialViewModel>? output = new();

            var materials = await _studyMaterialService.GetAll();
            output = _mapper.Map<List<StudyMaterialViewModel>>(materials);

            return View(output);
        }
        #endregion

        #region View Uploaded File [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> ViewUploadedFile(int id)
        {
            try
            {
                var material = await _studyMaterialService.GetById(id);

                if (material == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material not found."));
                }

                var viewModel = _mapper.Map<StudyMaterialViewModel>(material);

                return PartialView("_FileViewModel", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, ex.Message));
            }
        }
        #endregion


        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var material = await _studyMaterialService.GetById(id);
            if (material == null)
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "StudyMaterial not found."));

            var viewModel = _mapper.Map<StudyMaterialViewModel>(material);

            return View(viewModel);
        }
        #endregion


        #region Add [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddEditStudyMaterialViewModel();
            var materialTypesList = _mapper.Map<List<StudyMaterialTypeViewModel>>(await _studyMaterialService.GetAllTypes());
            var categoryList = _mapper.Map<List<StudyMaterialCategoryViewModel>>(await _studyMaterialCategoryService.GetAll());
            viewModel.Initialize(materialTypesList, categoryList);

            return PartialView("_AddEdit", viewModel);
        }
        #endregion

        #region Add [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Add(AddEditStudyMaterialViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                //if (await _studyMaterialService.IsDublicate(0, viewModel.Title.Trim()) == true)
                //{
                //	return PartialView("_AjaxActionResult", new AjaxActionResult(false, "StudyMaterial title already exist"));
                //}

                string currentUser = User.Identity.Name;
                if (string.IsNullOrEmpty(currentUser))
                {
                    currentUser = "UnAuthorized";
                }

                var material = _mapper.Map<AddEditStudyMaterialViewModel, StudyMaterial>(viewModel);
                //material.CreatedBy = currentUser;
                //material.CreatedOn = DateTime.Now;

                await _studyMaterialService.Add(material);
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
                var material = await _studyMaterialService.GetById(id);

                if (material == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material not found."));
                }

                var viewModel = _mapper.Map<AddEditStudyMaterialViewModel>(material);

                var materialTypesList = _mapper.Map<List<StudyMaterialTypeViewModel>>(await _studyMaterialService.GetAllTypes());
                var categoryList = _mapper.Map<List<StudyMaterialCategoryViewModel>>(await _studyMaterialCategoryService.GetAll());
                viewModel.Initialize(materialTypesList, categoryList);

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
        public async Task<IActionResult> Edit(AddEditStudyMaterialViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                //if (await _studyMaterialService.IsDublicate(viewModel.Id.Value, viewModel.Title.Trim()) == true)
                //{
                //	return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material title already exist"));
                //}

                var materialInDb = await _studyMaterialService.GetById(viewModel.Id.Value);
                if (materialInDb == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material not found"));
                }

                string currentUser = User.Identity.Name;
                if (string.IsNullOrEmpty(currentUser))
                {
                    currentUser = "UnAuthorized";
                }

                var material = _mapper.Map(viewModel, materialInDb);
                //course.LastUpdatedOn = DateTime.Now;
                //course.LastUpdatedBy = currentUser;

                await _studyMaterialService.Update(material);
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
            var material = await _studyMaterialService.GetById(id);
            if (material == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material not found."));
            }
            var viewModel = _mapper.Map<StudyMaterialViewModel>(material);
            return PartialView("_Delete", viewModel);
        }
        #endregion

        #region Delete [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> Delete(StudyMaterialViewModel viewModel)
        {
            try
            {
                var result = await _studyMaterialService.Delete(viewModel.Id);
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


        #region Upload Study Material [ HttpGet ]
        [HttpGet]
        public IActionResult UploadStudyMaterialFile(int id, string type)
        {
            var viewModel = new AddStudyMaterialFileViewModel();
            viewModel.StudyMaterialId = id;
            viewModel.FileType = type;
            return PartialView("_UploadStudyMaterialFile", viewModel);
        }
        #endregion

        #region Upload Study Material [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> UploadStudyMaterialFile(AddStudyMaterialFileViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Validations failed."));
                }

                var material = await _studyMaterialService.GetById(viewModel.StudyMaterialId);
                if (material == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study Material not found."));
                }

                var file = viewModel.UploadedFile;

                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = DateTime.Now.Ticks.ToString() + "." + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), ReturnFolderPathbyFileType(viewModel.FileType));

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), ReturnFolderPathbyFileType(viewModel.FileType), filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                if (viewModel.FileType == FileTypes.Image)
                {
                    material.Image = filename;
                }
                else
                {
                    material.FileName = filename;
                }

                await _studyMaterialService.Update(material);
                return PartialView("_AjaxActionResult", new AjaxActionResult(true, "Successfully uploaded.", "", true));
            }
            catch (Exception ex)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Failed to upload."));
            }
        }
        #endregion

        private string ReturnFolderPathbyFileType(string filetype)
        {
            string output = "";

            if (filetype == FileTypes.Image)
            {
                output = "wwwroot\\img\\StudyMaterialImages";
            }
            else if (filetype == FileTypes.Pdf)
            {
                output = "wwwroot\\pdfs";
            }
            else if (filetype == FileTypes.Video)
            {
                output = "wwwroot\\videos";
            }

            return output;
        }


        #region Delete File [ HttpGet ]
        [HttpGet]
        public async Task<IActionResult> DeleteFileConfirmation(int id)
        {
            var material = await _studyMaterialService.GetById(id);
            if (material == null)
            {
                return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material not found."));
            }
            var viewModel = _mapper.Map<StudyMaterialViewModel>(material);
            return PartialView("_DeleteFile", viewModel);
        }
        #endregion

        #region Delete File [ HttpPost ]
        [HttpPost]
        public async Task<IActionResult> DeleteFile(StudyMaterialViewModel viewModel)
        {
            try
            {
                var material = await _studyMaterialService.GetById(viewModel.Id);
                if (material == null)
                {
                    return PartialView("_AjaxActionResult", new AjaxActionResult(false, "Study material not found."));
                }
                material.FileName = null;

                var result = await _studyMaterialService.Update(material);
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
