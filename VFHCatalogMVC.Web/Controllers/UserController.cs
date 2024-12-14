
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using VFHCatalogMVC.Application.Constants;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.Services;
using VFHCatalogMVC.Application.ViewModels.Message;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Application.ViewModels.User;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserPlantService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IPlantService _plantService;
        private readonly IMessageService _messageService;
        private readonly IPlantHelperService _plantHelperService;
        private readonly IUserContactDataService _userHelperService;

        public UserController(
            IUserPlantService userService, 
            ILogger<UserController> logger, 
            IPlantService plantService, 
            IMessageService messageService, 
            IPlantHelperService plantHelperService,
            IUserContactDataService userHelperService
            )
        {
            _userService = userService;
            _logger = logger;
            _plantService = plantService;
            _messageService = messageService;
            _plantHelperService = plantHelperService;
            _userHelperService = userHelperService;
        }

        [HttpGet, HttpPost]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult IndexSeeds(
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

                if (!pageNo.HasValue)
                {
                    pageNo = 1;
                }
                if (searchString is null)
                {
                    searchString = string.Empty;
                }
                if (pageSize == 0)
                {
                    pageSize = 30;
                }
                if (typeId != 0)
                    ViewBag.TypeId = typeId;
                if (groupId != 0)
                    ViewBag.GroupId = groupId;
                if (sectionId != 0)
                    ViewBag.SectionId = sectionId;

                var model = _userService.GetUserSeeds(pageSize, pageNo, searchString, typeId, groupId, sectionId, User.Identity.Name);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet, HttpPost]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult IndexSeedlings(
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
                ViewBag.GroupsList = _plantHelperService.GetGroups(typeId);
                ViewBag.SectionsList = _plantHelperService.GetSections(groupId);

                if (!pageNo.HasValue)
                {
                    pageNo = 1;
                }
                if (searchString is null)
                {
                    searchString = string.Empty;
                }
                if (pageSize == 0)
                {
                    pageSize = 30;
                }
                if (typeId != 0)
                    ViewBag.TypeId = typeId;
                if (groupId != 0)
                    ViewBag.GroupId = groupId;
                if (sectionId != 0)
                    ViewBag.SectionId = sectionId;

                var model = _userService.GetUserSeedlings(pageSize, pageNo, searchString, typeId, groupId, sectionId, User.Identity.Name);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }


        [HttpGet, HttpPost]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        public IActionResult IndexNewPlants(
            int pageSize, 
            int? pageNo, 
            string searchString, 
            int typeId, 
            int groupId,
            int? sectionId, 
            bool viewAll)
        {
            try
            {
                ViewBag.TypesList = _plantHelperService.GetSelectList<PlantType, PlantTypesVm>();
                ViewBag.GroupsList = _plantHelperService.GetGroups(typeId);
                ViewBag.SectionsList = _plantHelperService.GetSections(groupId);

                if (!pageNo.HasValue)
                {
                    pageNo = 1;
                }
                if (searchString is null)
                {
                    searchString = string.Empty;
                }
                if (pageSize == 0)
                {
                    pageSize = 30;
                }
                if (typeId != 0)
                    ViewBag.TypeId = typeId;
                if (groupId != 0)
                    ViewBag.GroupId = groupId;
                if (sectionId != 0)
                    ViewBag.SectionId = sectionId;

                var model = _userService.GetNewUserPlants(pageSize, pageNo, typeId, groupId, sectionId, viewAll, User.Identity.Name);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }

        }

        [HttpGet]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult EditSeed(int id)
        {
            try
            {
                var seed = _userService.GetUserSeedToEdit(id);

                return PartialView("EditSeedModalPartial", seed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult EditSeed(UserSeedVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.UpdateSeed(model);
                    ViewBag.Message = "Zapisano";
                    ModelState.Clear();
                    //ViewData["JavaScript"] = " window.location.reload()" /*+ Url.Action("IndexSeeds") + "'"*/;
                    //return RedirectToAction("IndexSeeds"/*,"User"*/);
                }
                else
                {
                    ViewBag.Message = "Wystąpił bład podczas zapisu. Spróbuj ponownie.";
                }
                return PartialView("EditSeedModalPartial", model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult DeleteSeed(int id)
        {
            try
            {
                _userService.DeleteItem<PlantSeed>(id);
                return RedirectToAction("IndexSeeds");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult EditSeedling(int id)
        {
            try
            {
                var seedling = _userService.GetUserSeedlingToEdit(id);

                return PartialView("EditSeedlingModalPartial", seedling);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult EditSeedling(UserSeedlingVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.UpdateSeedling(model);
                    ViewBag.Message = "Zapisano";
                    ModelState.Clear();
                    //return RedirectToAction("IndexSeedlings","User");
                }
                else
                {
                    ViewBag.Message = "Wystąpił bład podczas zapisu. Spróbuj ponownie.";
                }

                return PartialView("EditSeedlingModalPartial", model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.PRIVATEUSER_COMPANY)]
        public IActionResult DeleteSeedling(int id)
        {
            try
            {
                _userService.DeleteItem<PlantSeedling>(id);
                return RedirectToAction("IndexSeedlings");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public JsonResult GetRegions(int id)
        {
            var regions = _userHelperService.GetRegions(id);

            List<SelectListItem> voivodeshipsList = new List<SelectListItem>();

            if (regions.Count > 0)
            {

                voivodeshipsList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var group in regions)
                {
                    voivodeshipsList.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
                }
            }

            return Json(voivodeshipsList);
        }

        [HttpPost]
        public JsonResult GetCities(int id)
        {
            var cities = _userHelperService.GetCities(id);

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


    }
}
