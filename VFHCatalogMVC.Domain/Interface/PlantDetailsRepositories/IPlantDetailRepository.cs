// VFHCatalogMVC.Domain.Interface\IPlantDetailRepository.cs
using System.Linq;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories
{
    public interface IPlantDetailRepository : IRepository<PlantDetail>
    {
		int Add(PlantDetail plantDetail, int plantId);
		string GetPropertyName<T>(int? id) where T : BasePlantEntityNameProperty;
		IQueryable<T> GetById<T>(int id) where T : BasePlantDetailKeyProperty;

	}
}