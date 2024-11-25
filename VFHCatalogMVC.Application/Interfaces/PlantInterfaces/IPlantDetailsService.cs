using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;

namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantDetailsService
    {
        int AddPlantDetails(NewPlantVm model);
        PlantDetailsVm GetPlantDetails(int id);
        void UpdateEntity<TEntity>(int plantDetailId, IEnumerable<int> entityIds, Func<int, IEnumerable<TEntity>> getPropertiesAction, Action<int[], int> addAction, Action<int> deleteAction);
        void AddPlantOpinion(PlantOpinionsVm opinion);
        public PlantOpinionsVm FillPropertyOpinion(int id, string userName);




    }
}
