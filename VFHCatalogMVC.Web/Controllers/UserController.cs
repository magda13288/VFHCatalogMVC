using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VFHCatalogMVC.Application.Interfaces;

namespace VFHCatalogMVC.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public JsonResult GetRegions(int id)
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

            return Json(voivodeshipsList);
        }

        [HttpPost]
        public JsonResult GetCities(int id)
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

            return Json(citiesList);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
