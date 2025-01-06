using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using VFHCatalogMVC.Domain.Model;

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
        List<SelectListItem> FillList<TVm>(List<TVm> items) where TVm : SelectListItemVm;
        Task<AddressVm> GetAddressAsync(string userId);
        Task AddAddressAsync(AddressVm address);
        string UserAccountName(ApplicationUser user);
    }
}
