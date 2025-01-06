using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;

namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantItemProcessor<TVm> where TVm : PlantItemVm
    {
        Task<List<TVm>> ProcessItemsAsync(List<TVm> items, int detailId, int countryId, int regionId, int cityId, bool isCompany);
    }
}
