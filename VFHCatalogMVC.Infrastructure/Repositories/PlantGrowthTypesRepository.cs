using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
	public class PlantGrowthTypesRepository : IPlantGrowthTypeRepository
	{
	    private readonly Context _context;

		public PlantGrowthTypesRepository(Context context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public int Add(PlantGrowthType entity)
		{
			throw new NotImplementedException();
		}

		public void AddEntities<T>(int[] entityIds, int plantDetailId, Func<int, int, T> createEntity) where T : class
		{
			var entities = entityIds.Select(id => createEntity(id, plantDetailId)).ToList();
			_context.Set<T>().AddRange(entities);
			_context.SaveChanges();
		}

		public void Add(int[] growthTypesIds, int plantDetailId)
		{
			AddEntities<PlantGrowthType>(
			   growthTypesIds,
			   plantDetailId,
			   (id, detailId) => new PlantGrowthType { GrowthTypeId = id, PlantDetailId = detailId });
		}

		public void Delete(PlantGrowthType entity)
		{
			throw new NotImplementedException();
		}

		public void DeleteById(int id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantGrowthType> GetAll()
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantGrowthType> GetAll(int pageNumber, int rowCount)
		{
			throw new NotImplementedException();
		}

		public PlantGrowthType GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(PlantGrowthType entity)
		{
			throw new NotImplementedException();
		}
	}
}
