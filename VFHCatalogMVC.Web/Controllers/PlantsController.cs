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
using System.ComponentModel;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Diagnostics;
using VFHCatalogMVC.Web.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;

namespace VFHCatalogMVC.Web.Controllers
{
    public class PlantsController : Controller
    {

        private readonly IPlantService _plantService;
        private readonly IPlantDetailsService _plantDetailsSerrvice;
        private readonly IUserContactDataService _userContactDataService;
        private readonly ILogger<PlantsController> _logger;
        private readonly IPlantHelperService _plantHelperService;

        public PlantsController(IPlantService plantService, ILogger<PlantsController> logger, IUserContactDataService userContactDataService, IPlantHelperService plantHelperService, IPlantDetailsService plantDetailsSerrvice)
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
        public IActionResult Index(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId)
        {
            try
            {        
                var types = _plantHelperService.GetPlantTypes();
                ViewBag.TypesList = _plantHelperService.FillPropertyList(types, null, null);
                var groupsList = GetPlantGroupsList(typeId);
                ViewBag.GroupsList = groupsList.Value;
                var sectionsList = GetPlantSectionsList(groupId, typeId);
                ViewBag.SectionsList = sectionsList.Value;

                if (!pageNo.HasValue)
                {
                    pageNo = 1;
                }
                if (searchString is null)
                {
                    searchString = string.Empty;
                }
                if (typeId != 0)
                    ViewBag.TypeId = typeId;
                if (groupId != 0)
                    ViewBag.GroupId = groupId;
                if (sectionId != 0)
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
        public IActionResult IndexSeeds(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany, string sortOrder)
        {
            try
            {
                ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
                ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "";
                ViewBag.CountriesList = _userContactDataService.Countries();
                ViewBag.RegionsList = _userContactDataService.Regions(countryId);
                ViewBag.CitiesList = _userContactDataService.Cities(regionId);

                if (!pageNo.HasValue)
                {
                    pageNo = 1;
                }
                if (pageSize == 0)
                {
                    pageSize = 30;
                }
                if (countryId != 0)
                {
                    ViewBag.CountryId = countryId;
                }
                if (regionId != 0)
                {
                    ViewBag.RegionId = regionId;
                }
                if (cityId != 0)
                {
                    ViewBag.CityId = cityId;
                }

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
        public IActionResult IndexSeedlings(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo,bool isCompany)
        {
            try
            {
                ViewBag.CountriesList = _userContactDataService.Countries();
                ViewBag.RegionsList = _userContactDataService.Regions(countryId);
                ViewBag.CitiesList = _userContactDataService.Cities(regionId);

                if (!pageNo.HasValue)
                {
                    pageNo = 1;
                }
                if (pageSize == 0)
                {
                    pageSize = 30;
                }
                if (countryId != 0)
                {
                    ViewBag.CountryId = countryId;
                }
                if (regionId != 0)
                {
                    ViewBag.RegionId = regionId;
                }
                if (cityId != 0)
                {
                    ViewBag.CityId = cityId;
                }

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
        [Authorize(Roles = "Admin, PrivateUser, Company")]
        public IActionResult AddPlant()
        {
            ViewBag.TypesList = _plantHelperService.PlantTypes();
            ViewBag.ColorsList = _plantHelperService.PlantColors();
            ViewBag.GrowingSeazons = _plantHelperService.PlantGrowingSeaznos();

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
                        ViewBag.TypesList = _plantHelperService.PlantTypes();
                        ViewBag.ColorsList = _plantHelperService.PlantColors();
                        ViewBag.GrowingSeazons = _plantHelperService.PlantGrowingSeaznos();
                        return View(model);
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.TypesList = _plantHelperService.PlantTypes();
                    ViewBag.ColorsList = _plantHelperService.PlantColors();
                    ViewBag.GrowingSeazons = _plantHelperService.PlantGrowingSeaznos();
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
        [Authorize(Roles = "Admin,PrivateUser,Company")]
        public IActionResult Edit(int id)
        {
            try
            {
                var plantToEdit = _plantService.GetPlantToEdit(id);
                ViewBag.ColorsList = _plantHelperService.PlantColors();

                ViewBag.GrowingSeazons = _plantHelperService.PlantGrowingSeaznos();

                var growthTypes = GetGrowthTypes(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);
                ViewBag.GrowthTypes = growthTypes.Value;
      
                var destinations = GetDestinations();
                ViewBag.Destinations = destinations.Value;

                var fruitTypes = GetFruitTypes(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);
                ViewBag.FruitTypes = fruitTypes.Value;
                var fruitSize = GetFruitSizes(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);
                ViewBag.FruitSizes = fruitSize.Value;

                return View(plantToEdit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PrivateUser,Company")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "PrivateUser,Company")]
        public IActionResult AddSeed(int id)
        {
            try
            {
                var plantSedd = _plantService.FillProperties(id, User.Identity.Name);
                return PartialView("AddSeedModalPartial", plantSedd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }


        [HttpPost]
        [Authorize(Roles = "PrivateUser,Company")]
    
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
        [Authorize(Roles = "PrivateUser,Company")]
        public IActionResult AddSeedling(int id)
        {
            try
            {
                var plantSeedling = _plantService.FillPropertiesSeedling(id, User.Identity.Name);
                return PartialView("AddSeedlingModalPartial", plantSeedling);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "PrivateUser,Company")]
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
        [Authorize(Roles = "PrivateUser,Company")]
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
        [Authorize(Roles = "Admin")]

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
            var groups = _plantHelperService.GetPlantGroups(typeId);
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
            var sections = _plantHelperService.GetPlantSections(groupId);

            if (sections.Count > 0)
            {
                if (typeId != 3)
                {
                    sectionsList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                    foreach (var section in sections)
                    {
                        sectionsList.Add(new SelectListItem { Text = section.Name, Value = section.Id.ToString() });
                    }
                }         
            }
            //else
            //{
            //    sectionsList.Add(new SelectListItem { Text = "Brak sekcji", Value = 0.ToString() });

            //}
            return Json(sectionsList);
        }

        [HttpPost]
        public JsonResult GetGrowthTypes( int typeId, int groupId, int? sectionId)
        {
            var list = _plantHelperService.GetGrowthTypes(typeId, groupId,sectionId);

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
            var destList = _plantHelperService.GetDestinations();

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
            var fruitTypes = _plantHelperService.GetFruitType(typeId, groupId, sectionId);

            List<SelectListItem> fruitTypesList = new List<SelectListItem>();

            if (fruitTypes.Count > 0)              
            {
                fruitTypesList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var types in fruitTypes)
                {
                    fruitTypesList.Add(new SelectListItem { Text = types.Name, Value = types.Id.ToString() });
                }
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
            var fruitSiezes = _plantHelperService.GetFruitSize(typeId, groupId, sectionId);

            List<SelectListItem> fruitSizesList = new List<SelectListItem>();

            if (fruitSiezes.Count > 0)
            {
                fruitSizesList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });
                foreach (var sizes in fruitSiezes)
                {
                    fruitSizesList.Add(new SelectListItem { Text = sizes.Name, Value = sizes.Id.ToString() });
                }
            }
            else
            {
                fruitSizesList.Add(new SelectListItem { Text = "Nie określono wielkości owoców", Value = 0.ToString() });
            }

            return Json(fruitSizesList);
        }
    }
}
