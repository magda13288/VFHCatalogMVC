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
	public class PlantOpinionRepository : IPlantOpinionRepository
	{
	   private readonly Context _context;

		public PlantOpinionRepository(Context context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}
		public int Add(PlantOpinion entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(PlantOpinion entity)
		{
			throw new NotImplementedException();
		}

		public void DeleteById(int id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantOpinion> GetAll()
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantOpinion> GetAll(int pageNumber, int rowCount)
		{
			throw new NotImplementedException();
		}

		public IQueryable<PlantOpinion> GetAll(int id)
		{
			return _context.PlantOpinions.AsNoTracking().Where(p => p.PlantDetailId == id);
		}

		public PlantOpinion GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(PlantOpinion entity)
		{
			throw new NotImplementedException();
		}
	}
}
