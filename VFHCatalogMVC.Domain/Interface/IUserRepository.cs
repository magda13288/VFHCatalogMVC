using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;
namespace VFHCatalogMVC.Domain.Interface
{
    public interface IUserRepository
    {
        IQueryable<T> GetUserPlantEntity<T>(string userId) where T : BaseEntityProperty;
        Task<T> GetUserEntityAsync<T>(int id) where T : BaseEntityProperty;
        Task EditEntityAsync<T>(T entity) where T : BasePlantSeedSeedlingProperty;
        Task<int> AddEntityAsync<T>(T entity) where T : class;
        Task<int> DeleteEntityAsync<T>(T entity) where T : class;
        IQueryable<T> GetEntity<T>() where T : class;
        Task<Address> GetAddressAsync(int id);
        IQueryable<Region> GetRegions(int countryId);
        IQueryable<City> GetCities(int regionId);
        Task<Address> GetAddressInfoAsync(string userId);
        Task<ContactDetail> GetContactDetailAsync(int? id);
        Task<int?> GetContactDetailForSeedAsync(int id);
        Task<int?> GetContactDetailForSeedlingAsync(int id);
        Task EditContactDetailsAsync(ContactDetail contact);    

    }
}
