using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.User;
using VFHCatalogMVC.Domain.Model;

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
        UserSeedsForListVm GetUserSeeds(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName);
        UserSeedlingsForListVm GetUserSeedlings (int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName);
        UserSeedsVm GetSeedsList(Plant plant, UserSeedsVm item);
        UserSeedlingVm GetSeedlingsList(Plant plant, UserSeedlingVm item);
        UserSeedsVm GetUserSeedToEdit(int id);
        void UpdateSeed(UserSeedsVm seed);
        void DeleteSeed(int id);
        UserSeedlingVm GetUserSeedlingToEdit(int id);
        void UpdateSeedling(UserSeedlingVm seedling);
        void DeleteSeedling(int id);


    }
}
