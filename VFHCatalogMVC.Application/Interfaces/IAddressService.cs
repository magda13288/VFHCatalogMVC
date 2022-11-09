using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Adresses;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IAddressService
    {
        AddressVm GetAddres(int id);
        List<CountryVm> GetCountries();
        List<VoivodeshipVm> GetVoivodeships(int countryId);
        List<CityVm> GetCities(int voivodeshipId);

        List<SelectListItem> FillCountryList(List<CountryVm> countries);
        void AddAddress(AddressVm address);



    }
}
