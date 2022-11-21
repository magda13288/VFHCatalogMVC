using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using VFHCatalogMVC.Application;
using System.IO;
using VFHCatalogMVC.Application.Interfaces;
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

namespace VFHCatalogMVC.Web.Controllers
{
    public class PlantsController : Controller
    {

        private readonly IPlantService _plantService;
        private readonly IAddressService _addressService;
        private readonly ILogger<PlantsController> _logger;

        public PlantsController(IPlantService plantService, ILogger<PlantsController> logger, IAddressService addressService)
        {
            _plantService = plantService;
            _logger = logger;
            _addressService = addressService;
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var model = _plantService.GetAllActivePlantsForList(4, 1, "", null, null, null); //2 elementy na stronie, pierwsza strona, zadnego wyszukiwania
        //    var types = _plantService.GetPlantTypes();
        //    ViewBag.TypesList = FillPropertyList(types,null,null);
        //    ViewBag.GroupsList = string.Empty;
        //    ViewBag.SectionsList = string.Empty;

        //    return View(model);
        //}

        [HttpPost, HttpGet]
        [AllowAnonymous]
        //pageSize okresla ile rekordow bedzie wyswietlanych na stronie 
        //pageNumber okresla na ktorej stronie jestesm
        public IActionResult Index(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId)
        {
            try
            {
                var types = _plantService.GetPlantTypes();
                ViewBag.TypesList = _plantService.FillPropertyList(types, null, null);
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

                var model = _plantService.GetAllActivePlantsForList(pageSize, pageNo, searchString, typeId, groupId, sectionId);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet,HttpPost]
        public IActionResult IndexSeeds(int id, int countryId, int voivodeshipId, int cityId, int pageSize, int? pageNo)
        {
            var countries = _addressService.GetCountries();
            ViewBag.CountriesList = _addressService.FillCountryList(countries);
            var voivodeships = _addressService.GetVoivodeships(countryId);
            ViewBag.VoivodeshipsList = _addressService.FillVoivodeshipList(voivodeships);
            var cities = _addressService.GetCities(voivodeshipId);
            ViewBag.CitiesList = _addressService.FillCityList(cities);

            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (pageSize == 0)
            {
                pageSize = 10;
            }

            if (countryId != 0)
            {
                ViewBag.CountryId = countryId;              
            }
            if (voivodeshipId != 0)
            {
                ViewBag.VoivodeshipId = voivodeshipId;
            }
            if (cityId != 0)
            {
                ViewBag.CityId = cityId;
            }

            var model = _plantService.GetAllPlantSeeds(id, countryId, voivodeshipId, cityId, pageSize, pageNo);
                                                                 
            return View(model);
        }

        [HttpGet, HttpPost]

        public IActionResult IndexSeedlings(int id, int? countryId, int? voivodeshipId, int? cityId)
        {
            return View();
        }
        //wyświetli pusty formularz gotowy do wypełnienia
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddPlant()
        {
            var types = _plantService.GetPlantTypes();
            ViewBag.TypesList = _plantService.FillPropertyList(types, null, null);
            var colors = _plantService.GetColors();
            ViewBag.ColorsList = _plantService.FillPropertyList(null, colors, null);
            var growingSeaznos = _plantService.GetGrowingSeazons();
            ViewBag.GrowingSeazons = _plantService.FillPropertyList(null, null, growingSeaznos);

            return View();
        }

        //zostanie przekazny model plantu.Serwis po odpowiednim przygtowaniu danych do zapisu przekaże je do repozytorium, które zapisze je w bazie danych
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken] // zabezpiecza przed przesłaniem falszywego widoku dodawania nowego widoku (danych)
        public IActionResult AddPlant(NewPlantVm model)
        {

            if (ModelState.IsValid)
            { //check DataAnnotations
                var id = _plantService.AddPlant(model);
                return RedirectToAction("Index");
            }

            var types = _plantService.GetPlantTypes();
            ViewBag.TypesList = _plantService.FillPropertyList(types, null, null);
            var colors = _plantService.GetColors();
            ViewBag.ColorsList = _plantService.FillPropertyList(null, colors, null);
            var growingSeaznos = _plantService.GetGrowingSeazons();
            ViewBag.GrowingSeazons = _plantService.FillPropertyList(null, null, growingSeaznos);
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var plantDetails = _plantService.GetPlantDetails(id);

            if (plantDetails == null)
            {
                return RedirectToAction("Index");
            }

            return View(plantDetails);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var plantToEdit = _plantService.GetPlantToEdit(id);
            var colors = _plantService.GetColors();
            ViewBag.ColorsList = _plantService.FillPropertyList(null, colors, null);

            var growingSeaznos = _plantService.GetGrowingSeazons();
            ViewBag.GrowingSeazons = _plantService.FillPropertyList(null, null, growingSeaznos);

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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewPlantVm plant)
        {
            if (ModelState.IsValid)
            {
                _plantService.UpdatePlant(plant);
                return RedirectToAction("Index");
            }
            return View(plant);
        }
        //Add referesing table after delete plant
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var plant = _plantService.DeletePlant(id);
            return RedirectToAction("Index", plant);
        }

        [HttpGet]
        [Authorize(Roles = "PrivateUser,Customer")]
        public IActionResult AddSeed(int id)
        {
            var plantSedd = _plantService.FillProperties(id,User.Identity.Name);
            return PartialView("AddSeedModalPartial",plantSedd);
        }


        [HttpPost]
        [Authorize(Roles = "PrivateUser,Customer")]
        public IActionResult AddSeed(PlantSeedVm plantSeed)
        {
            if (ModelState.IsValid)
            {
                _plantService.AddPlantSeed(plantSeed);
                return RedirectToAction("Index");
            }
            return PartialView("AddSeedModalPartial", plantSeed);
        }

        [HttpGet]
        [Authorize(Roles = "PrivateUser,Customer")]
        public IActionResult AddSeedling(int id)
        {
            var plantSeedling = _plantService.FillPropertiesSeedling(id,User.Identity.Name);
            return PartialView("AddSeedlingModalPartial",plantSeedling);
        }

        [HttpPost]
        [Authorize(Roles = "PrivateUser,Customer")]
        public IActionResult AddSeedling(PlantSeedlingVm plantSeedling)
        {
            if (ModelState.IsValid)
            {
                _plantService.AddPlantSeedling(plantSeedling);
                return RedirectToAction("Index");

            }
            return PartialView("AddSeedlingModalPartial", plantSeedling);

        }

        [HttpGet]
        [Authorize(Roles = "PrivateUser,Customer")]
        public IActionResult AddOpinion(int id)
        {
            var plantOpinion = _plantService.FillPropertyOpinion(id, User.Identity.Name);
            return PartialView("AddOpinionModalPartial", plantOpinion);
        }
        [HttpPost]
        [Authorize(Roles = "PrivateUser,Customer")]
        //Add refereshing page after save opinion on modal popup
        public IActionResult AddOpinion(PlantOpinionsVm plantOpinion)
        {
            if (ModelState.IsValid)
            {
                _plantService.AddPlantOpinion(plantOpinion);
                return RedirectToAction("Details");

            }
            return PartialView("AddOpinionModalPartial", plantOpinion);

        }

        [HttpPost]
        public JsonResult GetVoivodesipsList(int countryId)
        {
            var voivodeships = _addressService.GetVoivodeships(countryId);
            List<SelectListItem> voivodeshipsList = new List<SelectListItem>();

            if (voivodeships.Count > 0)
            {

                voivodeshipsList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var group in voivodeships)
                {
                    voivodeshipsList.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
                }
            }

            return Json(voivodeshipsList);

        }

        [HttpPost]
        public JsonResult GetCitiesList(int voivodeshipId)
        {
            var cities = _addressService.GetVoivodeships(voivodeshipId);
            List<SelectListItem> citiesList = new List<SelectListItem>();

            if (cities.Count > 0)
            {

                citiesList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var group in cities)
                {
                    citiesList.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
                }
            }

            return Json(citiesList);

        }

        [HttpPost]
        public JsonResult GetPlantGroupsList(int typeId)
        {
            var groups = _plantService.GetPlantGroups(typeId);
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
            var sections = _plantService.GetPlantSections(groupId);

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
            var list = _plantService.GetGrowthTypes(typeId, groupId,sectionId);

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
            var destList = _plantService.GetDestinations();

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
            var fruitTypes = _plantService.GetFruitType(typeId, groupId, sectionId);

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
            var fruitSiezes = _plantService.GetFruitSize(typeId, groupId, sectionId);

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
        [HttpPost]
        public JsonResult GetVoivodeships(int id)
        {
            var voivodeships = _addressService.GetVoivodeships(id);

            List<SelectListItem> voivodeshipsList = new List<SelectListItem>();

            if (voivodeships.Count > 0)
            {

                voivodeshipsList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var group in voivodeships)
                {
                    voivodeshipsList.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
                }
            }

            return Json(voivodeshipsList);
        }

        [HttpPost]
        public JsonResult GetCities(int id)
        {
            var cities = _addressService.GetCities(id);

            List<SelectListItem> citiesList = new List<SelectListItem>();

            if (cities.Count > 0)
            {

                citiesList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var group in cities)
                {
                    citiesList.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
                }
            }

            return Json(citiesList);
        }
    }
}
