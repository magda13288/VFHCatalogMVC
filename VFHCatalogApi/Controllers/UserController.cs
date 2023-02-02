using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VFHCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IPlantService _plantService;

        public UserController(IUserService userService, ILogger<UserController> logger, IPlantService plantService)
        {
            _userService = userService;
            _logger = logger;
            _plantService = plantService;

        }

        [HttpGet, HttpPost]
        [Authorize(Roles = "PrivateUser,Company")]
        public ActionResult<UserSeedsForListVm> IndexSeeds([FromBody] int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId)
        {
            try
            {
                var types = _plantService.GetPlantTypes();
                var viewBagTypesList = _plantService.FillPropertyList(types, null, null);
                var groupsList = GetPlantGroupsList(typeId);
                var viewBagGroupsList = groupsList;
                var sectionsList = GetPlantSectionsList(groupId, typeId);
                var viewBagSectionsList = sectionsList;
                int viewBagTypeId, viewBagGroupId;
                int? viewBagSectionId;

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
                    viewBagTypeId = typeId;
                if (groupId != 0)
                    viewBagGroupId = groupId;
                if (sectionId != 0)
                    viewBagSectionId = sectionId;

                var model = _userService.GetUserSeeds(pageSize, pageNo, searchString, typeId, groupId, sectionId, User.Identity.Name);

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet, HttpPost]
        [Authorize(Roles = "PrivateUser,Company")]
        public ActionResult<UserSeedlingsForListVm> IndexSeedlings([FromBody] int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId)
        {
            try
            {
                var types = _plantService.GetPlantTypes();
                var viewBagTypesList = _plantService.FillPropertyList(types, null, null);
                var groupsList = GetPlantGroupsList(typeId);
                var viewBagGroupsList = groupsList;
                var sectionsList = GetPlantSectionsList(groupId, typeId);
                var viewBagSectionsList = sectionsList;
                int viewBagTypeId, viewBagGroupId;
                int? viewBagSectionId;

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
                    viewBagTypeId = typeId;
                if (groupId != 0)
                    viewBagGroupId = groupId;
                if (sectionId != 0)
                    viewBagSectionId = sectionId;

                var model = _userService.GetUserSeedlings(pageSize, pageNo, searchString, typeId, groupId, sectionId, User.Identity.Name);

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet/*("{id}")*/]
        [Authorize(Roles = "PrivateUser,Company")]
        public ActionResult<UserSeedVm> EditSeed([FromBody] int id)
        {
            try
            {
                var seed = _userService.GetUserSeedToEdit(id);

                //return PartialView("EditSeedModalPartial", seed);
                return Ok(seed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        [HttpPost]
        [Authorize(Roles = "PrivateUser,Company")]
        public IActionResult EditSeed([FromBody] UserSeedVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.UpdateSeed(model);
                    //ViewData["JavaScript"] = " window.location.reload()" /*+ Url.Action("IndexSeeds") + "'"*/;
                    // return RedirectToAction("IndexSeeds", "User");
                    return Ok();
                }
                else
                {
                    //return PartialView("EditSeedModalPartial", model);
                    return BadRequest(ModelState.ErrorCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpDelete/*("{id}")*/]
        [Authorize(Roles = "PrivateUser,Company")]
        public IActionResult DeleteSeed([FromBody] int id)
        {
            try
            {
                _userService.DeleteSeed(id);
                //return RedirectToAction("IndexSeeds");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet/*("{id}")*/]
        [Authorize(Roles = "PrivateUser,Company")]
        public ActionResult<UserSeedlingVm> EditSeedling([FromBody]int id)
        {
            try
            {
                var seedling = _userService.GetUserSeedlingToEdit(id);

                //return PartialView("EditSeedlingModalPartial", seedling);
                return Ok(seedling);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "PrivateUser,Company")]
        public IActionResult EditSeedling([FromBody] UserSeedlingVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.UpdateSeedling(model);
                    //return RedirectToAction("IndexSeedlings");
                    return Ok();
                }
                else
                {
                    //return PartialView("EditSeedlingModalPartial", model);
                    return BadRequest(ModelState.ErrorCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpDelete/*("{id}")*/]
        [Authorize(Roles = "PrivateUser,Company")]
        public IActionResult DeleteSeedling([FromBody] int id)
        {
            try
            {
                _userService.DeleteSeedling(id);
                //return RedirectToAction("IndexSeedlings");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        private List<SelectListItem> GetRegions([FromBody]int id)
        {
            var regions = _userService.GetRegions(id);

            List<SelectListItem> voivodeshipsList = new List<SelectListItem>();

            if (regions.Count > 0)
            {

                voivodeshipsList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var group in regions)
                {
                    voivodeshipsList.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
                }
            }

            return voivodeshipsList;
        }

        [HttpPost]
        private List<SelectListItem> GetCities([FromBody] int id)
        {
            var cities = _userService.GetCities(id);

            List<SelectListItem> citiesList = new List<SelectListItem>();

            if (cities.Count > 0)
            {

                citiesList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var group in cities)
                {
                    citiesList.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
                }
            }

            return citiesList;
        }
        [HttpPost]
        private List<SelectListItem> GetPlantGroupsList([FromBody] int typeId)
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

            return groupsList;

        }

        [HttpPost]
        private List<SelectListItem> GetPlantSectionsList([FromBody] int groupId, int typeId)
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
            return sectionsList;
        }

    }
}
