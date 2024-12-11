using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;
namespace VFHCatalogMVC.Domain.Interface
{
    public interface IUserRepository
    {
        IQueryable<T> GetUserPlantEntity<T>(string userId) where T : BaseEntityProperty;
        T GetUserEntity<T>(int id) where T : BaseEntityProperty;
        Address GetAddress(int id);
        IQueryable<Country> GetCountries();
        IQueryable<Region> GetRegions(int countryId);
        IQueryable<City> GetCities(int regionId);
        void AddAddress(Address address);
        Address GetAddressInfo(string userId);
        void EditUserSeed(PlantSeed seed);
        void DeleteUserSeed(PlantSeed seed);
        void EditUserSeedling(PlantSeedling seedling);
        void DeleteUserSeedling(PlantSeedling seedling);
        ContactDetail GetContactDetail(int? id);
        int? GetContactDetailForSeed(int id);
        int? GetContactDetailForSeedling(int id);
        void EditContactDetails(ContactDetail contact);
        void AddNewUserPlant(NewUserPlant plant);       
        IQueryable<NewUserPlant> GetAllNewUserPlants();

    }
}
