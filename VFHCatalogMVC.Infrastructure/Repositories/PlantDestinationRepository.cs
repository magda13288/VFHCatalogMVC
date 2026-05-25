using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
	public class PlantDestinationRepository : IPlantDestinationRepository
	{
	    private readonly Context _context;

		public PlantDestinationRepository(Context context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}
		public int Add(PlantDestination entity)
		{
			throw new NotImplementedException();
		}

		public void AddEntities<T>(int[] entityIds, int plantDetailId, Func<int, int, T> createEntity) where T : class
		{
			var entities = entityIds.Select(id => createEntity(id, plantDetailId)).ToList();
			_context.Set<T>().AddRange(entities);
			_context.SaveChanges();
		}

		public void Add(int[] plantDestinationsIds, int plantDetailId)
		{
			AddEntities<PlantDestination>(
			   plantDestinationsIds,
			   plantDetailId,
			   (id, detailId) => new PlantDestination { DestinationId = id, PlantDetailId = detailId });
		}

		public void Delete(PlantDestination entity)
		{
			throw new NotImplementedException();
		}

		public void DeleteById(int id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantDestination> GetAll()
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantDestination> GetAll(int pageNumber, int rowCount)
		{
			throw new NotImplementedException();
		}

		public PlantDestination GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(PlantDestination entity)
		{
			throw new NotImplementedException();
		}
	}
}
