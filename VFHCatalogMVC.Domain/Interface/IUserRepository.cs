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
        ContactDetail GetContactDetail(int? id);
        int? GetContactDetailForSeed(int id);
        int? GetContactDetailForSeedling(int id);
        void EditContactDetails(ContactDetail contact);
        void AddNewUserPlant(NewUserPlant plant);
        IQueryable<PlantSeed> GetUserPlantSeeds(string userId);
        IQueryable<PlantSeedling> GetUserPlantSeedlings(string userId);
        IQueryable<NewUserPlant> GetNewUserPlants(string userId);
        IQueryable<NewUserPlant> GetAllNewUserPlants();
        IQueryable<Filters> GetAllFilters(int typeId, int? groupId, int? sectionId);
        string GetFruitSizeValue(int id);
        string GetFruitTypeValue(int id);
        string GetGrowingSezaonValue(int id);
        string GetGrowthTypeValue(int id);
        string GetHeightValue(int id);
        string GetPollinationValue(int id);
        //ContactDetail GetContactDetails(string userId);
    }
}
