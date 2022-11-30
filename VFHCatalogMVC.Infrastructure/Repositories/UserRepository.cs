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


    }
}
