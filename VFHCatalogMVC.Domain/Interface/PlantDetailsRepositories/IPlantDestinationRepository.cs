using System;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories
{
    public interface IPlantDestinationRepository : IRepository<PlantDestination>
    {
		void AddEntities<T>(int[] entityIds, int plantDetailId, Func<int, int, T> createEntity) where T : class;
		void Add(int[] plantDestinationsIds, int plantDetailId);

	}
}