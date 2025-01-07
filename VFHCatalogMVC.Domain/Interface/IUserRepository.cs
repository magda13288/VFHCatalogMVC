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
        void EditEntity<T>(T entity) where T : BasePlantSeedSeedlingProperty;
        int AddEntity<T>(T entity) where T : class;
        int DeleteEntity<T>(T entity) where T : class;
        IQueryable<T> GetEntity<T>() where T : class;
        Address GetAddress(int id);
        IQueryable<Region> GetRegions(int countryId);
        IQueryable<City> GetCities(int regionId);
        Address GetAddressInfo(string userId);
        ContactDetail GetContactDetail(int? id);
        int? GetContactDetailForSeed(int id);
        int? GetContactDetailForSeedling(int id);
        void EditContactDetails(ContactDetail contact);    

    }
}
