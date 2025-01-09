
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
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
        public async Task<IActionResult> IndexSeeds(
            int pageSize,
            int? pageNo,
            string searchString, 
            int typeId, 
            int groupId, 
            int? sectionId)
        {
            try
            {
                ViewBag.TypesList = await _plantHelperService.GetSelectListAsync<PlantType, PlantTypesVm>();
                var groupsList = await GetPlantGroupsList(typeId);
                ViewBag.GroupsList = groupsList.Value;
                var sectionsList = await GetPlantSectionsList(groupId, typeId);
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

                var model = await _userService.GetUserSeedsAsync(pageSize, pageNo, searchString, typeId, groupId, sectionId, User.Identity.Name);

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
        public async Task<IActionResult> IndexSeedlings(
            int pageSize,
            int? pageNo, 
            string searchString, 
            int typeId, 
            int groupId,
            int? sectionId)
        {
            try
            {
                ViewBag.TypesList = await _plantHelperService.GetSelectListAsync<PlantType, PlantTypesVm>();
                ViewBag.GroupsList = await _plantHelperService.GetGroupsAsync(typeId);
                ViewBag.SectionsList = await _plantHelperService.GetSectionsAsync(groupId);

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

                var model = await _userService.GetUserSeedlingsAsync(pageSize, pageNo, searchString, typeId, groupId, sectionId, User.Identity.Name);

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
        public async Task<IActionResult> IndexNewPlants(
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
                ViewBag.TypesList = await _plantHelperService.GetSelectListAsync<PlantType, PlantTypesVm>();
                ViewBag.GroupsList = await _plantHelperService.GetGroupsAsync(typeId);
                ViewBag.SectionsList = await _plantHelperService.GetGroupsAsync(groupId);

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

                var model = await _userService.GetNewUserPlantsAsync(pageSize, pageNo, typeId, groupId, sectionId, viewAll, User.Identity.Name);

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
        public async Task<IActionResult> EditSeed(int id)
        {
            try
            {
                var seed = await _userService.GetUserSeedToEditAsync(id);

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
        public async Task<IActionResult> EditSeed(UserSeedVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userService.UpdateSeedAsync(model);
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
        public async Task<IActionResult> DeleteSeed(int id)
        {
            try
            {
                await _userService.DeleteItemAsync<PlantSeed>(id);
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
        public async Task<IActionResult> EditSeedling(int id)
        {
            try
            {
                var seedling = await _userService.GetUserSeedlingToEditAsync(id);

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
        public async Task<IActionResult> EditSeedling(UserSeedlingVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userService.UpdateSeedlingAsync(model);
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
        public async Task<IActionResult> DeleteSeedling(int id)
        {
            try
            {
                await _userService.DeleteItemAsync<PlantSeedling>(id);
                return RedirectToAction("IndexSeedlings");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetRegions(int id)
        {
            var regions = await _userHelperService.Regions(id);         

            return Json(regions);
        }

        [HttpPost]
        public async Task<JsonResult> GetCities(int id)
        {
            var cities = await _userHelperService.Cities(id);            

            return Json(cities);
        }
        [HttpPost]
        public async Task<JsonResult> GetPlantGroupsList(int typeId)
        {
            var groups = await _plantHelperService.GetGroupsAsync(typeId);       

            return Json(groups);

        }

        [HttpPost]
        public async Task<JsonResult> GetPlantSectionsList(int groupId, int typeId)
        {

            var sections = await _plantHelperService.GetGroupsAsync(groupId);
           
            return Json(sections);
        }


    }
}
