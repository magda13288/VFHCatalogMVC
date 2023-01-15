using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Message;

namespace VFHCatalogMVC.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,PrivateUser,Company")]

        public IActionResult SendNewUserPlantMessage(int id)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PrivateUser,Company")]

        public IActionResult SendNewUserPlantMessage(NewUserPlantMessageVm message)
        {
            return View(message);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,PrivateUser,Company")]

        public IActionResult SendMessageToAdmin(int id)
        {
            var message = _messageService.FillMessageProperties(id,User.Identity.Name);

            return PartialView("SendMessageToAdminModal",message); 
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PrivateUser,Company")]

        public IActionResult SendMessageToAdmin(MessageVm message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _messageService.SendMessageToAdmin(message);
                    ViewBag.Message = "Zapisano";
                    //return PartialView("SendMessageToAdminModal", message);
                }
                //else
                //{
                    return PartialView("Message//SendMessageToAdminModal", message);
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
    }
}
