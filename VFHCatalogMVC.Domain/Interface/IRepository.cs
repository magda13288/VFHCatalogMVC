using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Interface
{
	public interface IRepository<TEntity>
	where TEntity : BaseEntity
	{
		IQueryable<TEntity> GetAll();

		IQueryable<TEntity> GetAll(int pageNumber, int rowCount);

		TEntity GetById(int id);

		int Add(TEntity entity);

		void Delete(TEntity entity);

		void DeleteById(int id);

		void Update(TEntity entity);
	}
}
