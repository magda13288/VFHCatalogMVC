using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Model;
namespace VFHCatalogMVC.Domain.Interface
{
    public interface IUserRepository
    {
        Address GetAddress(int id);
        IQueryable<Country> GetCountries();
        IQueryable<Region> GetRegions(int countryId);
        IQueryable<City> GetCities(int regionId);
        void AddAddress(Address address);
        Address GetAddressInfo(string userId);
        void EditUserSeed(PlantSeed seed);
        PlantSeed GetUserSeed(int id);
        void DeleteUserSeed(PlantSeed seed);
        void EditUserSeedling(PlantSeedling seedling);
        PlantSeedling GetUserSeedling(int id);
        void DeleteUserSeedling(PlantSeedling seedling);
    }
}
