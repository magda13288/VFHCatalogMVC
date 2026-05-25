using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface.PlantRepositories
{
    public interface IPlantRepository: IRepository <Plant>
    {
        Plant GetByIdFull(int id);

		void ActivatePlant(Plant plant);

        IQueryable<Plant> GetEntitiesForFilterList(int typeId, int? groupId, int? sectionId);

		IQueryable<T> GetAllEntities<T>() where T : class;



		int AddEntity<T>(T entity) where T : BaseEntity;
        void DeletePlantDetailEntity<T>(int id) where T : BasePlantDetailKeyProperty;
        int AddContactDetailsEntity<T>(T entity) where T : class;
        int AddContactDetail(ContactDetail contact);
        void UpdatePlant(Plant plant);
        IQueryable<T> GetPlantSeedOrSeedling<T>(int id) where T : BasePlantSeedSeedlingProperty;
        IQueryable<T> GetEntitiesForListFilters<T>(int typeId, int? groupId, int? sectionId) where T : BasePropertyForListFilters;



    }
}
