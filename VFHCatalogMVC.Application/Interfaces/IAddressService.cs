using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Plant;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IAddressService
    {
        AddressVm GetAddres(int id);
        List<CountryVm> GetCountries();
        List<VoivodeshipVm> GetVoivodeships(int countryId);
        List<CityVm> GetCities(int voivodeshipId);
        List<SelectListItem> FillCountryList(List<CountryVm> countries);
        List<SelectListItem> FillVoivodeshipList(List<VoivodeshipVm> voivodeships);
        List<SelectListItem> FillCityList(List<CityVm> city);
        void AddAddress(AddressVm address);
        List<string> FilterUsers(int countryId, int voivodeshipId, int cityId, List<PlantSeedVm> seeds);



    }
}
