using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
	internal class PlantGrowingSeazonRepository : IPlantGrowingSeazonRepository
	{
		private readonly Context _context;

		public PlantGrowingSeazonRepository(Context context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}
		public int Add(PlantGrowingSeazon entity)
		{
			throw new System.NotImplementedException();
		}

		public void AddEntities<T>(int[] entityIds, int plantDetailId, Func<int, int, T> createEntity) where T : class
		{
			var entities = entityIds.Select(id => createEntity(id, plantDetailId)).ToList();
			_context.Set<T>().AddRange(entities);
			_context.SaveChanges();
		}

		public void Add(int[] growingSeazonsIds, int plantDetailId)
		{
			AddEntities<PlantGrowingSeazon>(
				growingSeazonsIds,
				plantDetailId,
				(id, detailId) => new PlantGrowingSeazon { GrowingSeazonId = id, PlantDetailId = detailId });
		}

		public void Delete(PlantGrowingSeazon entity)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteById(int id)
		{
			throw new System.NotImplementedException();
		}

		public IQueryable<PlantGrowingSeazon> GetAll()
		{
			throw new System.NotImplementedException();
		}

		public IQueryable<PlantGrowingSeazon> GetAll(int pageNumber, int rowCount)
		{
			throw new System.NotImplementedException();
		}

		public PlantGrowingSeazon GetById(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Update(PlantGrowingSeazon entity)
		{
			throw new System.NotImplementedException();
		}
	}
}