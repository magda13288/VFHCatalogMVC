using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Services;
using VFHCatalogMVC.Application.ViewModels.Message;

namespace VFHCatalogMVC.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessageController> _logger;
        private readonly IHelperService _helperService;

        public MessageController(IMessageService messageService, ILogger<MessageController> logger, IHelperService helperService)
        {
            _messageService = messageService;
            _logger = logger;
            _helperService = helperService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,PrivateUser,Company")]

        public IActionResult SendNewPlantUserMessage(int id, bool seeds, bool seedlings, bool newPlant)
        {
            var indexPlant = _helperService.GetIndexPlantType(seeds, seedlings, newPlant);
            var message = _messageService.FillMessageProperties(id, User.Identity.Name, indexPlant);

            return PartialView("SendNewPlantUserMessageModal", message);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PrivateUser,Company")]

        public IActionResult SendNewPlantUserMessage(MessageVm message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _messageService.SendNewPlantMessage(message);
                    ViewBag.Message = "Zapisano";
                    ModelState.Clear();
                    return PartialView("SendNewPlantUserMessageModal");
                    //return PartialView("SendMessageToAdminModal", message);
                }
                else
                {
                    ViewBag.Message = "Wystąpił bład podczas zapisu. Spróbuj ponownie.";
                    return PartialView("SendNewPlantUserMessageModal", message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,PrivateUser,Company")]
        public IActionResult NewPlantMessages(int id, int pageSize, int? pageNo, int type, bool seeds, bool seedlings, bool newPlant)
        {

            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (pageSize == 0)
            {
                pageSize = 10;
            }

            var messageDisplay = _helperService.MessagesToView(type);
            var index = _helperService.GetIndexPlantType(seeds, seedlings, newPlant);

            var messages = _messageService.GetMessagesForPlant(id, pageSize, pageNo, messageDisplay, index, User.Identity.Name);
            // return PartialView("PlantMessagesFromAdminModal",messages);
            return View(messages);
        }
    }
}
