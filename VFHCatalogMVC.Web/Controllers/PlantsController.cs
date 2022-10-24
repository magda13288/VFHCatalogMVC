﻿using Microsoft.AspNetCore.Mvc;
using VFHCatalogMVC.Application;
using System.IO;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Plant;
using System.Linq;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VFHCatalogMVC.Web.Controllers
{
    public class PlantsController : Controller
    {

        private readonly IPlantService _plantService;

        public PlantsController(IPlantService plantService)
        {
            _plantService = plantService;
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

        //pageSize okresla ile rekordow bedzie wyswietlanych na stronie 
        //pageNumber okresla na ktorej stronie jestesmy
        public IActionResult Index(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId)
        {

            var types = _plantService.GetPlantTypes();
            ViewBag.TypesList = FillPropertyList(types, null, null);
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
        //wyświetli pusty formularz gotowy do wypełnienia
        [HttpGet]
        public IActionResult AddPlant()
        {
            var types = _plantService.GetPlantTypes();
            ViewBag.TypesList = FillPropertyList(types,null,null);
            var colors = _plantService.GetColors();
            ViewBag.ColorsList = FillPropertyList(null, colors, null);
            var growingSeaznos = _plantService.GetGrowingSeazons();
            ViewBag.GrowingSeazons = FillPropertyList(null, null, growingSeaznos);

            return View();
        }      

        //zostanie przekazny model plantu.Serwis po odpowiednim przygtowaniu danych do zapisu przekaże je do repozytorium, które zapisze je w bazie danych
        [HttpPost]

        [ValidateAntiForgeryToken] // zabezpiecza przed przesłaniem falszywego widoku dodawania nowego widoku (danych)
        public IActionResult AddPlant(NewPlantVm model)
        {
            
            if (ModelState.IsValid)
            { //check DataAnnotations
                var id = _plantService.AddPlant(model);
                return RedirectToAction("Index");
            }

            var types = _plantService.GetPlantTypes();
            ViewBag.TypesList = FillPropertyList(types, null, null);
            var colors = _plantService.GetColors();
            ViewBag.ColorsList = FillPropertyList(null, colors, null);
            var growingSeaznos = _plantService.GetGrowingSeazons();
            ViewBag.GrowingSeazons = FillPropertyList(null, null, growingSeaznos);
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
        public IActionResult Edit(int id)
        {
            var plantToEdit = _plantService.GetPlantToEdit(id);
            var colors = _plantService.GetColors();
            ViewBag.ColorsList = FillPropertyList(null, colors, null);

            var growingSeaznos = _plantService.GetGrowingSeazons();
            ViewBag.GrowingSeazons = FillPropertyList(null, null, growingSeaznos);
           
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
         var plant = _plantService.DeletePlant(id);
         return RedirectToAction("Index",plant);
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

        private List<SelectListItem> FillPropertyList(List<PlantTypesVm> list, List<ColorsVm> colorList, List<GrowingSeazonVm> seazonList)
        {
            List<SelectListItem> propertyList = new List<SelectListItem>();

            if (list != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in list)
                {
                    propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                }
            }
            if (colorList != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in colorList)
                {
                    propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                }
            }
            if (seazonList != null)
            {
                //propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in seazonList)
                {
                    propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                }
            }
            else
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });
            }

            return propertyList;
        }
    }
}
