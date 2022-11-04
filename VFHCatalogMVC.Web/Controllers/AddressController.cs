using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VFHCatalogMVC.Application.Interfaces;

namespace VFHCatalogMVC.Web.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public JsonResult GetVoivodeships(int id)
        {
            var voivodeships = _addressService.GetVoivodeships(id);

            List<SelectListItem> voivodeshipsList = new List<SelectListItem>();

            if (voivodeships.Count > 0)
            {

                voivodeshipsList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var group in voivodeships)
                {
                    voivodeshipsList.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
                }
            }

            return Json(voivodeshipsList);
        }

        [HttpPost]
        public JsonResult GetCities(int id)
        {
            var cities = _addressService.GetCities(id);

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
