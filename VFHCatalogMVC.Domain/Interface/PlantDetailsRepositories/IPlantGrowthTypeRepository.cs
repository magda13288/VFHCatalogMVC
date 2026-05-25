using System;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories
{
    public interface IPlantGrowthTypeRepository : IRepository<PlantGrowthType>
    {
		void Add(int[] growthTypesIds, int plantDetailId);
		void AddEntities<T>(int[] entityIds, int plantDetailId, Func<int, int, T> createEntity) where T : class;

	}
}