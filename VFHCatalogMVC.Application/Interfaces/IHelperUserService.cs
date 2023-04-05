using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IHelperUserService
    {
        List<SelectListItem> Countries();
        List<SelectListItem> Regions(int countryId);
        List<SelectListItem> Cities(int regionId);
        string UserAccountName(Task<ApplicationUser> user);
        MessageDisplay MessagesToView(int type);
        List<SelectListItem> FillCountryList(List<CountryVm> countries);
        List<SelectListItem> FillRegionList(List<RegionVm> regions);
        List<SelectListItem> FillCityList(List<CityVm> city);
        List<CountryVm> GetCountries();
        List<RegionVm> GetRegions(int countryId);
        List<CityVm> GetCities(int regionId);

    }
}
