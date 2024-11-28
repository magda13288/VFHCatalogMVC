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
using VFHCatalogApi.Models;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogApi.Controllers
{

    [Route("api/plant")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;
        private readonly IPlantDetailsService _plantDetailsSerrvice;
        private readonly IUserContactDataService _userContactDataService;
        private readonly ILogger<PlantController> _logger;
        private readonly IPlantHelperService _plantHelperService;

        public PlantController(IPlantService plantService, ILogger<PlantController> logger, IUserContactDataService userContactDataService, IPlantHelperService plantHelperService, IPlantDetailsService plantDetailsSerrvice)
        {
            _plantService = plantService;
            _logger = logger;
            _plantDetailsSerrvice = plantDetailsSerrvice;
            _plantHelperService = plantHelperService;
            _userContactDataService = userContactDataService;
        }

        [HttpPost("Index"), HttpGet("Index")]
        [AllowAnonymous]
        public ActionResult<ListPlantForListVm> Index([FromBody] IndexPlant properties)
        {
            try
            {

                if (!properties.pageNo.HasValue)
                {
                   properties.pageNo = 1;
                }
                if (properties.searchString is null)
                {
                    properties.searchString = string.Empty;
                }
               
                var model = _plantService.GetAllActivePlantsForList(properties.pageSize, properties.pageNo, properties.searchString, properties.typeId, properties.groupId, properties.sectionId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet("IndexSeeds"), HttpPost("IndexSeeds")]
        [Authorize(Roles = "PrivateUser,Company")]
        public ActionResult<PlantSeedsForListVm> IndexSeeds([FromRoute]int id, IndexSeedSeedling properties)
        {
            try
            {
            

                if (!properties.pageNo.HasValue)
                {
                   properties.pageNo = 1;
                }
                if (properties.pageSize == 0)
                {
                   properties.pageSize = 30;
                }
                
                var model = _plantService.GetAllPlantSeeds(id, properties.countryId, properties.regionId, properties.cityId,properties.pageSize,properties.pageNo,properties.isCompany, User.Identity.Name);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet("IndexSeedlings"), HttpPost("IndexSeedlings")]
        [Authorize(Roles = "PrivateUser,Company")]
        public IActionResult IndexSeedlings([FromBody] int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany)
        {
            try
            {
                

                if (!pageNo.HasValue)
                {
                    pageNo = 1;
                }
                if (pageSize == 0)
                {
                    pageSize = 30;
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

        [HttpGet("AddPlant")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddPlant()
        {
            
            return NoContent();
        }

        [HttpPost("AddPlant")]
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
               
                return BadRequest(ModelState.ErrorCount);
            }
        }

        [AllowAnonymous]
        [HttpGet("Details")]
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

        [HttpGet("Edit")]
        [Authorize(Roles = "Admin")]
        public ActionResult<NewPlantVm> Edit([FromBody] int id)
        {
            try
            {
                var plantToEdit = _plantService.GetPlantToEdit(id);               

                return Ok(plantToEdit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost("Edit")]
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

        [HttpDelete]
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

        [HttpGet("AddSeed")]
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

        [HttpPost("AddSeed")]
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

        [HttpGet("AddSeedling")]
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

        [HttpPost("AddSeedling")]
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

        [HttpGet("AddOpinion")]
        [Authorize(Roles = "PrivateUser,Company")]
        public ActionResult<PlantOpinionsVm> AddOpinion([FromBody]int id)
        {
            try
            {
                var plantOpinion = _plantDetailsSerrvice.FillPropertyOpinion(id, User.Identity.Name);
                //return PartialView("AddOpinionModalPartial", plantOpinion);
                return Ok(plantOpinion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost("AddOpinion")]
        [Authorize(Roles = "PrivateUser,Company")]
        //Add refereshing page after save opinion on modal popup
        public IActionResult AddOpinion([FromBody] PlantOpinionsVm plantOpinion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _plantDetailsSerrvice.AddPlantOpinion(plantOpinion);
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

}
}
