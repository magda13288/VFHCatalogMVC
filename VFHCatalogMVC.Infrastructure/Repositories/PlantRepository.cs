using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface.PlantRepositories;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly Context _context;
        public PlantRepository(Context context)
        {
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
        }

        public void Delete(Plant plant)
        {

            _context.Attach(plant);
            _context.Entry(plant).Property(e => e.isActive).IsModified = true;
            _context.SaveChanges();
        }

        public int AddEntity<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }
        public IQueryable<T> GetEntitiesForListFilters<T>(int typeId, int? groupId, int? sectionId) where T : BasePropertyForListFilters
        {
            var entities = _context.Set<T>().AsNoTracking().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId);

            return entities;

        }
        public IQueryable<T> GetAllEntities<T>() where T : class
        {
            return _context.Set<T>();
        }
        public void UpdatePlant(Plant plant)
        {
            _context.Attach(plant);
            _context.Entry(plant).Property(e => e.FullName).IsModified = true;
            _context.Entry(plant).Property(e => e.Photo).IsModified = true;
            _context.SaveChanges();
        }
        public void DeletePlantDetailEntity<T>(int id) where T : BasePlantDetailKeyProperty
        {
            var entity = _context.Set<T>().Where(p => p.PlantDetailId == id);
            _context.Set<T>().RemoveRange(entity);
            _context.SaveChanges();
        }
        public IQueryable<T> GetPlantSeedOrSeedling<T>(int id) where T : BasePlantSeedSeedlingProperty
        {
            return _context.Set<T>().AsNoTracking().Where(p => p.PlantId == id);
        }
        public int AddContactDetailsEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }
        public int AddContactDetail(ContactDetail contact)
        {
            _context.ContactDetails.Add(contact);
            _context.SaveChanges();
            return contact.Id;
        }

        public void ActivatePlant(Plant plant)
        {
            _context.Attach(plant);
            _context.Entry(plant).Property(e => e.isActive).IsModified = true;
            _context.Entry(plant).Property(e => e.isNew).IsModified = true;
            _context.SaveChanges();
        }

		public IQueryable<Plant> GetAll()
		{
			return _context.Plants.AsNoTracking().Where(p => p.isActive).OrderBy(p => p.Id);
		}

		public IQueryable<Plant> GetAll(int pageNumber, int rowCount)
		{
			return _context.Plants.AsNoTracking()
                .Where(p => p.isActive)
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount);
		}

		public Plant GetById(int id)
		{
			return _context.Plants.AsNoTracking().FirstOrDefault(p => p.Id == id);
		}

		public Plant GetByIdFull(int id)
		{
			return _context.Plants.AsNoTracking().Include(p => p.PlantDetail)
								   .ThenInclude(pd => pd.PlantGrowingSeazons)
							   .Include(p => p.PlantDetail)
								   .ThenInclude(pd => pd.PlantDestinations)
							   .Include(p => p.PlantDetail)
								   .ThenInclude(pd => pd.PlantGrowthTypes)
							   .Include(p => p.PlantDetail)
								   .ThenInclude(pd => pd.PlantDetailsImages)
							   .Include(p => p.PlantDetail)
								   .ThenInclude(pd => pd.PlantOpinions)
							   .Include(p => p.PlantDetail)
								   .ThenInclude(pd => pd.Color)
							   .Include(p => p.PlantDetail)
								   .ThenInclude(pd => pd.FruitSize)
							   .Include(p => p.PlantDetail)
								   .ThenInclude(pd => pd.FruitType)
							   .FirstOrDefault(p => p.Id == id);
		}

		public int Add(Plant entity)
		{
			_context.Set<Plant>().Add(entity);
			_context.SaveChanges();

            return entity.Id;
        }
		public void DeleteById(int id)
		{
			var entity = _context.Set<Plant>().Find(id);
			if (entity != null)
			{
				_context.Remove(entity);
				_context.SaveChanges();
			}
		}

		public void Update(Plant entity)
		{
			_context.Attach(entity);
			_context.Entry(entity).Property(e => e.FullName).IsModified = true;
			_context.Entry(entity).Property(e => e.Photo).IsModified = true;
            _context.Entry(entity).Property(e => e.PlantDetail).IsModified = true;
			_context.Entry(entity).Property(e => e.PlantDetail).IsModified = true;
			_context.SaveChanges();
		}

        public IQueryable<Plant> GetEntitiesForFilterList(int typeId, int? groupId, int? sectionId)
        {
			return _context.Set<Plant>().AsNoTracking().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId);

		}
	}
}
