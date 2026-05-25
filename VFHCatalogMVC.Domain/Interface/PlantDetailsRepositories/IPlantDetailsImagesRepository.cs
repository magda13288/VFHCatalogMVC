using System.Linq;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories
{
    public interface IPlantDetailsImagesRepository : IRepository<PlantDetailsImages>
    {
		void Add(string fileName, int plantDetailId);
		IQueryable<PlantDetailsImages> GetPlantDetailsImagesById(int plantDetailId);

	}
}