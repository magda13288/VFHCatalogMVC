using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public void AddNewUserPlant(NewUserPlant plant)
        {
            _context.NewUserPlants.Add(plant);
            _context.SaveChanges();
        }

        public void DeleteUserSeed(PlantSeed seed)
        {
            _context.PlantSeeds.Remove(seed);
            _context.SaveChanges();
        }

        public void DeleteUserSeedling(PlantSeedling seedling)
        {
           _context.PlantSeedlings.Remove(seedling);
            _context.SaveChanges();
        }

        public void EditContactDetails(ContactDetail contact)
        {
            _context.Attach(contact);
            _context.Entry(contact).Property("ContactDetailInformation").IsModified = true;
            _context.SaveChanges();
        }

        public void EditUserSeed(PlantSeed seed)
        {
            _context.Attach(seed);
            _context.Entry(seed).Property("Count").IsModified = true;
            _context.Entry(seed).Property("Description").IsModified= true;
            _context.Entry(seed).Property("DateAdded").IsModified= true;
            _context.SaveChanges();
        }

        public void EditUserSeedling(PlantSeedling seedling)
        {
            _context.Attach(seedling);
            _context.Entry(seedling).Property("Count").IsModified = true;
            _context.Entry(seedling).Property("Description").IsModified = true;
            _context.Entry(seedling).Property("DateAdded").IsModified = true;
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
        public IQueryable<Country> GetCountries()
        {
            var countries = _context.Countries;
            return countries;
        }

        public IQueryable<Region> GetRegions(int countryId)
        {
           var regions = _context.Regions.Where(p => p.CountryId == countryId);
            return regions;
        }

        public PlantSeed GetUserSeed(int id)
        {
           var seed = _context.PlantSeeds.FirstOrDefault(p => p.Id == id);
            return seed;
        }

        public PlantSeedling GetUserSeedling(int id)
        {
            var seedling = _context.PlantSeedlings.FirstOrDefault(p => p.Id == id);
            return seedling;
        }

        public IQueryable<PlantSeed> GetUserPlantSeeds(string userId)
        {
            var seeds = _context.PlantSeeds.Where(p => p.UserId == userId);
            return seeds;
        }

        public IQueryable<PlantSeedling> GetUserPlantSeedlings(string userId)
        {
            var seedlings = _context.PlantSeedlings.Where(p => p.UserId == userId);
            return seedlings;
        }

        public IQueryable<NewUserPlant> GetNewUserPlants(string userId)
        {
            var plants = _context.NewUserPlants.Where(p => p.UserId == userId);
            return plants;
        }

        public IQueryable<NewUserPlant> GetAllNewUserPlants()
        {
            var plants = _context.NewUserPlants;
            return plants;
        }
    }
}
