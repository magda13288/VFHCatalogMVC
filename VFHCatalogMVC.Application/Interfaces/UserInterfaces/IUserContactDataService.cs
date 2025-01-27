﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        List<SelectListItem> Countries();
        List<SelectListItem> Regions(int countryId);
        List<SelectListItem> Cities(int regionId);
        AddressVm GetAddress(string userId);
        void AddAddress(AddressVm address);
        string UserAccountName(Task<ApplicationUser> user);
        //List<SelectListItem> GetSelectListItem<T>(IEnumerable<T> entity) where T : SelectListItemVm;
    }
}
