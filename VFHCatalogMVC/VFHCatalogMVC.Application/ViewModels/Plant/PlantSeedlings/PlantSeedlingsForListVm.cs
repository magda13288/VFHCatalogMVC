using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Adresses;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings
{
    public class PlantSeedlingsForListVm : PlantListVm<PlantSeedlingVm>
    {
        public List<PlantSeedlingVm> PlantSeedlings { get; set; }

    }
}
