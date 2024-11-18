using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using VFHCatalogMVC.Application.ViewModels.Adresses;

namespace VFHCatalogMVC.Application.Interfaces.UserInterfaces
{
    public interface IUserContactDataService
    {
        List<SelectListItem> Countries();
        List<SelectListItem> Regions(int countryId);
        List<SelectListItem> Cities(int regionId);
        List<CountryVm> GetCountries();
        List<RegionVm> GetRegions(int countryId);
        List<CityVm> GetCities(int regionId);
        List<SelectListItem> FillCountryList(List<CountryVm> countries);
        List<SelectListItem> FillRegionList(List<RegionVm> regions);
        List<SelectListItem> FillCityList(List<CityVm> city);
        AddressVm GetAddress(string userId);
        void AddAddress(AddressVm address);
    }
}
