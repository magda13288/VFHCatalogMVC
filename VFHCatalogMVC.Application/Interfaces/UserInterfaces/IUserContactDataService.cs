using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.User.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Interfaces.UserInterfaces
{
    public interface IUserContactDataService
    {
        Task<List<SelectListItem>> Countries();
        Task<List<SelectListItem>> Regions(int countryId);
        Task<List<SelectListItem>> Cities(int regionId);  
        Task<AddressVm> GetAddressAsync(string userId);
        Task AddAddressAsync(AddressVm address);
        string UserAccountName(ApplicationUser user);
        Task<List<SelectListItem>> GetSelectListItemAsync<T>(IEnumerable<T> entity) where T : SelectListItemVm;
    }
}
