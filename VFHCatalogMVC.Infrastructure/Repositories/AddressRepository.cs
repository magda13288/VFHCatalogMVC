using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
    public class AddressRepository:IAddressesRepository
    {
        private Context _context;
        public AddressRepository(Context context)
        {
            _context = context;
        }

        public Address GetAddress(int id)
        {
            var address = _context.Addresses.FirstOrDefault(p => p.Id == id);
            return address;
        }

        public IQueryable<City> GetCities(int voivodeshipId)
        {
            var cities = _context.Cities.Where(p => p.VoivodeshipId == voivodeshipId);
            return cities;
        }

        public IQueryable<Country> GetCountries()
        {
            var countries = _context.Countries;
            return countries;
        }

        public IQueryable<Voivodeship> GetVoivodeships(int countryId)
        {
           var voivodeships = _context.Voivodeships.Where(p => p.CountryId == countryId);
            return voivodeships;
        }
    }
}
