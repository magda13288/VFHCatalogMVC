
using AutoMapper;
using AutoMapper.QueryableExtensions;
using NPOI.OpenXmlFormats.Dml;

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.ViewModels.Message;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services.PlantServices
{
    public class PlantHelperService : IPlantHelperService
    {
      
        private readonly IPlantRepository _plantRepo;
        private readonly IMapper _mapper;
        public PlantHelperService( IMapper mapper, IPlantRepository plantRepository)
        {            
            _mapper = mapper;
            _plantRepo = plantRepository;
        }

        public List<SelectListItem> GetGroups(int? typeId)
        {
            var groups = _plantRepo.GetAllEntities<PlantGroup>().Where(e => e.PlantTypeId == typeId).ProjectTo<PlantGroupsVm>(_mapper.ConfigurationProvider).ToList();

            return GetSelectListItem(groups);
        }

        public List<SelectListItem> GetTypes()
        {
            var types = _plantRepo.GetAllEntities<PlantType>().OrderBy(p => p.Id).ProjectTo<PlantTypesVm>(_mapper.ConfigurationProvider).ToList();

            return GetSelectListItem(types);
        }

        public List<SelectListItem> GetSections(int? groupId)
        {
            var sections = _plantRepo.GetAllEntities<PlantSection>().Where(e => e.PlantGroupId == groupId).ProjectTo<PlantSectionsVm>(_mapper.ConfigurationProvider).ToList();

            return GetSelectListItem(sections);
        }
        public List<SelectListItem> GetColors()
        {
            var colorsList = _plantRepo.GetAllEntities<Color>().OrderBy(p => p.Id).ProjectTo<ColorsVm>(_mapper.ConfigurationProvider).ToList();

            return GetSelectListItem(colorsList);
        }

        public List<SelectListItem> GetGrowingSeazons()
        {        

           var growingSeazonList = _plantRepo.GetAllEntities<GrowingSeazon>().OrderBy(p => p.Id).ProjectTo<GrowingSeazonVm>(_mapper.ConfigurationProvider).ToList();

            return GetSelectListItem(growingSeazonList);
        }

        public List<SelectListItem> GetGrowthTypes(int typeId, int? groupId, int? sectionId)
        {
            var growthTypes = FilterGrowthTypes(typeId, groupId, sectionId);

            return GetSelectListItem(growthTypes);
        }

        public List<SelectListItem> GetDestinations()
        {
            var destinations = _plantRepo.GetAllEntities<Destination>().OrderBy(p => p.Id).ProjectTo<DestinationsVm>(_mapper.ConfigurationProvider).ToList();

            return GetSelectListItem(destinations);

        }

        public List<SelectListItem> GetFruitSize(int typeId, int groupId, int? sectionId)
        {
            var fruitSiezes = FilterFruitSize(typeId, groupId, sectionId);

            return GetSelectListItem(fruitSiezes);
        }

        public List<SelectListItem> GetFruitTypes(int typeId, int groupId, int? sectionId)
        {
            var fruitTypes = FilterFruitType(typeId, groupId, sectionId);

            return GetSelectListItem(fruitTypes);
        }
        public List<SelectListItem> GetSelectList<TSource, TViewModel>()
        where TViewModel : SelectListItemVm
        where TSource: class
        {
            var entities = _plantRepo.GetAllEntities<TSource>()
                                     //.OrderBy(e => e.GetType().GetProperty("Id"))
                                     .ProjectTo<TViewModel>(_mapper.ConfigurationProvider)
                                     .ToList();

            if (!entities.Any()) return new List<SelectListItem>();

            return entities 
                           .OrderBy(p => p.Id)
                           .Where(e => e.Id != 0 && !string.IsNullOrEmpty(e.Name))
                           .Select(e => new SelectListItem
                           {
                               Value = e.Id.ToString(),
                               Text = e.Name
                           })
                           .Prepend(new SelectListItem { Text = "-Wybierz-", Value = "0" })
                           .ToList();
        }
        private List<SelectListItem> GetSelectListItem<T>(IEnumerable<T> entity) where T : SelectListItemVm
        {
           
            if (!entity.Any()) return null;

            return entity.Where(g => g.Id != 0 && !string.IsNullOrEmpty(g.Name)).Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList()
                .Prepend(new SelectListItem { Text = "-Wybierz-", Value = "0" })
                .ToList();
        }
        public List<PlantGroupsVm> GetGroupsJR(int typeId)
        {
            var groups = _plantRepo.GetAllEntities<PlantGroup>().Where(e => e.PlantTypeId == typeId).ProjectTo<PlantGroupsVm>(_mapper.ConfigurationProvider).ToList();

            return groups;

        }
        public List<PlantSectionsVm> GetSectionsJR(int groupId)
        {
            var sections = _plantRepo.GetAllEntities<PlantSection>().Where(e => e.PlantGroupId == groupId).ProjectTo<PlantSectionsVm>(_mapper.ConfigurationProvider).ToList();

            return sections;

        }
        public List<GrowthTypeVm> GetGrowthTypesJR(int typeId, int? groupId, int? sectionId)
        {
            var growthTyes = FilterGrowthTypes(typeId, groupId, sectionId);

            return growthTyes;
        }

        private List<GrowthTypeVm> FilterGrowthTypes(int typeId, int? groupId, int? sectionId)
        {
            List<GrowthTypeVm> growthTyes = new List<GrowthTypeVm>();

            if (typeId == 1)
            {
                growthTyes = _plantRepo.GetAllEntities<GrowthType>().Where(e => e.PlantTypeId == typeId && e.PlantGroupId == groupId && e.PlantSectionId == sectionId).OrderBy(e => e.PlantTypeId).ProjectTo<GrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
            }
            else if (typeId == 2 || typeId == 3)
            {
                growthTyes = _plantRepo.GetAllEntities<GrowthType>().Where(e => e.PlantTypeId == typeId).OrderBy(e => e.PlantTypeId).ProjectTo<GrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
            }

            return growthTyes;
        }
        public List<DestinationsVm> GetDestinationsJR()
        {

            var destinationsList = _plantRepo.GetAllEntities<Destination>().OrderBy(p => p.Id).ProjectTo<DestinationsVm>(_mapper.ConfigurationProvider).ToList();

            return destinationsList;
        }

        private List<FruitSizeVm> FilterFruitSize(int typeId, int groupId, int? sectionId)
        {
            List<FruitSizeVm> fruitSizeList = new List<FruitSizeVm>();
            if (!sectionId.HasValue)
            {
                fruitSizeList = _plantRepo.GetAllEntities<FruitSize>().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId).OrderBy(p => p.Id).ProjectTo<FruitSizeVm>(_mapper.ConfigurationProvider).ToList();

            }
            if (sectionId.HasValue)
            {
                fruitSizeList = _plantRepo.GetAllEntities<FruitSize>().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId).OrderBy(p => p.Id).ProjectTo<FruitSizeVm>(_mapper.ConfigurationProvider).ToList();
            }

            return fruitSizeList;
        }
        public List<FruitSizeVm> GetFruitSizeJR(int typeId, int groupId, int? sectionId)
        {
            
            var fruitSizeList = FilterFruitSize(typeId, groupId, sectionId);

            return fruitSizeList;
        }

        private List<FruitTypeVm> FilterFruitType(int typeId, int groupId, int? sectionId)
        {
            List<FruitTypeVm> fruitTypeList = new List<FruitTypeVm>();

            if (!sectionId.HasValue)
            {
                fruitTypeList = _plantRepo.GetAllEntities<FruitType>().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId).OrderBy(p => p.Id).ProjectTo<FruitTypeVm>(_mapper.ConfigurationProvider).ToList();

            }
            if (sectionId.HasValue)
            {
                fruitTypeList = _plantRepo.GetAllEntities<FruitType>().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId).OrderBy(p => p.Id).ProjectTo<FruitTypeVm>(_mapper.ConfigurationProvider).ToList();
            }
            return fruitTypeList;
        }
        public List<FruitTypeVm> GetFruitTypeJR(int typeId, int groupId, int? sectionId)
        {
            var fruitTypeList = FilterFruitType(typeId, groupId, sectionId);

            return fruitTypeList;
        }
        public IndexPlantType GetIndexPlantType(bool seeds, bool seedlings, bool newPlant)
        {
            var index = new IndexPlantType() { seeds = seeds, seedlings = seedlings, newPlant = newPlant };

            return index;
        }
        public MessageDisplay MessagesToView(int type)
        {
            var messageDisplay = new MessageDisplay();

            if (type == 0)
            {
                messageDisplay.Received = false;
                messageDisplay.Sent = false;
                messageDisplay.ViewAll = true;
            }
            else
            {
                if (type == 1)
                {
                    messageDisplay.Received = true;
                    messageDisplay.Sent = false;
                    messageDisplay.ViewAll = false;
                }
                else
                {
                    messageDisplay.Received = false;
                    messageDisplay.Sent = true;
                    messageDisplay.ViewAll = false;
                }

            }

            return messageDisplay;
        }
        public string UserAccountName(Task<ApplicationUser> user)
        {
            string userAccountName = null;

            if (user.Result.AccountName != null)
                userAccountName = user.Result.AccountName;
            if (user.Result.CompanyName != null)
                userAccountName = user.Result.CompanyName;
            return userAccountName;
        }

    }
}
