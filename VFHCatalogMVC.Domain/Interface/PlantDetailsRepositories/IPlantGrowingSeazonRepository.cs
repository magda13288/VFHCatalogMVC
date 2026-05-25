using System;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories
{
    public interface IPlantGrowingSeazonRepository : IRepository<PlantGrowingSeazon>
    {
		void AddEntities<T>(int[] entityIds, int plantDetailId, Func<int, int, T> createEntity) where T : class;
		void Add(int[] growingSeazonsIds, int plantDetailId);
	}
}