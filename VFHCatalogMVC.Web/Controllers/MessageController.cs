using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Services;
using VFHCatalogMVC.Application.ViewModels.Message;
using VFHCatalogMVC.Application.Constants;

namespace VFHCatalogMVC.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessageController> _logger;
        private readonly IPlantHelperService _helperService;

        public MessageController(
            IMessageService messageService,
            ILogger<MessageController> logger, 
            IPlantHelperService helperService)
        {
            _messageService = messageService;
            _logger = logger;
            _helperService = helperService;
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        //id -> PlantSeedId or PlantSeedlindId
        public IActionResult SendPlantMessage(
            int id,
            bool seeds,
            bool seedlings, 
            bool newPlant,
            string ownerId
            )
        {
            var indexPlant = _helperService.GetIndexPlantType(seeds, seedlings, newPlant);
            var message = _messageService.FillMessageProperties(/*plantId,*/id, User.Identity.Name, indexPlant, ownerId);

            return PartialView("SendPlantMessageModal", message);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.ALL_ROLES)]

        public IActionResult SendPlantMessage(MessageVm message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _messageService.SendMessage(message);
                    ViewBag.Message = "Zapisano";
                    ModelState.Clear();
                    return PartialView("SendPlantMessageModal");
                    //return PartialView("SendMessageToAdminModal", message);
                }
                else
                {
                    ViewBag.Message = "Wystąpił bład podczas zapisu. Spróbuj ponownie.";
                    return PartialView("SendPlantMessageModal", message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet,HttpPost]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        public IActionResult IndexPlantMessages(
            int id,
            int pageSize,
            int? pageNo,
            int type,
            bool seeds,
            bool seedlings,
            bool newPlant
            )
        {

            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (pageSize == 0)
            {
                pageSize = 30;
            }

            var messageDisplay = _helperService.MessagesToView(type);
            var index = _helperService.GetIndexPlantType(seeds, seedlings, newPlant);

            var messages = _messageService.GetMessagesForPlant(id, pageSize, pageNo, messageDisplay, index, User.Identity.Name);
            // return PartialView("PlantMessagesFromAdminModal",messages);
            return View(messages);
        }

        [HttpGet,HttpPost]
        [Authorize(Roles = UserRoles.ALL_ROLES)]
        public IActionResult IndexMessages(
            int pageSize, 
            int? pageNo, 
            int type, 
            string userName
            )
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (pageSize == 0)
            {
                pageSize = 30;
            }

            var messageDisplay = _helperService.MessagesToView(type);

            var messages = _messageService.GetMessages(pageSize, pageNo, messageDisplay, User.Identity.Name);

            return View(messages);
        }
    }
}
