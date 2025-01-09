
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
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<SelectListItem>> GetGroupsAsync(int? typeId)
        {
            var groupsQuery = _plantRepo.GetAllEntities<PlantGroup>().Where(e => e.PlantTypeId == typeId).ProjectTo<PlantGroupsVm>(_mapper.ConfigurationProvider);

            var groups = await groupsQuery.ToListAsync();

            return await GetSelectListItemAsync(groups);
        }

        public async Task<List<SelectListItem>> GetSectionsAsync(int? groupId)
        {
            var sectionsQuery = _plantRepo.GetAllEntities<PlantSection>().Where(e => e.PlantGroupId == groupId).ProjectTo<PlantSectionsVm>(_mapper.ConfigurationProvider);

            var sections = await sectionsQuery.ToListAsync();

            return await GetSelectListItemAsync(sections);
        }
        public async Task<List<SelectListItem>> GetDestinationsAsync()
        {

            var destinationsListQuery = _plantRepo.GetAllEntities<Destination>().OrderBy(p => p.Id).ProjectTo<DestinationsVm>(_mapper.ConfigurationProvider);

            var destinationList = await destinationsListQuery.ToListAsync();

            return  await GetSelectListItemAsync(destinationList);
        }
        public async Task<List<SelectListItem>> GetSelectListAsync<TSource, TViewModel>()
        where TViewModel : SelectListItemVm
        where TSource: class
        {
            var entities = await _plantRepo.GetAllEntities<TSource>()
                                     .ProjectTo<TViewModel>(_mapper.ConfigurationProvider)
                                     .ToListAsync();

            return await GetSelectListItemAsync(entities); 

        }
        public async Task<List<SelectListItem>> GetPlantPropertySelectListItemAsync<TSource, TVm, TSourceList, TVmList>(int typeId, int? groupId, int? sectionId)
           where TSource : class
           where TSourceList : BasePropertyForListFilters
           where TVm : SelectListItemVm
           where TVmList : PlantPropertyForListFiltersVm
        {
            var filteredList = await GetPlantPropertyAsync<TSourceList, TVmList>(typeId, groupId, sectionId);
            var propertyList = await FilterPropertyListAsync<TSource, TVm, TVmList>(filteredList);

            return await GetSelectListItemAsync(propertyList);
        }
        private async Task<List<TVm>> GetPlantPropertyAsync<TSource, TVm>(int typeId, int? groupId, int? sectionId)
           where TSource : BasePropertyForListFilters
           where TVm : PlantPropertyForListFiltersVm
        {
            return await _plantRepo.GetEntitiesForListFilters<TSource>(typeId, groupId, sectionId).ProjectTo<TVm>(_mapper.ConfigurationProvider).ToListAsync();
        }

        private async Task<List<TVm>> FilterPropertyListAsync<TSource, TVm, TVmList>(List<TVmList> entity)
           where TSource : class
           where TVm : SelectListItemVm
           where TVmList : PlantPropertyForListFiltersVm
        {
            var propertyList = await _plantRepo.GetAllEntities<TSource>().ProjectTo<TVm>(_mapper.ConfigurationProvider).ToListAsync();

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
        public async Task<List<SelectListItem>> GetSelectListItemAsync<T>(IEnumerable<T> entity) where T : SelectListItemVm
        {

            if (!entity.Any()) return new List<SelectListItem>();

            var result = entity
                           .OrderBy(p => p.Id)
                           .Where(e => e.Id != 0 && !string.IsNullOrEmpty(e.Name))
                           .Select(e => new SelectListItem
                           {
                               Value = e.Id.ToString(),
                               Text = e.Name
                           })
                           .Prepend(new SelectListItem { Text = "-Select-", Value = "0" , Disabled = true})
                           .ToList();

            return await Task.FromResult(result);
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
