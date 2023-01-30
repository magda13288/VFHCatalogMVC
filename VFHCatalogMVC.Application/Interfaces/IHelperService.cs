using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IHelperService
    {
        List<SelectListItem> PlantTypes();
        List<SelectListItem> PlantColors();
        List<SelectListItem> PlantGrowingSeaznos();
        List<SelectListItem> Countries();
        List<SelectListItem> Regions(int countryId);
        List<SelectListItem> Cities(int regionId);
        string UserAccountName(Task<ApplicationUser> user);
        MessageDisplay MessagesToView(int type);
        IndexPlantType GetIndexPlantType(bool seeds, bool seedlings, bool newPlant);
    }
}
