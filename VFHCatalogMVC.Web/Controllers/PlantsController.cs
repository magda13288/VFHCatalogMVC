using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.Constants;
using System;
using System.Linq;

namespace VFHCatalogMVC.Web.Controllers
{
    public class PlantsController : Controller
    {
        private readonly IPlantService _plantService;
        private readonly IPlantDetailsService _plantDetailsService;
        private readonly IUserContactDataService _userContactDataService;
        private readonly ILogger<PlantsController> _logger;
        private readonly IPlantHelperService _plantHelperService;

        public PlantsController(
            IPlantService plantService,
            ILogger<PlantsController> logger,
            IUserContactDataService userContactDataService,
            IPlantHelperService plantHelperService,
            IPlantDetailsService plantDetailsService)
        {
            _plantService = plantService;
            _logger = logger;
            _plantDetailsService = plantDetailsService;
            _plantHelperService = plantHelperService;
            _userContactDataService = userContactDataService;
        }

        [HttpGet, HttpPost]
        [AllowAnonymous]
        public IActionResult Index(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId)
        {
            try
            {
                SetPlantLists(typeId, groupId);
                pageNo ??= 1;
                pageSize = pageSize == 0 ? 30 : pageSize;
                searchString ??= string.Empty;
                SetViewBagIds(typeId, groupId, sectionId);

                var model = _plantService.GetAllActivePlantsForList(pageSize, pageNo.Value, searchString, typeId, groupId, sectionId);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet, HttpPost]
        [AllowAnonymous]
        public IActionResult IndexSeeds(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany, string sortOrder)
        {
            try
            {
                SetLocationLists(countryId, regionId);
                ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
                ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "";
                pageNo ??= 1;
                pageSize = pageSize == 0 ? 30 : pageSize;
                SetViewBagLocationIds(countryId, regionId, cityId);

                var model = _plantService.GetAllPlantSeeds(id, countryId, regionId, cityId, pageSize, pageNo, isCompany, User.Identity.Name);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet, HttpPost]
        [AllowAnonymous]
        public IActionResult IndexSeedlings(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany)
        {
            try
            {
                SetLocationLists(countryId, regionId);
                pageNo ??= 1;
                pageSize = pageSize == 0 ? 30 : pageSize;
                SetViewBagLocationIds(countryId, regionId, cityId);

                var model = _plantService.GetAllPlantSeedlings(id, countryId, regionId, cityId, pageSize, pageNo, isCompany);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        //wyświetli pusty formularz gotowy do wypełnienia
        [HttpGet]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        public IActionResult AddPlant()
        {
            SetPlantFormLists();
            return View();
        }

        //zostanie przekazny model plantu.Serwis po odpowiednim przygtowaniu danych do zapisu przekaże je do repozytorium, które zapisze je w bazie danych
        [HttpPost]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        [ValidateAntiForgeryToken] // zabezpiecza przed przesłaniem falszywego widoku podczas dodawania nowego widoku (danych)
        public IActionResult AddPlant(NewPlantVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var id = _plantService.AddPlant(model, User.Identity.Name);
                    if (id == 0)
                    {
                        ViewBag.Message = "Podana nazwa już istnieje";
                        SetPlantFormLists();
                        return View(model);
                    }
                    return RedirectToAction("Index");
                }
                SetPlantFormLists();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var plantDetails = _plantDetailsService.GetPlantDetails(id);
            if (plantDetails == null)
            {
                return RedirectToAction("Index");
            }
            return View(plantDetails);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        public IActionResult Edit(int id)
        {
            try
            {
                var plantToEdit = _plantService.GetPlantToEdit(id);
                SetEditFormLists(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);
                return View(plantToEdit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewPlantVm plant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantService.UpdatePlant(plant);
                    return RedirectToAction("Index");
                }
                return View(plant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
        //Add referesing table after delete plant
        [HttpGet]
        [Authorize(Roles = UserRoles.ADMIN)]
        public ActionResult Delete(int id)
        {
            try
            {
                _plantService.DeletePlant(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult AddSeed(int id)
        {
            try
            {
                var plantSeed = _plantService.FillProperty<PlantSeedVm>(id, User.Identity.Name);
                return PartialView("AddSeedModalPartial", plantSeed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult AddSeed(PlantSeedVm plantSeed)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantService.AddPlantSeed(plantSeed);
                    ViewBag.Message = "Zapisano";
                    ModelState.Clear();
                    return PartialView("AddSeedModalPartial");
                }
                ViewBag.Message = "Wystąpił bład podczas zapisu. Spróbuj ponownie.";
                return PartialView("AddSeedModalPartial", plantSeed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult AddSeedling(int id)
        {
            try
            {
                var plantSeedling = _plantService.FillProperty<PlantSeedlingVm>(id, User.Identity.Name);
                return PartialView("AddSeedlingModalPartial", plantSeedling);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult AddSeedling(PlantSeedlingVm plantSeedling)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantService.AddPlantSeedling(plantSeedling);
                    ViewBag.Message = "Zapisano";
                    ModelState.Clear();
                    return PartialView("AddSeedlingModalPartial");
                }
                ViewBag.Message = "Wystąpił bład podczas zapisu. Spróbuj ponownie.";
                return PartialView("AddSeedlingModalPartial", plantSeedling);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "PrivateUser,Company")]
        public IActionResult AddOpinion(int id)
        {
            try
            {
                var plantOpinion = _plantDetailsService.FillPropertyOpinion(id, User.Identity.Name);
                return PartialView("AddOpinionModalPartial", plantOpinion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        //Add refereshing page after save opinion on modal popup
        public IActionResult AddOpinion(PlantOpinionsVm plantOpinion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantDetailsService.AddPlantOpinion(plantOpinion);
                    ViewBag.Message = "Zapisano";
                    ModelState.Clear();
                    return PartialView("AddOpinionModalPartial");
                }
                ViewBag.Message = "Wystąpił bład podczas zapisu. Spróbuj ponownie.";
                return PartialView("AddOpinionModalPartial", plantOpinion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.ADMIN)]

        public IActionResult ActivatePlant(int id)
        {
            try
            {
                _plantService.ActivatePlant(id);
                return RedirectToAction("IndexNewPlants", "User", new { viewAll = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public JsonResult GetPlantGroupsList(int typeId) =>
            Json(_plantHelperService.GetGroups(typeId));

        [HttpPost]
        public JsonResult GetPlantSectionsList(int groupId, int typeId) =>
            Json(_plantHelperService.GetSections(groupId));

        [HttpPost]
        public JsonResult GetGrowthTypes(int typeId, int groupId, int? sectionId) =>
            Json(_plantHelperService.GetPlantPropertySelectListItem<GrowthType, GrowthTypeVm, GrowthTypesForListFilters, GrowthTypesForListFiltersVm>(typeId, groupId, sectionId).Skip(1));

        [HttpPost]
        public JsonResult GetDestinations() =>
            Json(_plantHelperService.GetDestinations().Skip(1));

        [HttpPost]
        public JsonResult GetFruitTypes(int typeId, int groupId, int? sectionId) =>
            Json(_plantHelperService.GetPlantPropertySelectListItem<FruitType, FruitTypeVm, FruitTypeForListFilters, FruitTypeForListFiltersVm>(typeId, groupId, sectionId));

        [HttpPost]
        public JsonResult GetFruitSizes(int typeId, int groupId, int? sectionId) =>
            Json(_plantHelperService.GetPlantPropertySelectListItem<FruitSize, FruitSizeVm, FruitSizeForListFilters, FruitSizeForListFiltersVm>(typeId, groupId, sectionId));

        #region Private Helpers

        private void SetPlantLists(int typeId, int groupId)
        {
            ViewBag.TypesList = _plantHelperService.GetSelectList<PlantType, PlantTypesVm>();
            ViewBag.GroupsList = GetPlantGroupsList(typeId).Value;
            ViewBag.SectionsList = GetPlantSectionsList(groupId, typeId).Value;
        }

        private void SetViewBagIds(int typeId, int groupId, int? sectionId)
        {
            ViewBag.TypeId = typeId;
            ViewBag.GroupId = groupId;
            ViewBag.SectionId = sectionId;
        }

        private void SetLocationLists(int countryId, int regionId)
        {
            ViewBag.CountriesList = _userContactDataService.Countries();
            ViewBag.RegionsList = _userContactDataService.Regions(countryId);
            ViewBag.CitiesList = _userContactDataService.Cities(regionId);
        }

        private void SetViewBagLocationIds(int countryId, int regionId, int cityId)
        {
            ViewBag.CountryId = countryId;
            ViewBag.RegionId = regionId;
            ViewBag.CityId = cityId;
        }

        private void SetPlantFormLists()
        {
            ViewBag.TypesList = _plantHelperService.GetSelectList<PlantType, PlantTypesVm>();
            ViewBag.ColorsList = _plantHelperService.GetSelectList<Color, ColorsVm>();
            ViewBag.GrowingSeazons = _plantHelperService.GetSelectList<GrowingSeazon, GrowingSeazonVm>().Skip(1);
        }

        private void SetEditFormLists(int typeId, int groupId, int? sectionId)
        {
            ViewBag.ColorsList = _plantHelperService.GetSelectList<Color, ColorsVm>();
            ViewBag.GrowingSeazons = _plantHelperService.GetSelectList<GrowingSeazon, GrowingSeazonVm>().Skip(1);
            ViewBag.GrowthTypes = _plantHelperService.GetPlantPropertySelectListItem<GrowthType, GrowthTypeVm, GrowthTypesForListFilters, GrowthTypesForListFiltersVm>(typeId, groupId, sectionId).Skip(1);
            ViewBag.Destinations = _plantHelperService.GetSelectList<Destination, DestinationsVm>().Skip(1);
            ViewBag.FruitTypes = _plantHelperService.GetPlantPropertySelectListItem<FruitType, FruitTypeVm, FruitTypeForListFilters, FruitTypeForListFiltersVm>(typeId, groupId, sectionId);
            ViewBag.FruitSizes = _plantHelperService.GetPlantPropertySelectListItem<FruitSize, FruitSizeVm, FruitSizeForListFilters, FruitSizeForListFiltersVm>(typeId, groupId, sectionId);
        }

        #endregion
    }
}
