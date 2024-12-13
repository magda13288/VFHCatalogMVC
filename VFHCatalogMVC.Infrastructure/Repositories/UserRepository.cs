using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public int AddEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public int DeleteEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }      
        public void EditContactDetails(ContactDetail contact)
        {
            _context.Attach(contact);
            _context.Entry(contact).Property("ContactDetailInformation").IsModified = true;
            _context.SaveChanges();
        }

        public void EditEntity<T>(T entity) where T: BasePlantSeedSeedlingProperty
        {
            _context.Attach(entity);
            _context.Entry(entity).Property(e=>e.Count).IsModified = true;
            _context.Entry(entity).Property(e=>e.Description).IsModified = true;
            _context.Entry(entity).Property(e=>e.DateAdded).IsModified = true;
            _context.SaveChanges();
        }
        
        public Address GetAddress(int id)
        {
            var address = _context.Addresses.FirstOrDefault(p => p.Id == id);
            return address;
        }

        public Address GetAddressInfo(string userId)
        {
            var address = _context.Addresses.FirstOrDefault(p => p.UserId == userId);
            return address;
        }

        public IQueryable<City> GetCities(int regionId)
        {
            var cities = _context.Cities.Where(p => p.RegionId == regionId);
            return cities;
        }

        public ContactDetail GetContactDetail(int? id)
        {
            var contactDetail = _context.ContactDetails.AsNoTracking().FirstOrDefault(p => p.Id == id);
            return contactDetail;
        }

        public int? GetContactDetailForSeed(int id)
        {
            var contactDetails = _context.ContactDetailForSeeds.FirstOrDefault(p => p.PlantSeedId == id);
            if (contactDetails == null)
                return null;
            else
            return contactDetails.ContactDetailId;
        }

        public int? GetContactDetailForSeedling(int id)
        {
            var contactDetails = _context.ContactDetailForSeedlings.FirstOrDefault(p => p.PlantSeedlingId == id);
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
           var regions = _context.Regions.Where(p => p.CountryId == countryId);
            return regions;
        }

        public IQueryable<T> GetUserPlantEntity<T>(string userId) where T : BaseEntityProperty
        {
            var entity = _context.Set<T>().Where(p=>p.UserId == userId);
            return entity;
        }

        public T GetUserEntity<T>(int id) where T : BaseEntityProperty
        {
            var entity = _context.Set<T>().FirstOrDefault(p=>p.Id == id);
            return entity;
            
        }      
    }
}
