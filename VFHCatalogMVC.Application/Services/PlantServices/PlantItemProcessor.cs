using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Application.ViewModels.User;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services.PlantServices
{
    public class PlantItemProcessor<TVm> : IPlantItemProcessor<TVm> where TVm : PlantItemVm, new()
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserPlantService _userPlantService;
        private readonly IPlantRepository _plantRepo;
        private readonly IUserContactDataService _userContactDataService;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public PlantItemProcessor(
            UserManager<ApplicationUser> userManager,
            IUserPlantService userPlantService,
            IPlantRepository plantRepo,
            IUserContactDataService userContactDataService,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _userPlantService = userPlantService;
            _plantRepo = plantRepo;
            _userContactDataService = userContactDataService;
            _userRepo = userRepository;
            _mapper = mapper;
        }

        public async Task<List<TVm>> ProcessItemsAsync(List<TVm> items, int detailId, int countryId, int regionId, int cityId, bool isCompany)
        {
            var result = new List<TVm>();

            if (countryId == 0 && regionId == 0 && cityId == 0)
            {
                result = await FilterAsync(items, isCompany, detailId);
            }
            else
            {
                var filteredUserList =  await _userPlantService.FilterUsersAsync(countryId, regionId, cityId, items.Cast<PlantItemVm>().ToList());

                var list = await FilterPlantItems(items, filteredUserList);

                result = await FilterAsync(list,isCompany,detailId);
            }
                

            return result;
        }
        private async Task<List<TVm>> FilterPlantItems<TVm>(List<TVm> items, List<string> filteredUsersList)
           where TVm : PlantItemVm
        {
            //FilteredUsersList Filter elements which UserId in on the list filteredUsersList
            var filteredItems = items
                .Where(item => filteredUsersList.Contains(item.UserId))
                .Distinct() // Removes duplicates based on reference values
                .ToList();

            // Symulacja operacji asynchronicznej
            await Task.CompletedTask;

            return filteredItems;
        }

        private async Task<List<TVm>> FilterAsync(List<TVm> items, bool isCompany, int detailId)
        {
            var result = new List<TVm>();

            foreach (var item in items)
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                if (user != null && CheckUserRole(user, isCompany))
                {
                    await PopulatePlantItemDetailsAsync(item, user, detailId, isCompany);
                    result.Add(item);
                }
            }
            return result;
        }
        private bool CheckUserRole(ApplicationUser user, bool isCompany)
        {
            return isCompany
                ? _userManager.IsInRoleAsync(user, "Company").Result
                : _userManager.IsInRoleAsync(user, "PRIVATE_USER").Result;
        }

        private async Task PopulatePlantItemDetailsAsync(TVm item, ApplicationUser user, int detailId, bool isCompany)
        {
            item.AccountName = isCompany ? user.CompanyName : _userContactDataService.UserAccountName(user);
            item.Date = item.DateAdded.ToShortDateString();

            var contactId = isCompany
                ? await _userPlantService.GetContactDetailForPlantAsync(item.Id, id => _userRepo.GetContactDetailForSeedAsync(item.Id))
                : await _userPlantService.GetContactDetailForPlantAsync(item.Id, id=> _userRepo.GetContactDetailForSeedlingAsync(item.Id));

            if (contactId.HasValue)
            {
                var contactDetails = await _userPlantService.GetContactDetailAsync(contactId.Value);
                item.ContactDetail = _mapper.Map<ContactDetailVm>(contactDetails);
            }
            else
            {
                item.ContactDetail = new ContactDetailVm { ContactDetailInformation = "" };
            }

            item.PlantOpinions = _plantRepo.GetPlantOpinions(detailId)
                .Where(p => p.UserId == user.Id)
                .ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
