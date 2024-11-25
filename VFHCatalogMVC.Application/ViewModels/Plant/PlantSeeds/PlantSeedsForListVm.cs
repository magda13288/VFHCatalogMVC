using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Adresses;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds
{
    public class PlantSeedsForListVm : PlantListVm<PlantSeedVm>
    {
        public List<PlantSeedVm> PlantSeeds { get; set; }

    }
}
