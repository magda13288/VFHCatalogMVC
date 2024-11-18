using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.ViewModels.Plant;

namespace VFHCatalogApi.Controllers
{

    [Route("api/plant")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;
        private readonly IPlantHelperService _plantHelperService;
        private readonly IPlantDetailsSerrvice _plantDetailsSerrvice;
        private readonly IUserContactDataService _userHelperService;
        private readonly ILogger<PlantController> _logger;

        public PlantController(IPlantService plantService, ILogger<PlantController> logger, IUserContactDataService userHelperService, IPlantDetailsSerrvice plantDetailsSerrvice, IPlantHelperService plantHelperService)
        {
            _plantService = plantService;
            _logger = logger;
            _userHelperService = userHelperService;
            _plantDetailsSerrvice = plantDetailsSerrvice;
            _plantHelperService = plantHelperService;
        }

        [HttpPost, HttpGet]
        [AllowAnonymous]
        public ActionResult<ListPlantForListVm> Index([FromBody]int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId)
        {
            try
            {
                var types = _plantHelperService.GetPlantTypes();
                var viewBagtypesList = _plantHelperService.FillPropertyList(types, null, null);
                var groupsList = GetPlantGroupsList(typeId);
                var viewBaggroupsList = groupsList;
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
                if (typeId != 0)
                    viewBagTypeId = typeId;
                if (groupId != 0)
                    viewBagGroupId = groupId;
                if (sectionId != 0)
                    viewBagSectionId = sectionId;

                var model = _plantService.GetAllActivePlantsForList(pageSize, pageNo, searchString, typeId, groupId, sectionId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet/*("{id}")*/, HttpPost]
        [Authorize(Roles = "PrivateUser,Company")]
        public ActionResult<PlantSeedsForListVm> IndexSeeds([FromBody]int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany)
        {
            try
            {
                var countries = _userHelperService.GetCountries();
                var viewBagCountriesList = _userHelperService.FillCountryList(countries);
                var regions = _userHelperService.GetRegions(countryId);
                var viewBagRegionsList = _userHelperService.FillRegionList(regions);
                var cities = _userHelperService.GetCities(regionId);
                var viewBagCitiesList = _userHelperService.FillCityList(cities);
                int viewBagCountryId, viewBagRegionId, viewBagCityId;

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
                    viewBagCountryId = countryId;
                }
                if (regionId != 0)
                {
                    viewBagRegionId = regionId;
                }
                if (cityId != 0)
                {
                    viewBagCityId = cityId;
                }

                var model = _plantService.GetAllPlantSeeds(id, countryId, regionId, cityId, pageSize, pageNo, isCompany, User.Identity.Name);
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
        public IActionResult IndexSeedlings([FromBody] int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany)
        {
            try
            {
                var countries = _userHelperService.GetCountries();
                var viewBagCountriesList = _userHelperService.FillCountryList(countries);
                var regions =   .GetRegions(countryId);
                var viewBagRegionsList = _userHelperService.FillRegionList(regions);
                var cities = _userHelperService.GetCities(regionId);
                var viewBagCitiesList = _userHelperService.FillCityList(cities);
                int viewBagCountryId, viewBagRegionId, viewBagCityId;

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
                    viewBagCountryId = countryId;
                }
                if (regionId != 0)
                {
                    viewBagRegionId = regionId;
                }
                if (cityId != 0)
                {
                    viewBagCityId = cityId;
                }

                var model = _plantService.GetAllPlantSeedlings(id, countryId, regionId, cityId, pageSize, pageNo, isCompany);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddPlant()
        {
            var types = _plantHelperService.GetPlantTypes();
            var viewBagTypesList = _plantHelperService.FillPropertyList(types, null, null);
            var colors = _plantDetailsSerrvice.GetColors();
            var viewBagColorsList = _plantHelperService.FillPropertyList(null, colors, null);
            var growingSeaznos = _plantDetailsSerrvice.GetGrowingSeazons();
            var viewBagGrowingSeazons = _plantHelperService.FillPropertyList(null, null, growingSeaznos);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult AddPlant([FromBody]NewPlantVm model)
        {
            if (ModelState.IsValid)
            {
                var id = _plantService.AddPlant(model, User.Identity.Name);
                return Created("Index", model);
            }
            else
            {
                var types = _plantHelperService.GetPlantTypes();
                var viewBagTypesList = _plantHelperService.FillPropertyList(types, null, null);
                var colors = _plantDetailsSerrvice.GetColors();
                var viewBagColorsList = _plantHelperService.FillPropertyList(null, colors, null);
                var growingSeaznos = _plantDetailsSerrvice.GetGrowingSeazons();
                var viewBagGrowingSeazons = _plantHelperService.FillPropertyList(null, null, growingSeaznos);
                //return RedirectToAction("AddPlant", model);
                return BadRequest(ModelState.ErrorCount);
            }
        }

        [AllowAnonymous]
        [HttpGet/*("{id}")*/]
        public ActionResult<PlantDetailsVm> Details([FromBody] int id)
        {
            var plantDetails = _plantDetailsSerrvice.GetPlantDetails(id);

            if (plantDetails == null)
            {
                //return RedirectToAction("Index");
                return NotFound();
            }
            else
            { 
            return Ok(plantDetails);
            }
        }

        [HttpGet/*("{id}")*/]
        [Authorize(Roles = "Admin")]
        public ActionResult<NewPlantVm> Edit([FromBody] int id)
        {
            try
            {
                var plantToEdit = _plantService.GetPlantToEdit(id);
                var colors = _plantDetailsSerrvice.GetColors();
                var viewBagColorsList = _plantHelperService.FillPropertyList(null, colors, null);

                var growingSeaznos = _plantDetailsSerrvice.GetGrowingSeazons();
                var viewBagGrowingSeazons = _plantHelperService.FillPropertyList(null, null, growingSeaznos);

                var growthTypes = GetGrowthTypes(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);
                var viewBagGrowthTypes = growthTypes;

                var destinations = GetDestinations();
                var viewBagDestinations = destinations;

                var fruitTypes = GetFruitTypes(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);
                var viewBagFruitTypes = fruitTypes;
                var fruitSize = GetFruitSizes(plantToEdit.TypeId, plantToEdit.GroupId, plantToEdit.SectionId);
                var viewBagFruitSizes = fruitSize;

                return Ok(plantToEdit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromBody] NewPlantVm plant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantService.UpdatePlant(plant);
                    return Ok();
                }
                else
                {
                    return RedirectToAction("Edit", plant);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpDelete/*("{id}")*/]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromBody] int id)
        {
            try
            {
                var plant = _plantService.DeletePlant(id);
                //return RedirectToAction("Index", plant);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet/*("{id}")*/]
        [Authorize(Roles = "PrivateUser,Company")]
        public ActionResult<PlantSeedVm> AddSeed([FromBody] int id)
        {
            try
            {
                var plantSedd = _plantService.FillProperties(id, User.Identity.Name);
                //return PartialView("AddSeedModalPartial", plantSedd);
                return Ok(plantSedd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "PrivateUser,Company")]
        public IActionResult AddSeed([FromBody] PlantSeedVm plantSeed)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantService.AddPlantSeed(plantSeed);
                    //return RedirectToAction("Index");
                    return Created("Index",plantSeed);
                }
                else
                {
                    // return PartialView("AddSeedModalPartial", plantSeed);
                    //return BadRequest("Invalid data. Fill fields again.");
                    return BadRequest(ModelState.ErrorCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet/*("{id}")*/]
        [Authorize(Roles = "PrivateUser,Company")]
        public ActionResult<PlantSeedlingVm> AddSeedling([FromBody] int id)
        {
            try
            {
                var plantSeedling = _plantService.FillPropertiesSeedling(id, User.Identity.Name);
                //return PartialView("AddSeedlingModalPartial", plantSeedling);
                return Ok(plantSeedling);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "PrivateUser,Company")]
        public IActionResult AddSeedling([FromBody] PlantSeedlingVm plantSeedling)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantService.AddPlantSeedling(plantSeedling);
                    // return RedirectToAction("Index");
                    return Created("Index",plantSeedling);

                }
                else
                {
                    //return PartialView("AddSeedlingModalPartial", plantSeedling);
                    return BadRequest(ModelState.ErrorCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }

        }

        [HttpGet/*("{id}")*/]
        [Authorize(Roles = "PrivateUser,Company")]
        public ActionResult<PlantOpinionsVm> AddOpinion([FromBody]int id)
        {
            try
            {
                var plantOpinion = _plantService.FillPropertyOpinion(id, User.Identity.Name);
                //return PartialView("AddOpinionModalPartial", plantOpinion);
                return Ok(plantOpinion);
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
        public IActionResult AddOpinion([FromBody] PlantOpinionsVm plantOpinion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantService.AddPlantOpinion(plantOpinion);
                    // return RedirectToAction("Details");
                    return Created("Details", plantOpinion);

                }
                else
                {
                    //return PartialView("AddOpinionModalPartial", plantOpinion);
                    return BadRequest(ModelState.ErrorCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }

        }

        [HttpPost]
        private List<SelectListItem> GetPlantGroupsList([FromBody] int typeId)
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

            return groupsList;
        }

        [HttpPost]
        private List<SelectListItem> GetPlantSectionsList([FromBody] int groupId, int typeId)
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
            return sectionsList;
        }

        [HttpPost]
        private List<SelectListItem> GetGrowthTypes([FromBody] int typeId, int groupId, int? sectionId)
        {
            var list = _plantHelperService.GetGrowthTypes(typeId, groupId, sectionId);

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

            return growthTypes;
        }

        [HttpPost]
        private List<SelectListItem> GetDestinations()
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

            return destinations;
        }

        [HttpPost]
        private List<SelectListItem> GetFruitTypes([FromBody] int typeId, int groupId, int? sectionId)
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

            return fruitTypesList;
        }

        [HttpPost]
        private List<SelectListItem> GetFruitSizes([FromBody] int typeId, int groupId, int? sectionId)
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

            return fruitSizesList;
        }
}
}
