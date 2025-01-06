using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private Context _context;
        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<int> AddEntityAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteEntityAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }      
        public async Task EditContactDetailsAsync(ContactDetail contact)
        {
            _context.Attach(contact);
            _context.Entry(contact).Property(e=>e.ContactDetailInformation).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task EditEntityAsync<T>(T entity) where T: BasePlantSeedSeedlingProperty
        {
            _context.Attach(entity);
            _context.Entry(entity).Property(e=>e.Count).IsModified = true;
            _context.Entry(entity).Property(e=>e.Description).IsModified = true;
            _context.Entry(entity).Property(e=>e.DateAdded).IsModified = true;
            await _context.SaveChangesAsync();
        }
        
        public async Task<Address> GetAddressAsync(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(p => p.Id == id);
            
        }

        public async Task<Address> GetAddressInfoAsync(string userId)
        {
           return await _context.Addresses.FirstOrDefaultAsync(p => p.UserId == userId);
            
        }

        public IQueryable<City> GetCities(int regionId)
        {
            var cities = _context.Cities.Where(p => p.RegionId == regionId);
            return cities;
        }

        public async Task<ContactDetail> GetContactDetailAsync(int? id)
        {
            var contactDetail = await _context.ContactDetails.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return contactDetail;
        }

        public async Task<int?> GetContactDetailForSeedAsync(int id)
        {
            var contactDetails = await _context.ContactDetailForSeeds.FirstOrDefaultAsync(p => p.PlantSeedId == id);
            if (contactDetails == null)
                return null;
            else
            return contactDetails.ContactDetailId;
        }

        public async Task<int?> GetContactDetailForSeedlingAsync(int id)
        {
            var contactDetails = await _context.ContactDetailForSeedlings.FirstOrDefaultAsync(p => p.PlantSeedlingId == id);
            if (contactDetails == null)
                return null;
            else
                return contactDetails.ContactDetailId;
        }

        public IQueryable<T> GetEntity<T>() where T : class
        {
            return _context.Set<T>();
        }
        public IQueryable<Region> GetRegions(int countryId)
        {
            return _context.Regions.Where(p => p.CountryId == countryId);
        }

        public IQueryable<T> GetUserPlantEntity<T>(string userId) where T : BaseEntityProperty
        {
            return _context.Set<T>().Where(p => p.UserId == userId);
        }

        public async Task<T> GetUserEntityAsync<T>(int id) where T : BaseEntityProperty
        {
            return await _context.Set<T>().FirstOrDefaultAsync(p => p.Id == id);

        }      
    }
}
