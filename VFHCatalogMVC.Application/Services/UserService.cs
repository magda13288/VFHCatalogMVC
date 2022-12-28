using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Adresses;
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
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepo, IMapper mapper, UserManager<ApplicationUser> userManager, IPlantRepository plantRepository )
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _userManager = userManager;
            _plantRepo = plantRepository;
        }

        public void AddAddress(AddressVm address)
        {
            var addressToSave = _mapper.Map<Address>(address);
            _userRepo.AddAddress(addressToSave);
        }

        public UserSeedsForListVm GetUserSeeds(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            var seeds = _plantRepo.GetUserPlantSeeds(user.Result.Id).ProjectTo<UserSeedsVm>(_mapper.ConfigurationProvider).ToList();
            var seedsToList = new List<UserSeedsVm>();
            var seedsToShow = new List<UserSeedsVm>();

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

        public UserSeedsVm GetSeedsList(Plant plant, UserSeedsVm item)
        {
            item.PlantForList = new PlantForListVm();
            item.PlantForList.FullName = plant.FullName;
            item.PlantForList.Photo = plant.Photo;
            return item;
        }
        public UserSeedlingVm GetSeedlingsList(Plant plant, UserSeedlingVm item)
        {
            item.PlantForList = new PlantForListVm();
            item.PlantForList.FullName = plant.FullName;
            item.PlantForList.Photo = plant.Photo;
            return item;
        }
        public List<SelectListItem> FillCountryList(List<CountryVm> countries)
        {        
                List<SelectListItem> propertyList = new List<SelectListItem>();

                if (countries != null)
                {
                    propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                    foreach (var type in countries)
                    {
                        propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                    }
                }
                else
                {
                    propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });
                }
                return propertyList;
        }

        public List<SelectListItem> FillRegionList(List<RegionVm> regions)
        {
            List<SelectListItem> propertyList = new List<SelectListItem>();

            if (regions != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in regions)
                {
                    propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                }
            }
            else
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });
            }
            return propertyList;
        }

        public List<SelectListItem> FillCityList(List<CityVm> city)
        {
            List<SelectListItem> propertyList = new List<SelectListItem>();

            if (city != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in city)
                {
                    propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                }
            }
            else
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });
            }
            return propertyList;
        }
        public AddressVm GetAddress(string userId)
        {
            var address = _userRepo.GetAddressInfo(userId);
            var addressVm = _mapper.Map<AddressVm>(address);

            return addressVm;
        }

        public List<CityVm> GetCities(int regionId)
        {
            var cities = _userRepo.GetCities(regionId).ProjectTo<CityVm>(_mapper.ConfigurationProvider).ToList();
            return cities;
        }

        public List<CountryVm> GetCountries()
        {
            var countries = _userRepo.GetCountries().ProjectTo<CountryVm>(_mapper.ConfigurationProvider).ToList();
            return countries;
        }

        public List<RegionVm> GetRegions(int countryId)
        {
            var regions = _userRepo.GetRegions(countryId).ProjectTo<RegionVm>(_mapper.ConfigurationProvider).ToList();
            return regions;
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
        public UserSeedsVm GetUserSeedToEdit(int id)
        {
            var plantSeed = _userRepo.GetUserSeed(id);
            var userSedd = _mapper.Map<UserSeedsVm>(plantSeed);
            return userSedd;
            
        }
        public void UpdateSeed(UserSeedsVm seed)
        {
            if (seed != null)
            {
                var seedToEdit = _mapper.Map<PlantSeed>(seed);
                _userRepo.EditUserSeed(seedToEdit);
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
            var seedlings = _plantRepo.GetUserPlantSeedlings(user.Result.Id).ProjectTo<UserSeedlingVm>(_mapper.ConfigurationProvider).ToList();
            var seedlingsToList = new List<UserSeedlingVm>();
            var seedlingsToShow = new List<UserSeedlingVm>();

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

            return userSeedling;
        }
        public void UpdateSeedling(UserSeedlingVm seedling)
        {
            if (seedling != null)
            {
                var seedlingToEdit = _mapper.Map<PlantSeedling>(seedling);
                _userRepo.EditUserSeedling(seedlingToEdit);
            }
        }
        public void DeleteSeedling(int id)
        {
            var seedling = _userRepo.GetUserSeedling(id);
           _userRepo.DeleteUserSeedling(seedling);
        }

        public ContactDetail GetContactDetail(int id)
        {
           var contactDetails = _userRepo.GetContactDetail(id);
            return contactDetails;
        }

        public int GetContactDetailForSeed(int id)
        {
            var contactId = _userRepo.GetContactDetailForSeed(id);
            return contactId;
        }

        public int GetContactDetailForSeedling(int id)
        {
            var contactId = _userRepo.GetContactDetailForSeedling(id);
            return contactId;
        }
    }
}
