using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using VFHCatalogMVC.Application;
using System.IO;
using VFHCatalogMVC.Application.ViewModels.Plant;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.Extensions.Logging;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.Constants;
using System.Web.WebPages;
using VFHCatalogMVC.Application.Services.UserServices;
using System.Threading.Tasks;

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
            IPlantDetailsService plantDetailsService
       )
        {
            _plantService = plantService;
            _logger = logger;
            _plantDetailsService = plantDetailsService;
            _plantHelperService = plantHelperService;
            _userContactDataService = userContactDataService;
   
        }


        [HttpPost, HttpGet]
        [AllowAnonymous]
        //pageSize okresla ile rekordow bedzie wyswietlanych na stronie 
        //pageNumber okresla na ktorej stronie jestesm
        public async Task<IActionResult> Index(
            int pageSize,
            int? pageNo,
            string searchString, 
            int typeId,
            int groupId,
            int? sectionId)
        {
            try
            {        
                ViewBag.TypesList = await _plantHelperService.GetSelectListAsync<PlantType,PlantTypesVm>();
                var groupsList = await GetPlantGroupsList(typeId);
                ViewBag.GroupsList = groupsList.Value;
                var sectionsList = await GetPlantSectionsList(groupId, typeId);
                ViewBag.SectionsList = sectionsList.Value;

                pageNo = pageNo.HasValue ? pageNo.Value : 1;
                pageSize = pageSize == 0 ? 30 : pageSize;
                searchString = searchString is null ? string.Empty : searchString;
                ViewBag.TypeId = typeId;
                ViewBag.GroupId = groupId;
                ViewBag.SectionId = sectionId;

                var model = await _plantService.GetAllActivePlantsForListAsync(pageSize, pageNo.Value, searchString, typeId, groupId, sectionId);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet,HttpPost]
        //[Authorize(Roles = "PrivateUser,Company")]
        [AllowAnonymous]
        public async Task<IActionResult> IndexSeeds(
            int id,
            int countryId,
            int regionId,
            int cityId,
            int pageSize,
            int? pageNo,
            bool isCompany,
            string sortOrder)
        {
            try
            {
                ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
                ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "";
                ViewBag.CountriesList = _userContactDataService.Countries();
                ViewBag.RegionsList = _userContactDataService.Regions(countryId);
                ViewBag.CitiesList = _userContactDataService.Cities(regionId);

                pageNo = pageNo.HasValue ? pageNo.Value : 1;
                pageSize = pageSize == 0 ? 30 : pageSize;

                ViewBag.CountryId = countryId;
                ViewBag.RegionId = regionId;
                ViewBag.CityId = cityId;



                var model = await _plantService.GetAllPlantSeedsAsync(id, countryId, regionId, cityId, pageSize, pageNo, isCompany, User.Identity.Name);
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }                                                                      
        }

        [HttpGet, HttpPost]
        //[Authorize(Roles = "PrivateUser,Company")]
        [AllowAnonymous]
        public async Task<IActionResult> IndexSeedlings(
            int id, 
            int countryId,
            int regionId,
            int cityId, 
            int pageSize,
            int? pageNo,
            bool isCompany)
        {
            try
            {
                ViewBag.CountriesList = _userContactDataService.Countries();
                ViewBag.RegionsList = _userContactDataService.Regions(countryId);
                ViewBag.CitiesList = _userContactDataService.Cities(regionId);

                pageNo = !pageNo.HasValue ? 1 : pageNo;
                pageSize = pageSize == 0 ? 30 : pageSize;

                ViewBag.CountryId = countryId;
                ViewBag.RegionId = regionId;
                ViewBag.CityId = cityId;

                var model = await _plantService.GetAllPlantSeedlingsAsync(id, countryId, regionId, cityId, pageSize, pageNo,isCompany);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }

        }

        //wyświetli pusty formularz gotowy do wypełnienia
        [HttpGet]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        public async Task<IActionResult> AddPlant()
        {
            ViewBag.TypesList = await _plantHelperService.GetSelectListAsync<PlantType, PlantTypesVm>();
            ViewBag.ColorsList = await _plantHelperService.GetSelectListAsync<Color, ColorsVm>();
            ViewBag.GrowingSeazons = await _plantHelperService.GetSelectListAsync<GrowingSeazon, GrowingSeazonVm>();

            return View();
        }

        //zostanie przekazny model plantu.Serwis po odpowiednim przygtowaniu danych do zapisu przekaże je do repozytorium, które zapisze je w bazie danych
        [HttpPost]
        [Authorize(Roles = "Admin,PrivateUser,Company")]
        [ValidateAntiForgeryToken] // zabezpiecza przed przesłaniem falszywego widoku podczas dodawania nowego widoku (danych)
        public async Task<IActionResult> AddPlant(NewPlantVm model)
        {
            try
            {
                if (ModelState.IsValid)
                { //check DataAnnotations
                    var id = await _plantService.AddPlantAsync(model,User.Identity.Name);

                    if (id == 0)
                    {
                        ViewBag.Message = "Podana nazwa już istnieje";
                        ViewBag.TypesList = await _plantHelperService.GetSelectListAsync<PlantType, PlantTypesVm>();
                        ViewBag.ColorsList = await _plantHelperService.GetSelectListAsync<Color, ColorsVm>();
                        ViewBag.GrowingSeazons = await _plantHelperService.GetSelectListAsync<GrowingSeazon, GrowingSeazonVm>();
                        return View(model);
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.TypesList = await _plantHelperService.GetSelectListAsync<PlantType, PlantTypesVm>();
                    ViewBag.ColorsList = await _plantHelperService.GetSelectListAsync<Color, ColorsVm>();
                    ViewBag.GrowingSeazons = await _plantHelperService.GetSelectListAsync<GrowingSeazon, GrowingSeazonVm>();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var plantDetails = await _plantDetailsService.GetPlantDetailsAsync(id);

            if (plantDetails == null)
            {
                return RedirectToAction("Index");
            }
            else
            { 
            return View(plantDetails);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var plantToEdit = await _plantService.GetPlantToEditAsync(id);

                ViewBag.ColorsList = await _plantHelperService.GetSelectListAsync<Color, ColorsVm>();

                ViewBag.GrowingSeazons = await _plantHelperService.GetSelectListAsync<GrowingSeazon, GrowingSeazonVm>();               
        
                ViewBag.GrowthTypes = await _plantHelperService.GetPlantPropertySelectListItemAsync<GrowthType, GrowthTypeVm, GrowthTypesForListFilters, GrowthTypesForListFiltersVm>(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);
                  
                ViewBag.Destinations = await _plantHelperService.GetSelectListAsync<Destination, DestinationsVm>();

                ViewBag.FruitTypes = await _plantHelperService.GetPlantPropertySelectListItemAsync<FruitType, FruitTypeVm, FruitTypeForListFilters, FruitTypeForListFiltersVm>(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);

                ViewBag.FruitSizes = await _plantHelperService.GetPlantPropertySelectListItemAsync<FruitSize, FruitSizeVm, FruitSizeForListFilters, FruitSizeForListFiltersVm>(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);

                return View(plantToEdit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewPlantVm plant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _plantService.UpdatePlantAsync(plant);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(plant);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        //Add referesing table after delete plant
        [HttpGet]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var plant = await _plantService.DeletePlantAsync(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            { 
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public async Task<IActionResult> AddSeed(int id)
        {
            try
            {
                var plantSedd = await _plantService.FillPropertyAsync<PlantSeedVm>(id, User.Identity.Name);
                return PartialView("AddSeedModalPartial", plantSedd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }


        [HttpPost]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
    
        public async Task<IActionResult> AddSeed(PlantSeedVm plantSeed)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _plantService.AddPlantSeedAsync(plantSeed);
                    ViewBag.Message = "Zapisano";
                    ModelState.Clear();
                    return PartialView("AddSeedModalPartial");
                }
                else
                {
                    ViewBag.Message = "Wystąpił bład podczas zapisu. Spróbuj ponownie.";
                    return PartialView("AddSeedModalPartial", plantSeed);
                }

                //return PartialView("AddSeedModalPartial", plantSeed);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public async Task<IActionResult> AddSeedling(int id)
        {
            try
            {
                var plantSeedling = await _plantService.FillPropertyAsync<PlantSeedlingVm>(id, User.Identity.Name);
                return PartialView("AddSeedlingModalPartial", plantSeedling);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public async Task<IActionResult> AddSeedling(PlantSeedlingVm plantSeedling)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   await _plantService.AddPlantSeedlingAsync(plantSeedling);
                    ViewBag.Message = "Zapisano";
                    ModelState.Clear();
                    return PartialView("AddSeedlingModalPartial");
                    // return RedirectToAction("Index","Plants");

                }
                else
                {
                    ViewBag.Message = "Wystąpił bład podczas zapisu. Spróbuj ponownie.";
                    return PartialView("AddSeedlingModalPartial", plantSeedling);
                }
      
               

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }

        }

        [HttpGet]
        [Authorize(Roles = "PrivateUser,Company")]
        public async Task<IActionResult> AddOpinion(int id)
        {
            try
            {
                var plantOpinion = await _plantDetailsService.FillPropertyOpinionAsync(id, User.Identity.Name);
                return PartialView("AddOpinionModalPartial", plantOpinion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        //Add refereshing page after save opinion on modal popup
        public async Task<IActionResult> AddOpinion(PlantOpinionsVm plantOpinion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   await _plantDetailsService.AddPlantOpinionAsync(plantOpinion);
                    ViewBag.Message = "Zapisano";
                    ModelState.Clear();
                    return PartialView("AddOpinionModalPartial");
                    // return RedirectToAction("Details");

                }
                else
                {
                    ViewBag.Message = "Wystąpił bład podczas zapisu. Spróbuj ponownie.";
                    return PartialView("AddOpinionModalPartial", plantOpinion);
                }
                   
             
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }

        }

        [HttpGet]
        [Authorize(Roles = UserRoles.ADMIN)]

        public async Task<IActionResult> ActivatePlant(int id)
        {
            try
            {
                await _plantService.ActivatePlantAsync(id);
                bool viewAll = true;
                return RedirectToAction("IndexNewPlants", "User", viewAll);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetPlantGroupsList(int typeId)
        {
            var groupsList = await _plantHelperService.GetGroupsAsync(typeId);        

            return Json(groupsList);

        }

        [HttpPost]
        public async Task<JsonResult> GetPlantSectionsList(int groupId, int typeId)
        {

            var sectionsList = await _plantHelperService.GetSectionsAsync(groupId) ;
            
            return Json(sectionsList);
        }

        [HttpPost]
        public async Task<JsonResult> GetGrowthTypes( int typeId, int groupId, int? sectionId)
        {

            var growthTypes = await _plantHelperService.GetPlantPropertySelectListItemAsync<GrowthType,GrowthTypeVm,GrowthTypesForListFilters,GrowthTypesForListFiltersVm>(typeId,groupId,sectionId);        

            return Json(growthTypes);
        }

        [HttpPost]
        public async Task<JsonResult> GetDestinations()
        {
            var destList = await _plantHelperService.GetDestinationsAsync();

            return Json(destList);
        }

        [HttpPost]
        public async Task<JsonResult> GetFruitTypes(int typeId, int groupId, int? sectionId)
        {
            var fruitTypesList = await _plantHelperService.GetPlantPropertySelectListItemAsync<FruitType,FruitTypeVm,FruitTypeForListFilters,FruitTypeForListFiltersVm>(typeId,groupId,sectionId);

            return Json(fruitTypesList);
        }

        [HttpPost]
        public async Task<JsonResult> GetFruitSizes(int typeId, int groupId, int? sectionId)
        {
         
            var fruitSizesList = await _plantHelperService.GetPlantPropertySelectListItemAsync<FruitSize,FruitSizeVm,FruitSizeForListFilters,FruitSizeForListFiltersVm>(typeId,groupId,sectionId);

            return Json(fruitSizesList);
        }
    }
}
