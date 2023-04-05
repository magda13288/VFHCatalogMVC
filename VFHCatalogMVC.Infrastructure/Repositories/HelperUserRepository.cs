using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
    public class HelperUserRepository:IHelperUserRepository
    {
        private Context _context;
        public HelperUserRepository(Context context)
        {
            _context = context;
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
        public IQueryable<Filters> GetAllFilters(int typeId, int? groupId, int? sectionId)
        {
            var filters = _context.Filters.Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId);

            return filters;
        }
    }
}
