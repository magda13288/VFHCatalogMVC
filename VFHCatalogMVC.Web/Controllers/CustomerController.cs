using Microsoft.AspNetCore.Mvc;
using VFHCatalogMVC.Application.Interfaces;

namespace VFHCatalogMVC.Web.Controllers
{
    public class Customer : Controller
    {
        private readonly ICustomerService _custromerService;

        public Customer(ICustomerService custromerService)
        {
            _custromerService = custromerService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
