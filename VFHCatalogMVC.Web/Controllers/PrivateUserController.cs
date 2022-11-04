using Microsoft.AspNetCore.Mvc;
using VFHCatalogMVC.Application.Interfaces;

namespace VFHCatalogMVC.Web.Controllers
{
    public class PrivateUserController : Controller
    {
        private readonly IPrivateUserService _userService;
        public PrivateUserController(IPrivateUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
