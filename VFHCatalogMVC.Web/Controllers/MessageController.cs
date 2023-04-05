using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Services;
using VFHCatalogMVC.Application.ViewModels.Message;

namespace VFHCatalogMVC.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessageController> _logger;
        private readonly IHelperPlantService _helperPlantService;
        private readonly IHelperUserService _helperUserService;

        public MessageController(IMessageService messageService, ILogger<MessageController> logger, IHelperPlantService helperPlantService, IHelperUserService helperUserService)
        {
            _messageService = messageService;
            _logger = logger;
            _helperPlantService = helperPlantService;
            _helperUserService = helperUserService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,PrivateUser,Company")]
        //id -> PlantSeedId or PlantSeedlindId
        public IActionResult SendPlantMessage(/*int plantId, */int id, bool seeds, bool seedlings, bool newPlant,string ownerId)
        {
            var indexPlant = _helperPlantService.GetIndexPlantType(seeds, seedlings, newPlant);
            var message = _messageService.FillMessageProperties(/*plantId,*/id, User.Identity.Name, indexPlant, ownerId);

            return PartialView("SendPlantMessageModal", message);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PrivateUser,Company")]

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
        [Authorize(Roles = "Admin,PrivateUser,Company")]
        public IActionResult IndexPlantMessages(int id, int pageSize, int? pageNo, int type, bool seeds, bool seedlings, bool newPlant)
        {

            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (pageSize == 0)
            {
                pageSize = 30;
            }

            var messageDisplay = _helperUserService.MessagesToView(type);
            var index = _helperPlantService.GetIndexPlantType(seeds, seedlings, newPlant);

            var messages = _messageService.GetMessagesForPlant(id, pageSize, pageNo, messageDisplay, index, User.Identity.Name);
            // return PartialView("PlantMessagesFromAdminModal",messages);
            return View(messages);
        }

        [HttpGet,HttpPost]
        [Authorize(Roles = "Admin,PrivateUser,Company")]
        public IActionResult IndexMessages(int pageSize, int? pageNo, int type, /*IndexPlantType index,*/ string userName)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (pageSize == 0)
            {
                pageSize = 30;
            }

            var messageDisplay = _helperUserService.MessagesToView(type);

            var messages = _messageService.GetMessages(pageSize, pageNo, messageDisplay, User.Identity.Name);

            return View(messages);
        }
    }
}
