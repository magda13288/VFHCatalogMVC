using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Filters;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.User;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPlantRepository _plantRepo;
        private readonly IHelperUserRepository _helperUserRepo;
        private readonly IHelperPlantRepository _helperPlantRepo;
        private readonly IMapper _mapper;
        private readonly ClientMessagesService _clientMessagesService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepo, IMapper mapper, UserManager<ApplicationUser> userManager, IPlantRepository plantRepository, IHelperUserRepository helperUserRepository, ClientMessagesService clientMessagesService, IHelperPlantRepository helperPlantRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _userManager = userManager;
            _plantRepo = plantRepository;
            _helperUserRepo = helperUserRepository;
            _clientMessagesService = clientMessagesService;
            _helperPlantRepo = helperPlantRepo;
        }

        public void AddAddress(AddressVm address)
        {
            var addressToSave = _mapper.Map<Address>(address);
            _userRepo.AddAddress(addressToSave);
        }

        public UserSeedsForListVm GetUserSeeds(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
           
            var seeds = _userRepo.GetUserPlantSeeds(user.Result.Id).ProjectTo<UserSeedVm>(_mapper.ConfigurationProvider).ToList();
            var seedsToList = new List<UserSeedVm>();
            var seedsToShow = new List<UserSeedVm>();

            var userRole = _userManager.IsInRoleAsync(user.Result, "Company");
            if (userRole.Result is true)
            {
                foreach ( var seed in seeds ) 
            {
                    seed.Date = seed.DateAdded.ToShortDateString();

                    var contactId = _userRepo.GetContactDetailForSeed(seed.Id);
                    if (contactId != null)
                    {
                        var contactDetails = _userRepo.GetContactDetail(contactId);
                        var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                        seed.ContactDetail = new ContactDetailVm();
                        seed.ContactDetail = contactDetailsVm;
                    }
                    else
                    {
                        seed.ContactDetail = new ContactDetailVm();
                        seed.ContactDetail.ContactDetailInformation = "";
                    }
                }
            }

            if (typeId == 0 && searchString == "")
            {
                foreach (var item in seeds)
                {
                    var plant = _plantRepo.GetPlantById(item.PlantId);
                    seedsToList.Add(GetSeedsList(plant,item));
                }
                seedsToShow = seedsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
            }
            else
            {
                if (searchString == "")
                {
                    if (typeId != 0 && typeId != null)
                    {
                        if (groupId != 0 && groupId != null)
                        {
                            //projectTo wykorzystywane przy kolekcjach IQueryable
                            if (sectionId != 0 && sectionId != null)
                            {
                                foreach (var item in seeds)
                                {
                                    var plant = _plantRepo.GetPlantById(item.PlantId);
                                    if (plant.PlantTypeId == typeId && plant.PlantGroupId == groupId && plant.PlantSectionId == sectionId)
                                    {
                                        seedsToList.Add(GetSeedsList(plant, item));                         
                                    }
                                }
                                seedsToShow = seedsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                            }
                            else
                            {
                                foreach (var item in seeds)
                                {
                                    var plant = _plantRepo.GetPlantById(item.PlantId);
                                    if (plant.PlantTypeId == typeId && plant.PlantGroupId == groupId)
                                    {
                                        
                                        seedsToList.Add(GetSeedsList(plant,item));
                                    }
                                }
                                seedsToShow = seedsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                            }
                        }
                        else
                        {
                            foreach (var item in seeds)
                            {
                                var plant = _plantRepo.GetPlantById(item.PlantId);
                                if (plant.PlantTypeId == typeId)
                                {
                                   
                                    seedsToList.Add(GetSeedsList(plant,item));
                                }
                            }
                            seedsToShow = seedsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                        }
                    }
                }
                else
                {
                    foreach (var item in seeds)
                    {
                        var plant = _plantRepo.GetPlantById(item.PlantId);
                        if (plant.FullName.StartsWith(searchString))
                        {
                            seedsToList.Add(GetSeedsList(plant,item));
                        }
                    }
                    seedsToShow = seedsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                }
            }

            var plantSeedsList = new UserSeedsForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                UserSeeds = seedsToShow,
                SearchString = searchString,
                Count = seeds.Count,              
            };
            return plantSeedsList;
        }

        public UserSeedVm GetSeedsList(Plant plant, UserSeedVm item)
        {
            item.PlantForList = new PlantForListVm();
            item.PlantForList.FullName = plant.FullName;
            item.PlantForList.Photo = plant.Photo;
            item.Date = item.DateAdded.ToShortDateString();
            return item;
        }
        public UserSeedlingVm GetSeedlingsList(Plant plant, UserSeedlingVm item)
        {
            item.PlantForList = new PlantForListVm();
            item.PlantForList.FullName = plant.FullName;
            item.PlantForList.Photo = plant.Photo;
            item.Date = item.DateAdded.ToShortDateString();
            return item;
        }
        public AddressVm GetAddress(string userId)
        {
            var address = _userRepo.GetAddressInfo(userId);
            var addressVm = _mapper.Map<AddressVm>(address);

            return addressVm;
        }
        public List<string> FilterUsers(int countryId, int regionId, int cityId, List<PlantSeedVm> seeds, List<PlantSeedlingVm> seedlings)
        {
            var usersList = new List<string>();
            var address = new Address();
            //var genericList = typeof(List<>);
            //var dataType = new Type[] { typeof(PlantSeedVm), typeof(PlantSeedlingVm) };     

            if (seeds is null)
            {
                foreach (var item in seedlings)
                {
                    address = _userRepo.GetAddressInfo(item.UserId);

                    if (countryId != 0)
                    {
                        if (regionId == 0 && cityId == 0)
                        {
                            if (address.CountryId == countryId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }
                        if (regionId != 0 && cityId == 0)
                        {
                            if (address.CountryId == countryId && address.RegionId == regionId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }
                        if (regionId != 0 && cityId != 0)
                        {
                            if (address.CountryId == countryId && address.RegionId == regionId && address.CityId == cityId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }

                    }
                }
            }
            else
            {
                foreach (var item in seeds)
                {
                    address = _userRepo.GetAddressInfo(item.UserId);

                    if (countryId != 0)
                    {
                        if (regionId == 0 && cityId == 0)
                        {
                            if (address.CountryId == countryId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }
                        if (regionId != 0 && cityId == 0)
                        {
                            if (address.CountryId == countryId && address.RegionId == regionId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }
                        if (regionId != 0 && cityId != 0)
                        {
                            if (address.CountryId == countryId && address.RegionId == regionId && address.CityId == cityId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }

                    }
                }
            }

            return usersList;
        }
        public UserSeedVm GetUserSeedToEdit(int id)
        {
            var plantSeed = _userRepo.GetUserSeed(id);
            var userSedd = _mapper.Map<UserSeedVm>(plantSeed);

            var user = _userManager.FindByIdAsync(userSedd.UserId);
            var userRole = _userManager.IsInRoleAsync(user.Result, "Company");
            if (userRole.Result is true)
            {
                var seedContactDetails = _userRepo.GetContactDetailForSeed(plantSeed.Id);
                if (seedContactDetails != 0)
                {
                    var contactDetails = _userRepo.GetContactDetail(seedContactDetails);
                    var contacDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                    userSedd.ContactDetail = contacDetailsVm;

                }
            }
            
            return userSedd;
            
        }
        public void UpdateSeed(UserSeedVm seed)
        {
            if (seed != null)
            {
                var seedToEdit = _mapper.Map<PlantSeed>(seed);
                _userRepo.EditUserSeed(seedToEdit);

                if (seed.ContactDetail != null)
                {
                    var seedDetails = _userRepo.GetContactDetailForSeed(seed.Id);
                    if (seedDetails != null)
                    {
                        var contact = _userRepo.GetContactDetail(seedDetails);
                        seed.ContactDetail.Id = contact.Id;
                        seed.ContactDetail.ContactDetailTypeID = contact.ContactDetailTypeID;
                        seed.ContactDetail.UserId = contact.UserId;
                        var contactDetails = _mapper.Map<ContactDetail>(seed.ContactDetail);
                        _userRepo.EditContactDetails(contactDetails);
                    }
                    else
                    {
                        var contact = new ContactDetailVm();
                        contact.ContactDetailTypeID = 1;
                        contact.UserId = seed.UserId;
                        contact.ContactDetailInformation = seed.ContactDetail.ContactDetailInformation;
                        var contactDetails = _mapper.Map<ContactDetail>(contact);
                        var id= _plantRepo.AddContactDetail(contactDetails);

                        var contactDetailsForSeedVm = new ContactDetailForSeedVm() {ContactDetailId=id,PlantSeedId=seed.Id };
                        var contactDetailsForSeed = _mapper.Map<ContactDetailForSeed>(contactDetailsForSeedVm);
                        _plantRepo.AddContactDetailForSeed(contactDetailsForSeed);


                    }
                }
            }
        }
        public void DeleteSeed(int id)
        {
            var seed = _userRepo.GetUserSeed(id);
            _userRepo.DeleteUserSeed(seed);
        }

        public UserSeedlingsForListVm GetUserSeedlings(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            var seedlings = _userRepo.GetUserPlantSeedlings(user.Result.Id).ProjectTo<UserSeedlingVm>(_mapper.ConfigurationProvider).ToList();
            var seedlingsToList = new List<UserSeedlingVm>();
            var seedlingsToShow = new List<UserSeedlingVm>();

            var userRole = _userManager.IsInRoleAsync(user.Result, "Company");
            if (userRole.Result is true)
            {
                foreach (var seedling in seedlings)
                {
                    seedling.Date = seedling.DateAdded.ToShortDateString();

                    var contactId = _userRepo.GetContactDetailForSeedling(seedling.Id);
                    if (contactId != null)
                    {
                        var contactDetails = _userRepo.GetContactDetail(contactId);
                        var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                        seedling.ContactDetail = new ContactDetailVm();
                        seedling.ContactDetail = contactDetailsVm;
                    }
                    else
                    {
                        seedling.ContactDetail = new ContactDetailVm();
                        seedling.ContactDetail.ContactDetailInformation = "";
                    }
                    
                }
            }

            if (typeId == 0 && searchString == "")
            {
                foreach (var item in seedlings)
                {
                    var plant = _plantRepo.GetPlantById(item.PlantId);                  
                    seedlingsToList.Add(GetSeedlingsList(plant,item));
                }
                seedlingsToShow = seedlingsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
            }
            else
            {
                if (searchString == "")
                {
                    if (typeId != 0 && typeId != null)
                    {
                        if (groupId != 0 && groupId != null)
                        {
                            //projectTo wykorzystywane przy kolekcjach IQueryable
                            if (sectionId != 0 && sectionId != null)
                            {
                                foreach (var item in seedlings)
                                {
                                    var plant = _plantRepo.GetPlantById(item.PlantId);
                                    if (plant.PlantTypeId == typeId && plant.PlantGroupId == groupId && plant.PlantSectionId == sectionId)
                                    {
                                        seedlingsToList.Add(GetSeedlingsList(plant,item));
                                    }
                                }
                                seedlingsToShow = seedlingsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                            }
                            else
                            {
                                foreach (var item in seedlings)
                                {
                                    var plant = _plantRepo.GetPlantById(item.PlantId);
                                    if (plant.PlantTypeId == typeId && plant.PlantGroupId == groupId)
                                    {
                                        seedlingsToList.Add(GetSeedlingsList(plant,item));
                                    }
                                }
                                seedlingsToShow = seedlingsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                            }
                        }
                        else
                        {
                            foreach (var item in seedlings)
                            {
                                var plant = _plantRepo.GetPlantById(item.PlantId);
                                if (plant.PlantTypeId == typeId)
                                {
                                    seedlingsToList.Add(GetSeedlingsList(plant,item));
                                }
                            }
                            seedlingsToShow = seedlingsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                        }
                    }
                }
                else
                {
                    foreach (var item in seedlings)
                    {
                        var plant = _plantRepo.GetPlantById(item.PlantId);
                        if (plant.FullName.StartsWith(searchString))
                        {
                            seedlingsToList.Add(GetSeedlingsList(plant,item));
                        }
                    }
                    seedlingsToShow = seedlingsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                }
            }

            var plantSeedlingsList = new UserSeedlingsForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                UserSeedlings = seedlingsToShow,
                SearchString = searchString,
                Count = seedlings.Count,
            };
            return plantSeedlingsList;
        }
        public UserSeedlingVm GetUserSeedlingToEdit(int id)
        {
         
            var plantSeedling = _userRepo.GetUserSeedling(id);
            var userSeedling = _mapper.Map<UserSeedlingVm>(plantSeedling);

            var user = _userManager.FindByIdAsync(userSeedling.UserId);
            var userRole = _userManager.IsInRoleAsync(user.Result, "Company");
            if (userRole.Result is true)
            {
                var seedContactDetails = _userRepo.GetContactDetailForSeed(plantSeedling.Id);
                if (seedContactDetails != 0)
                {
                    var contactDetails = _userRepo.GetContactDetail(seedContactDetails);
                    var contacDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                    userSeedling.ContactDetail = contacDetailsVm;
                }
            }
            return userSeedling;
        }
        public void UpdateSeedling(UserSeedlingVm seedling)
        {
            if (seedling != null)
            {
                var seedlingToEdit = _mapper.Map<PlantSeedling>(seedling);
                _userRepo.EditUserSeedling(seedlingToEdit);

                if (seedling.ContactDetail != null)
                {
                    var seedlingDetails = _userRepo.GetContactDetailForSeedling(seedling.Id);
                    if (seedlingDetails != null)
                    {
                        var contact = _userRepo.GetContactDetail(seedlingDetails);
                        seedling.ContactDetail.Id = contact.Id;
                        seedling.ContactDetail.ContactDetailTypeID = contact.ContactDetailTypeID;
                        seedling.ContactDetail.UserId = contact.UserId;
                        var contactDetails = _mapper.Map<ContactDetail>(seedling.ContactDetail);
                        _userRepo.EditContactDetails(contactDetails);
                    }
                    else
                    {
                        var contact = new ContactDetailVm();
                        contact.ContactDetailTypeID = 1;
                        contact.UserId = seedling.UserId;
                        contact.ContactDetailInformation = seedling.ContactDetail.ContactDetailInformation;
                        var contactDetails = _mapper.Map<ContactDetail>(contact);
                        var id = _plantRepo.AddContactDetail(contactDetails);

                        var contactDetailsForSeedlingVm = new ContactDetailForSeedlingVm() { ContactDetailId = id, PlantSeedlingId = seedling.Id };
                        var contactDetailsForSeedling = _mapper.Map<ContactDetailForSeedling>(contactDetailsForSeedlingVm);
                        _plantRepo.AddContactDetailForSeedling(contactDetailsForSeedling);
                    }
                }
            }
        }
        public void DeleteSeedling(int id)
        {
            var seedling = _userRepo.GetUserSeedling(id);
           _userRepo.DeleteUserSeedling(seedling);
        }

        public ContactDetail GetContactDetail(int? id)
        {
           var contactDetails = _userRepo.GetContactDetail(id);
            return contactDetails;
        }

        public int? GetContactDetailForSeed(int id)
        {
            var contactId = _userRepo.GetContactDetailForSeed(id);
            return contactId;
        }

        public int? GetContactDetailForSeedling(int id)
        {
            var contactId = _userRepo.GetContactDetailForSeedling(id);
            return contactId;
        }

        public void AddNewUserPlant(int plantId,string userId)
        {
            var newPlant = new NewUserPlantVm { PlantId = plantId,UserId = userId };
            var newUserPlant = _mapper.Map<NewUserPlant>(newPlant);

            _userRepo.AddNewUserPlant(newUserPlant);
           
        }

        public NewUserPlantsForListVm GetNewUserPlants(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, bool viewAll,string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            var userRole = _userManager.GetRolesAsync(user.Result);
            var plants = new List<NewUserPlantVm>();

            if (userRole.Result.Count > 0 && userRole.Result.Contains("Admin") == true)
            {
                plants = _userRepo.GetAllNewUserPlants().ProjectTo<NewUserPlantVm>(_mapper.ConfigurationProvider).ToList();
            }
            else
            {
                plants = _userRepo.GetNewUserPlants(user.Result.Id).ProjectTo<NewUserPlantVm>(_mapper.ConfigurationProvider).ToList();
            }

            var plantsToList = new List<NewUserPlantVm>();
            var plantsToShow = new List<NewUserPlantVm>();

            foreach (var plant in plants)
            {
                var details = _plantRepo.GetPlantById(plant.PlantId);
                var detailsVm = _mapper.Map<PlantForListVm>(details);
                plant.PlantForList = new PlantForListVm();
                plant.PlantForList = detailsVm;
            }

            if (viewAll is true)
            {
                plantsToShow = plants.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
            }
            else
            {
                if (typeId != 0 && typeId != null)
                {
                    if (groupId != 0 && groupId != null)
                    {
                        //projectTo wykorzystywane przy kolekcjach IQueryable
                        if (sectionId != 0 && sectionId != null)
                        {
                            foreach (var plant in plants)
                            {

                                if (plant.PlantForList.TypeId == typeId && plant.PlantForList.GroupId == groupId && plant.PlantForList.SectionId == sectionId)
                                {
                                    plantsToList.Add(plant);
                                }
                            }
                            plantsToShow = plantsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                        }
                        else
                        {
                            foreach (var plant in plants)
                            {
                                if (plant.PlantForList.TypeId == typeId && plant.PlantForList.GroupId == groupId)
                                {
                                    plantsToList.Add(plant);
                                }
                            }
                            plantsToShow = plantsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                        }
                    }
                    else
                    {
                        foreach (var plant in plants)
                        {                     
                            if (plant.PlantForList.TypeId == typeId)
                            {
                                plantsToList.Add(plant);
                            }
                        }
                        plantsToShow = plantsToList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                    }
                }

            }

            var newUserPlantsForList = new NewUserPlantsForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                NewUserPlants = plantsToShow,
                Count= plants.Count,
                ViewAll= viewAll,
            };

            return newUserPlantsForList;
        }

        public FiltersListToShowVm GetAllFilters(int pageSize, int? pageNo, int typeId, int? groupId, int? sectionId)
        {
            if (groupId == 0)
                groupId = null;
            if (sectionId == 0)
                sectionId = null;

            var filters = _helperUserRepo.GetAllFilters(typeId, groupId, sectionId).ProjectTo<FiltersVm>(_mapper.ConfigurationProvider).ToList();

            var filtersValue = GetFiltersValues(filters);

            var filtersToShow = filtersValue.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();

            var filtersListToShow = new FiltersListToShowVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                Filters = filtersToShow,
                Count = filters.Count,

            };

            return filtersListToShow;
      
        }
        //* - select all records from table
        public List<FiltersValuesVm> GetFiltersValues(List<FiltersVm> filters)
        {
            var filtersValue = new List<FiltersValuesVm>();

                foreach (var filter in filters)
                {
                    filter.FiltersValues = new FiltersValuesVm();

                    filter.FiltersValues.Id = filter.Id;
                    filter.FiltersValues.Color = _clientMessagesService._GET_ALL_FILTERS_FROM_TABLE;
                    filter.FiltersValues.Destination = _clientMessagesService._GET_ALL_FILTERS_FROM_TABLE;
                    filter.FiltersValues.Position = _clientMessagesService._GET_ALL_FILTERS_FROM_TABLE;
                    filter.FiltersValues.AdditionalFeatures = _clientMessagesService._GET_ALL_FILTERS_FROM_TABLE;
                    filter.FiltersValues.GrowingSeazon = _clientMessagesService._GET_ALL_FILTERS_FROM_TABLE;
                    filter.FiltersValues.PlantTypeId = filter.PlantTypeId;
                    filter.FiltersValues.PlantGroupId = filter.PlantGroupId;
                    filter.FiltersValues.PlantSectionId= filter.PlantSectionId;
                                      
                    if (filter.FruitSizeId != null)
                    {
                        filter.FiltersValues.Color = _helperPlantRepo.GetFruitSizeValue(filter.FruitSizeId.Value);
                    }
                    if (filter.FruitTypeId != null)
                    {
                        filter.FiltersValues.FruitType = _helperPlantRepo.GetFruitTypeValue(filter.FruitTypeId.Value);
                    }
                    if (filter.GrowthTypeId != null)
                    {
                        filter.FiltersValues.GrowthType = _helperPlantRepo.GetGrowthTypeValue(filter.GrowthTypeId.Value);
                    }
                    if (filter.HeightId != null)
                    {
                        filter.FiltersValues.Height = _helperPlantRepo.GetHeightValue(filter.HeightId.Value);
                    }
                    if (filter.PollinationId != null)
                    {
                        filter.FiltersValues.Pollination = _helperPlantRepo.GetPollinationValue(filter.PollinationId.Value);
                    }

                filtersValue.Add(filter.FiltersValues);
                }

            return filtersValue;
        }
    }
}
