using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;

namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantDetailsService
    {
        Task<int> AddPlantDetailsAsync(NewPlantVm model);
        Task<PlantDetailsVm> GetPlantDetailsAsync(int id);
        Task<int> UpdateEntityAsync<TEntity>(int plantDetailId, IEnumerable<int> entityIds, Func<int, IEnumerable<TEntity>> getPropertiesAction, Func<int[], int,Task<int>> addAction, Func<int,Task<int>> deleteAction);
        Task AddPlantOpinionAsync(PlantOpinionsVm opinion);
        Task<PlantOpinionsVm> FillPropertyOpinionAsync(int id, string userName);




    }
}
