using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface IHelperUserRepository
    {
        IQueryable<Country> GetCountries();
        IQueryable<Region> GetRegions(int countryId);
        IQueryable<City> GetCities(int regionId);
        IQueryable<Filters> GetAllFilters(int typeId, int? groupId, int? sectionId);
    }
}
