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

namespace VFHCatalogMVC.Web.Controllers
{
    public class PlantsController : Controller
    {

        private readonly IPlantService _plantService;
        private readonly IPlantDetailsService _plantDetailsSerrvice;
        private readonly IUserContactDataService _userContactDataService;
        private readonly ILogger<PlantsController> _logger;
        private readonly IPlantHelperService _plantHelperService;


        public PlantsController(
            IPlantService plantService, 
            ILogger<PlantsController> logger, 
            IUserContactDataService userContactDataService, 
            IPlantHelperService plantHelperService, 
            IPlantDetailsService plantDetailsSerrvice
       )
        {
            _plantService = plantService;
            _logger = logger;
            _plantDetailsSerrvice = plantDetailsSerrvice;
            _plantHelperService = plantHelperService;
            _userContactDataService = userContactDataService;
   
        }


        [HttpPost, HttpGet]
        [AllowAnonymous]
        //pageSize okresla ile rekordow bedzie wyswietlanych na stronie 
        //pageNumber okresla na ktorej stronie jestesm
        public IActionResult Index(
            int pageSize,
            int? pageNo,
            string searchString, 
            int typeId,
            int groupId,
            int? sectionId)
        {
            try
            {        
                ViewBag.TypesList = _plantHelperService.GetSelectList<PlantType,PlantTypesVm>();
                var groupsList = GetPlantGroupsList(typeId);
                ViewBag.GroupsList = groupsList.Value;
                var sectionsList = GetPlantSectionsList(groupId, typeId);
                ViewBag.SectionsList = sectionsList.Value;

                pageNo = pageNo.HasValue ? pageNo.Value : 1;
                pageSize = pageSize == 0 ? 30 : pageSize;
                searchString = searchString is null ? string.Empty : searchString;
                ViewBag.TypeId = typeId;
                ViewBag.GroupId = groupId;
                ViewBag.SectionId = sectionId;

                var model = _plantService.GetAllActivePlantsForList(pageSize, pageNo.Value, searchString, typeId, groupId, sectionId);

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
        public IActionResult IndexSeeds(
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



                var model = _plantService.GetAllPlantSeeds(id, countryId, regionId, cityId, pageSize, pageNo, isCompany, User.Identity.Name);
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
        public IActionResult IndexSeedlings(
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

                var model = _plantService.GetAllPlantSeedlings(id, countryId, regionId, cityId, pageSize, pageNo,isCompany);
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
        public IActionResult AddPlant()
        {
            ViewBag.TypesList = _plantHelperService.GetSelectList<PlantType,PlantTypesVm>();
            ViewBag.ColorsList = _plantHelperService.GetSelectList<Color,ColorsVm>();
            ViewBag.GrowingSeazons = _plantHelperService.GetSelectList<GrowingSeazon,GrowingSeazonVm>();

            return View();
        }

        //zostanie przekazny model plantu.Serwis po odpowiednim przygtowaniu danych do zapisu przekaże je do repozytorium, które zapisze je w bazie danych
        [HttpPost]
        [Authorize(Roles = "Admin,PrivateUser,Company")]
        [ValidateAntiForgeryToken] // zabezpiecza przed przesłaniem falszywego widoku podczas dodawania nowego widoku (danych)
        public IActionResult AddPlant(NewPlantVm model)
        {
            try
            {
                if (ModelState.IsValid)
                { //check DataAnnotations
                    var id = _plantService.AddPlant(model,User.Identity.Name);

                    if (id == 0)
                    {
                        ViewBag.Message = "Podana nazwa już istnieje";
                        ViewBag.TypesList = _plantHelperService.GetSelectList<PlantType, PlantTypesVm>();
                        ViewBag.ColorsList = _plantHelperService.GetSelectList<Color, ColorsVm>();
                        ViewBag.GrowingSeazons = _plantHelperService.GetSelectList<GrowingSeazon, GrowingSeazonVm>();
                        return View(model);
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.TypesList = _plantHelperService.GetSelectList<PlantType, PlantTypesVm>();
                    ViewBag.ColorsList = _plantHelperService.GetSelectList<Color, ColorsVm>();
                    ViewBag.GrowingSeazons = _plantHelperService.GetSelectList<GrowingSeazon, GrowingSeazonVm>();
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
        public IActionResult Details(int id)
        {
            var plantDetails = _plantDetailsSerrvice.GetPlantDetails(id);

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
        public IActionResult Edit(int id)
        {
            try
            {
                var plantToEdit = _plantService.GetPlantToEdit(id);

                ViewBag.ColorsList = _plantHelperService.GetSelectList<Color,ColorsVm>();

                ViewBag.GrowingSeazons = _plantHelperService.GetSelectList<GrowingSeazon,GrowingSeazonVm>();               
        
                ViewBag.GrowthTypes = _plantHelperService.GetGrowthTypes(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);
                  
                ViewBag.Destinations = _plantHelperService.GetSelectList<Destination,DestinationsVm>();

                ViewBag.FruitTypes = _plantHelperService.GetFruitPropertyList<FruitType,FruitTypeVm>(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);

                ViewBag.FruitSizes = _plantHelperService.GetFruitPropertyList<FruitSize,FruitSizeVm>(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);

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
        public IActionResult Edit(NewPlantVm plant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantService.UpdatePlant(plant);
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
        public ActionResult Delete(int id)
        {
            try
            {
                var plant = _plantService.DeletePlant(id);
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
        public IActionResult AddSeed(int id)
        {
            try
            {
                var plantSedd = _plantService.FillProperty<PlantSeedVm>(id, User.Identity.Name);
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
        public IActionResult AddSeedling(int id)
        {
            try
            {
                var plantSeedling = _plantService.FillProperty<PlantSeedlingVm>(id, User.Identity.Name);
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
        public IActionResult AddOpinion(int id)
        {
            try
            {
                var plantOpinion = _plantDetailsSerrvice.FillPropertyOpinion(id, User.Identity.Name);
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
        public IActionResult AddOpinion(PlantOpinionsVm plantOpinion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantDetailsSerrvice.AddPlantOpinion(plantOpinion);
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

        public IActionResult ActivatePlant(int id)
        {
            try
            {
                _plantService.ActivatePlant(id);
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
        public JsonResult GetPlantGroupsList(int typeId)
        {
            var groups = _plantHelperService.GetGroupsJR(typeId);
            List<SelectListItem> groupsList = new List<SelectListItem>();

            if (groups.Count > 0)
            {

                groupsList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var group in groups)
                {
                    groupsList.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
                }
            }

            return Json(groupsList);

        }

        [HttpPost]
        public JsonResult GetPlantSectionsList(int groupId, int typeId)
        {

            List<SelectListItem> sectionsList = new List<SelectListItem>();
            var sections = _plantHelperService.GetSectionsJR(groupId);


            if (sections.Count > 0)
            {
                if (typeId != 3)
                {
                    sectionsList = _plantHelperService.GetSelectListItem(sections);
                }
                else
                {
                    sectionsList.Add(new SelectListItem { Text = "Brak sekcji", Value = 0.ToString() });

                }
            }
            
            return Json(sectionsList);
        }

        [HttpPost]
        public JsonResult GetGrowthTypes( int typeId, int groupId, int? sectionId)
        {
            var list = _plantHelperService.GetGrowthTypesJR(typeId, groupId,sectionId);

            List<SelectListItem> growthTypes = new List<SelectListItem>();

            
            if (list.Count > 0)
            {
                foreach (var types in list)
                {
                    growthTypes.Add(new SelectListItem { Text = types.Name, Value = types.Id.ToString() });
                }
            }
            else
            {
                growthTypes.Add(new SelectListItem { Text = "Nie określono typów wzrostu", Value = 0.ToString() });
            }

            return Json(growthTypes);
        }

        [HttpPost]
        public JsonResult GetDestinations()
        {
            var destList = _plantHelperService.GetDestinationsJR();

            List<SelectListItem> destinations = new List<SelectListItem>();

            if (destList.Count > 0)
            {
                foreach (var dest in destList)
                {
                    destinations.Add(new SelectListItem { Text = dest.Name, Value = dest.Id.ToString() });
                }
            }
            else
            {
                destinations.Add(new SelectListItem { Text = "Nie określono przeznaczenia", Value = 0.ToString() });
            }

            return Json(destinations);
        }

        [HttpPost]
        public JsonResult GetFruitTypes(int typeId, int groupId, int? sectionId)
        {
            var fruitTypes = _plantHelperService.GetPropertiesListJR<FruitSizeVm,FruitType>(typeId, groupId, sectionId);

            List<SelectListItem> fruitTypesList = new List<SelectListItem>();

            if (fruitTypes.Count > 0)              
            {
                fruitTypesList = _plantHelperService.GetSelectListItem(fruitTypes);
            }
            else
            {
                fruitTypesList.Add(new SelectListItem { Text = "Nie określono typu owoców", Value = 0.ToString() });
            }

            return Json(fruitTypesList);
        }

        [HttpPost]
        public JsonResult GetFruitSizes(int typeId, int groupId, int? sectionId)
        {
            var fruitSiezes = _plantHelperService.GetPropertiesListJR<FruitSizeVm,FruitSize>(typeId, groupId, sectionId);

            List<SelectListItem> fruitSizesList = new List<SelectListItem>();

            if (fruitSiezes.Count > 0)
            {
                fruitSizesList = _plantHelperService.GetSelectListItem(fruitSiezes);
            }
            else
            {
                fruitSizesList.Add(new SelectListItem { Text = "Nie określono wielkości owoców", Value = 0.ToString() });
            }

            return Json(fruitSizesList);
        }
    }
}
