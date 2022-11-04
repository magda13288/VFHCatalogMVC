using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Model;
namespace VFHCatalogMVC.Domain.Interface
{
    public interface IAddressesRepository
    {
        Address GetAddress(int id);
        IQueryable<Country> GetCountries();
        IQueryable<Voivodeship> GetVoivodeships(int countryId);
        IQueryable<City> GetCities(int voivodeshipId);
    }
}
