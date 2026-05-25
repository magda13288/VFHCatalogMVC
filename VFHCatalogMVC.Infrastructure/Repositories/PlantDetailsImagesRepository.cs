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
	public class PlantDetailsImagesRepository : IPlantDetailsImagesRepository
	{
		private readonly Context _context;

		public PlantDetailsImagesRepository(Context context	)
		{	
		   ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public int Add(PlantDetailsImages entity)
		{
			throw new NotImplementedException();
		}

		public void Add(string fileName, int plantDetailId)
		{
			_context.PlantDetailsImages.Add(new PlantDetailsImages { PlantDetailId = plantDetailId, ImageURL = fileName });
			_context.SaveChanges();
		}

		public void Delete(PlantDetailsImages entity)
		{
			throw new NotImplementedException();
		}

		public void DeleteById(int id)
		{
			var imageToDelete = _context.PlantDetailsImages.FirstOrDefault(p => p.Id == id);
			_context.PlantDetailsImages.Remove(imageToDelete);
			_context.SaveChanges();
		}

		public IQueryable<PlantDetailsImages> GetAll()
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantDetailsImages> GetAll(int pageNumber, int rowCount)
		{
			throw new NotImplementedException();
		}

		public PlantDetailsImages GetById(int id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantDetailsImages> GetPlantDetailsImagesById(int plantDetailId)
		{
			var entities = _context.PlantDetailsImages.AsNoTracking().Where(p => p.PlantDetailId == plantDetailId);
			return entities;
		}

		public void Update(PlantDetailsImages entity)
		{
			throw new NotImplementedException();
		}
	}
}
