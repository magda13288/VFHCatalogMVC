using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
	public class PlantDetailsRepository : IPlantDetailRepository
	{
		private readonly Context _context;

		public PlantDetailsRepository(Context context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public int Add(PlantDetail entity)
		{
			throw new NotImplementedException();
		}

		public int Add(PlantDetail plantDetail, int plantId)
		{
			plantDetail.PlantRef = plantId;
			_context.PlantDetails.Add(plantDetail);
			_context.SaveChanges();

			return plantDetail.Id;
		}

		public void Delete(PlantDetail entity)
		{
			throw new NotImplementedException();
		}

		public void DeleteById(int id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantDetail> GetAll()
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantDetail> GetAll(int pageNumber, int rowCount)
		{
			throw new NotImplementedException();
		}

		public PlantDetail GetById(int id)
		{
			var entity = _context.PlantDetails.AsNoTracking().FirstOrDefault(p => p.PlantRef == id);
			return entity;
		}

		public IQueryable<T> GetById<T>(int id) where T : BasePlantDetailKeyProperty
		{
			return _context.Set<T>().AsNoTracking().Where(p => p.PlantDetailId == id);
		}

		public string GetPropertyName<T>(int? id) where T : BasePlantEntityNameProperty
		{
			var entity = _context.Set<T>().AsNoTracking().FirstOrDefault(p => p.Id == id);
			if (entity == null) return null;

			return entity.Name;
		}

		public void Update(PlantDetail entity)
		{
			_context.Attach(entity);
			_context.Entry(entity).Property(e => e.ColorId).IsModified = true;
			_context.Entry(entity).Property(e => e.FruitSizeId).IsModified = true;
			_context.Entry(entity).Property(e => e.FruitSizeId).IsModified = true;
			_context.Entry(entity).Property(e => e.Description).IsModified = true;
			_context.Entry(entity).Property(e => e.PlantPassportNumber).IsModified = true;
			_context.Entry(entity).Property(e => e.PlantRef).IsModified = false;
			_context.SaveChanges();
		}
	}
}
