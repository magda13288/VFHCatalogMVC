
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
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using VFHCatalogMVC.Domain.Common;
using System.Web.Razor.Generator;
using VFHCatalogMVC.Application.ViewModels.Common;
using VFHCatalogMVC.Application.Interfaces;

namespace VFHCatalogMVC.Application.Services.PlantServices
{
    public class PlantHelperService : IPlantHelperService
    {
      
        private readonly IPlantRepository _plantRepo;
        private readonly IMapper _mapper;
        private readonly IListService _listService;
        public PlantHelperService( IMapper mapper, IPlantRepository plantRepository, IListService listService)
        {            
            _mapper = mapper;
            _plantRepo = plantRepository;
            _listService = listService;
        }

        public List<SelectListItem> GetGroups(int? typeId)
        {
            var groups = _plantRepo.GetAllEntities<PlantGroup>().Where(e => e.PlantTypeId == typeId).ProjectTo<PlantGroupsVm>(_mapper.ConfigurationProvider).ToList();

            return _listService.GetSelectListItem(groups);
        }

        public List<SelectListItem> GetSections(int? groupId)
        {
            var sections = _plantRepo.GetAllEntities<PlantSection>().Where(e => e.PlantGroupId == groupId).ProjectTo<PlantSectionsVm>(_mapper.ConfigurationProvider).ToList();

            return _listService.GetSelectListItem(sections);
        }
        public List<SelectListItem> GetDestinations()
        {

            var destinationsList = _plantRepo.GetAllEntities<Destination>().OrderBy(p => p.Id).ProjectTo<DestinationsVm>(_mapper.ConfigurationProvider).ToList();

            return _listService.GetSelectListItem(destinationsList);
        }
        public List<SelectListItem> GetSelectList<TSource, TViewModel>()
        where TViewModel : SelectListItemVm
        where TSource: class
        {
            var entities = _plantRepo.GetAllEntities<TSource>()
                                     .ProjectTo<TViewModel>(_mapper.ConfigurationProvider)
                                     .ToList();

            return  _listService.GetSelectListItem(entities); 

        }
        public List<SelectListItem> GetPlantPropertySelectListItem<TSource, TVm, TSourceList, TVmList>(int typeId, int? groupId, int? sectionId)
           where TSource : class
           where TSourceList : BasePropertyForListFilters
           where TVm : SelectListItemVm
           where TVmList : PlantPropertyForListFiltersVm
        {
            var filteredList = GetPlantProperty<TSourceList, TVmList>(typeId, groupId, sectionId);
            var propertyList = FilterPropertyList<TSource, TVm, TVmList>(filteredList);

            return _listService.GetSelectListItem(propertyList);
        }
        private List<TVm> GetPlantProperty<TSource, TVm>(int typeId, int? groupId, int? sectionId)
           where TSource : BasePropertyForListFilters
           where TVm : PlantPropertyForListFiltersVm
        {
            return _plantRepo.GetEntitiesForListFilters<TSource>(typeId, groupId, sectionId).ProjectTo<TVm>(_mapper.ConfigurationProvider).ToList();
        }

        private List<TVm> FilterPropertyList<TSource, TVm, TVmList>(List<TVmList> entity)
           where TSource : class
           where TVm : SelectListItemVm
           where TVmList : PlantPropertyForListFiltersVm
        {
            var propertyList = _plantRepo.GetAllEntities<TSource>().ProjectTo<TVm>(_mapper.ConfigurationProvider).ToList();

            if (!entity.Any())
            {
                return propertyList.Where(p => p.Id == 1).ToList();
            }
            else
            {
                var entityIds = entity.Select(p => p.Id).ToList();
                return propertyList.Where(p => entityIds.Contains(p.Id)).ToList();
            }
        }     
        public IndexPlantType GetIndexPlantType(bool seeds, bool seedlings, bool newPlant)
        {
            var index = new IndexPlantType() 
            { seeds = seeds, 
              seedlings = seedlings,
              newPlant = newPlant
            };

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
