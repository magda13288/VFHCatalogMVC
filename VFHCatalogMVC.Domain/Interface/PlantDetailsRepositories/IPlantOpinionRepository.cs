using System.Linq;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories
{
    public interface IPlantOpinionRepository : IRepository<PlantOpinion>
    {
		IQueryable<PlantOpinion> GetAll(int id);

	}
}