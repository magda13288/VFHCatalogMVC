using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Plant;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IUserService
    {
        AddressVm GetAddress(string userId);
        List<CountryVm> GetCountries();
        List<RegionVm> GetRegions(int countryId);
        List<CityVm> GetCities(int regionId);
        List<SelectListItem> FillCountryList(List<CountryVm> countries);
        List<SelectListItem> FillRegionList(List<RegionVm> regions);
        List<SelectListItem> FillCityList(List<CityVm> city);
        void AddAddress(AddressVm address);
        List<string> FilterUsers(int countryId, int regionId, int cityId, List<PlantSeedVm> seeds,List<PlantSeedlingVm> seedlings);



    }
}
