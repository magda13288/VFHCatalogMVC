using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IHelperPlantService
    {
        List<SelectListItem> PlantTypes();
        List<SelectListItem> PlantColors();
        List<SelectListItem> PlantGrowingSeaznos();
        List<SelectListItem> PlantDestinations();
        List<SelectListItem> PlantPositions();
        List<SelectListItem> PlantAdditionalFeatures();        
        IndexPlantType GetIndexPlantType(bool seeds, bool seedlings, bool newPlant);
       
    }
}
