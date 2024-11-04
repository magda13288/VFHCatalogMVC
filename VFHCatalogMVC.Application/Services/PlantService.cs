using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;
using System.Numerics;
using System.Drawing;
using VFHCatalogMVC.Application.ViewModels.User;

namespace VFHCatalogMVC.Application.Services
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _plantRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public PlantService()
        {
                
        }
        public PlantService(IPlantRepository plantRepo, IMapper mapper, UserManager<ApplicationUser> userManager, IUserService userService, IImageService imageService)
        {
            _plantRepo = plantRepo;
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
            _imageService = imageService;
        }

        public int AddPlant(NewPlantVm model, string user)
        {
            int id = 0;

            if (model.SectionId == 0)
                model.SectionId = null;

            //check if adding plant does't exist in database

            var plantList = GetAllActivePlantsForList(1, 10, model.FullName,null,null,null);

            if (plantList.Count == 0)
            {
                //Save to table Plant
                var newPlant = _mapper.Map<Plant>(model);

                if (model.Photo != null)
                {
                    string _DIR = "plantGallery/searchPhoto";
                    var fileName = _imageService.UploadImage(model.Photo, model.FullName, _DIR);
                    newPlant.Photo = fileName;
                }

                var userInfo = _userManager.FindByNameAsync(user);
                //_userManager.Dispose();
                var userRole = _userManager.IsInRoleAsync(userInfo.Result, "Admin");

                if (userRole.Result is true)
                {
                    newPlant.isActive = true;
                    newPlant.isNew = false;
                    id = _plantRepo.AddPlant(newPlant);
                }
                else
                {
                    newPlant.isActive = false;
                    newPlant.isNew = true;

                    id = _plantRepo.AddPlant(newPlant);

                    _userService.AddNewUserPlant(id, userInfo.Result.Id);
                }
                
                if (id != 0)
                {
                    //Save to table PlantDetails
                    if (model.PlantDetails.ColorId == 0)
                        model.PlantDetails.ColorId = null;
                    if (model.PlantDetails.FruitSizeId == 0)
                        model.PlantDetails.FruitSizeId = null;
                    if (model.PlantDetails.FruitTypeId == 0)
                        model.PlantDetails.FruitTypeId = null;

                    var newPlantDetail = _mapper.Map<PlantDetail>(model.PlantDetails);
                    var plantDetailId = _plantRepo.AddPlantDetails(newPlantDetail, id);

                    //Save to PlantGrowthTypes
                    if (model.PlantDetails.ListGrowthTypes != null)
                    {
                        if (model.PlantDetails.ListGrowthTypes.GrowthTypesIds.Length > 0)
                        {
                            _plantRepo.AddPlantGrowthTypes(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, plantDetailId);
                        }
                    }
                    //Save to PlantDestinations
                    if (model.PlantDetails.ListPlantDestinations != null)
                    {
                        if (model.PlantDetails.ListPlantDestinations.DestinationsIds.Length > 0)
                        {
                            _plantRepo.AddPlantDestinations(model.PlantDetails.ListPlantDestinations.DestinationsIds, plantDetailId);
                        }
                    }
                    //Save to PlantGrowingSeaznos
                    if (model.PlantDetails.ListGrowingSeazons != null)
                    {
                        if (model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds.Length > 0)
                        {
                            _plantRepo.AddPlantGrowingSeazons(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, plantDetailId);
                        }
                    }

                    if (model.PlantDetails.Images != null)
                    {
                        if (model.PlantDetails.Images.Count > 0)
                        {
                            string _DIR = "plantGallery/plantDetailsGallery";

                            foreach (var item in model.PlantDetails.Images)
                            {
                                string fileName = _imageService.UploadImage(item, model.FullName, _DIR);
                                _plantRepo.AddPlantDetailsImages(fileName, plantDetailId);
                            }
                        }
                    }
                }
                //else
                //{
                //    return id;
                //}
                return id;
            }
            else
            {
                return 0;
            }
            
        }
        public ListPlantForListVm GetAllActivePlantsForList(int pageSize, int? pageNo, string searchString, int? typeId, int? groupId, int? sectionId)
        {
            var plants = new List<PlantForListVm>();
            var plantsToShow = new List<PlantForListVm>();

            if (searchString == "")
            {
                if (typeId != 0 && typeId != null)
                {
                    if (groupId != 0 && groupId != null)
                    {
                        //projectTo wykorzystywane przy kolekcjach IQueryable
                        if (sectionId != 0 && sectionId != null)
                        {
                            plants = _plantRepo.GetAllActivePlants().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId)
                               .ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();
                        }
                        else
                        {
                            plants = _plantRepo.GetAllActivePlants().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId)
                               .ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();

                        }
                    }
                    else
                    {
                        plants = _plantRepo.GetAllActivePlants().Where(p => p.PlantTypeId == typeId).ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();
                    }
                }
            }
            else
            {
                plants = _plantRepo.GetAllActivePlants().Where(p => p.FullName.StartsWith(searchString))
                       .ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();
            }

            plantsToShow = plants.Skip(pageSize * (pageNo.Value - 1)).Take(pageSize).ToList();

            var plantsList = new ListPlantForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                SearchString = searchString,
                Plants = plantsToShow,
                Count = plants.Count,
            };

            return plantsList;

        }
        public PlantSeedsForListVm GetAllPlantSeeds(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany, string userName)
        {
            var seeds = new List<PlantSeedVm>();
            var seedsToShow = new List<PlantSeedVm>();
            var opinions = new List<PlantOpinionsVm>();
            var filteredUsersList = new List<string>();
            var filteredSeeds = new List<PlantSeedVm>();
            var seedsList = new List<PlantSeedVm>();

            var detailId = _plantRepo.GetPlantDetailId(id);
            seeds = _plantRepo.GetPlantSeeds(id).ProjectTo<PlantSeedVm>(_mapper.ConfigurationProvider).ToList();

            if (id != 0 && countryId == 0 && regionId == 0 && cityId == 0)
            {
                foreach (var item in seeds)
                {
                    var user = _userManager.FindByIdAsync(item.UserId);
                    item.PlantOpinions = new List<PlantOpinionsVm>();

                    if (isCompany is true)
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "Company");
                        if (role.Result == true)
                        {
                            item.AccountName = user.Result.CompanyName;
                            item.Date = item.DateAdded.ToShortDateString();
                            var contactId = _userService.GetContactDetailForSeed(item.Id);

                            if (contactId != null)
                            {
                                var contactDetails = _userService.GetContactDetail(contactId);
                                var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                                item.ContactDetail = new ContactDetailVm();
                                item.ContactDetail = contactDetailsVm;
                            }
                            else
                            {
                                item.ContactDetail = new ContactDetailVm();
                                item.ContactDetail.ContactDetailInformation = "";
                            }
                           
                          

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;

                            filteredUsersList.Add(user.Result.Id);

                            seedsList = FilterSeedsList(seeds, filteredUsersList);

                            seedsToShow = seedsList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                           
                        }
                    }
                    else
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "PRIVATE_USER");
                        if (role.Result == true)
                        {
                            item.AccountName =UserAccountName(user);
                            item.Date = item.DateAdded.ToShortDateString();

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;

                            filteredUsersList.Add(user.Result.Id);
                            seedsList = FilterSeedsList(seeds, filteredUsersList);

                            seedsToShow = seedsList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                        }
                    }
                }           
                
            }
            else
            {
                filteredUsersList = _userService.FilterUsers(countryId, regionId, cityId, seeds, null);

                seedsList = FilterSeedsList(seeds, filteredUsersList);
                var seedsListFiltered = new List<PlantSeedVm>();
               
                foreach (var item in seedsList)
                {
                    var user = _userManager.FindByIdAsync(item.UserId);
                    item.PlantOpinions = new List<PlantOpinionsVm>();

                    if (isCompany is true)
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "Company");
                        if (role.Result == true)
                        {                        
                            item.AccountName = user.Result.CompanyName;
                            item.Date = item.DateAdded.ToShortDateString();
                            var contactId = _userService.GetContactDetailForSeed(item.Id);
                            var contactDetails = _userService.GetContactDetail(contactId);
                            var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                            item.ContactDetail = new ContactDetailVm();
                            item.ContactDetail = contactDetailsVm;

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;
                            seedsListFiltered.Add(item);
                      
                        }
                    }
                    else
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "PRIVATE_USER");
                        if (role.Result == true)
                        {
                            item.AccountName = UserAccountName(user);
                            item.Date = item.DateAdded.ToShortDateString();

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;

                            seedsListFiltered.Add(item);
                        }
                    }
                }

                seedsToShow = seedsListFiltered.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
            }

            string userId = null;

            if (userName != null)
            {
               var userInfo = _userManager.FindByNameAsync(userName);
               userId = userInfo.Result.Id;
            }

            var plantSeedsList = new PlantSeedsForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                PlantSeeds = seedsToShow,
                Count = seeds.Count,
                PlantId = id,
                isCompany = isCompany,
                LoggedUserName = userId
                
            };

            return plantSeedsList;
        }        
        public PlantSeedlingsForListVm GetAllPlantSeedlings(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany)
        {
            var seedlings = new List<PlantSeedlingVm>();
            var seedlingsToShow = new List<PlantSeedlingVm>();
            var opinions = new List<PlantOpinionsVm>();
            var filteredUsersList = new List<string>();
            var seedlingsList = new List<PlantSeedlingVm>();

            var detailId = _plantRepo.GetPlantDetailId(id);
            seedlings = _plantRepo.GetPlantSeedlings(id).ProjectTo<PlantSeedlingVm>(_mapper.ConfigurationProvider).ToList();

            if (id != 0 && countryId == 0 && regionId == 0 && cityId == 0)
            {
                foreach (var item in seedlings)
                {
                    item.PlantOpinions = new List<PlantOpinionsVm>();
                    var user = _userManager.FindByIdAsync(item.UserId);
                    if (isCompany is true)
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "Company");
                        if (role.Result == true)
                        {
                            item.AccountName = user.Result.CompanyName;
                            item.Date = item.DateAdded.ToShortDateString();
                            var contactId = _userService.GetContactDetailForSeedling(item.Id);
                            var contactDetails = _userService.GetContactDetail(contactId);
                            var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                            item.ContactDetail = new ContactDetailVm();
                            item.ContactDetail = contactDetailsVm;

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;

                            filteredUsersList.Add(user.Result.Id);

                            seedlingsList = FilterSeedlingsList(seedlings, filteredUsersList);

                            seedlingsToShow = seedlingsList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                        }
                    }
                    else
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "PRIVATE_USER");
                        if (role.Result == true)
                        {
                            item.AccountName = UserAccountName(user);
                            item.Date = item.DateAdded.ToShortDateString();

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;

                            filteredUsersList.Add(user.Result.Id);
                            seedlingsList = FilterSeedlingsList(seedlings, filteredUsersList);


                            seedlingsToShow = seedlingsList.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
                        }
                    }
                }             
            }
            else
            {

                filteredUsersList = _userService.FilterUsers(countryId, regionId, cityId,null,seedlings);

                seedlingsList = FilterSeedlingsList(seedlings, filteredUsersList);
                var seedlingsListFiltered = new List<PlantSeedlingVm>();

                foreach (var item in seedlingsList)
                {
                    var user = _userManager.FindByIdAsync(item.UserId);
                    if (isCompany is true)
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "Company");

                        if (role.Result == true)
                        {
                            item.AccountName = user.Result.CompanyName;
                            item.Date = item.DateAdded.ToShortDateString();
                            var contactId = _userService.GetContactDetailForSeedling(item.Id);
                            var contactDetails = _userService.GetContactDetail(contactId);
                            var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                            item.ContactDetail = new ContactDetailVm();
                            item.ContactDetail = contactDetailsVm;

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                           item.PlantOpinions = opinions;
                           seedlingsListFiltered.Add(item);
                        }
                    }
                    else
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "PRIVATE_USER");
                        if (role.Result == true)
                        {
                            item.AccountName = UserAccountName(user);
                            item.Date = item.DateAdded.ToShortDateString();

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;
                            seedlingsListFiltered.Add(item);
                        }
                    }
                }

                seedlingsToShow =seedlingsListFiltered.Skip((pageSize * ((int)pageNo - 1))).Take(pageSize).ToList();
            }


            var plantSeedlingsList = new PlantSeedlingsForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                PlantSeedlings = seedlingsToShow,
                Count = seedlings.Count,
                PlantId = id,
                isCompany = isCompany
            };

            return plantSeedlingsList;
        }
        public PlantDetailsVm GetPlantDetails(int id)
        {
            var plantDetails = _plantRepo.GetPlantDetails(id);
            var plantDetailsVm = _mapper.Map<PlantDetailsVm>(plantDetails);

            if (plantDetails != null)
            {

                var plant = _plantRepo.GetPlantById(id);
                var plantVm = _mapper.Map<PlantForListVm>(plant);

                if (plantDetailsVm.ColorId != null)
                    plantDetailsVm.ColorName = _plantRepo.GetPlantColorName(plantDetailsVm.ColorId);
                else
                    plantDetailsVm.ColorName = null;

                if (plantDetailsVm.FruitSizeId != null)
                    plantDetailsVm.FruitSizeName = _plantRepo.GetPlantFruitSizeName(plantDetailsVm.FruitSizeId);
                else
                    plantDetailsVm.FruitSizeName = null;

                if (plantDetailsVm.FruitTypeId != null)
                    plantDetailsVm.FruitTypeName = _plantRepo.GetPlantFriutTypeName(plantDetailsVm.FruitTypeId);
                else
                    plantDetailsVm.FruitTypeName = null;

                plantDetailsVm.Plant = plantVm;

                //GrowthTypesNames

                var propertyNames = new List<string>();
                propertyNames = GetGrowthTypesNames(plantDetailsVm.Id);
                plantDetailsVm.ListGrowthTypes = new ListGrowthTypesVm();

                if (propertyNames != null)
                {

                    plantDetailsVm.ListGrowthTypes.GrowthTypesNames = new List<string>();

                    foreach (var item in propertyNames)
                    {
                        plantDetailsVm.ListGrowthTypes.GrowthTypesNames.Add(item);

                    }

                }
                propertyNames.Clear();

                //DestiantionsNames

                propertyNames = GetDestinationsNames(plantDetailsVm.Id);
                plantDetailsVm.ListPlantDestinations = new ListPlantDestinationsVm();

                if (propertyNames != null)
                {
                    plantDetailsVm.ListPlantDestinations.DestinationsNames = new List<string>();
                    foreach (var item in propertyNames)
                    {
                        plantDetailsVm.ListPlantDestinations.DestinationsNames.Add(item);

                    }
                }

                propertyNames.Clear();

                //GrowingSeazonsNames

                propertyNames = GetGrowingSeaznosNames(plantDetailsVm.Id);
                plantDetailsVm.ListGrowingSeazons = new ListGrowingSeazonsVm();

                if (propertyNames != null)
                {
                    plantDetailsVm.ListGrowingSeazons.GrwoingSeazonsNames = new List<string>();
                    foreach (var item in propertyNames)
                    {
                        plantDetailsVm.ListGrowingSeazons.GrwoingSeazonsNames.Add(item);
                    }
                }

                propertyNames.Clear();

                //PlantGallery
                var plantGallery = new List<PlantDetailsImagesVm>();

                plantGallery = _plantRepo.GetPlantDetailsImages(plantDetails.Id).ProjectTo<PlantDetailsImagesVm>(_mapper.ConfigurationProvider).ToList();

                if (plantGallery != null)
                {
                    if (plantGallery.Count > 0)
                    {
                        foreach (var image in plantGallery)
                        {
                            plantDetailsVm.PlantDetailsImages.Add(image);
                        }
                    }
                }

                var plantOpinions = new List<PlantOpinionsVm>();
                plantOpinions = _plantRepo.GetPlantOpinions(plantDetails.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();

                if (plantOpinions != null)
                {
                    foreach (var item in plantOpinions)
                    {
                        var userInfo = _userManager.FindByIdAsync(item.UserId);
                        item.Date = item.DateAdded.ToShortDateString();
                        item.AccountName = userInfo.Result.AccountName;
                        plantDetailsVm.PlantOpinions.Add(item);
                    }                  
                }
            }
            return plantDetailsVm;
        }

            public List<string> GetGrowthTypesNames(int id)
        {
            var plantGrowthTypes = new List<PlantGrowthTypeVm>();

            plantGrowthTypes = _plantRepo.GetPlantGrowthTypes(id).ProjectTo<PlantGrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
            var growthTypes = _plantRepo.GetGrowthTypes().ProjectTo<GrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
            var propertyNames = new List<string>();

            if (plantGrowthTypes != null)
            {
                var propertyIds = new List<int>();

                foreach (var items in plantGrowthTypes)
                {
                    propertyIds.Add(items.GrowthTypeId);
                }

                var growthTypesForPlant = new List<GrowthTypeVm>();

                foreach (var items in propertyIds)
                {
                    foreach (var item in growthTypes)
                    {
                        if (item.Id == items)
                        {
                            growthTypesForPlant.Add(growthTypes.FirstOrDefault(p => p.Id == items));

                        }
                    }
                }

                foreach (var i in growthTypesForPlant)
                {
                    propertyNames.Add(i.Name);
                    propertyNames.Add(", ");
                }
                propertyNames = propertyNames.Take(propertyNames.Count - 1).ToList();
            }

            return propertyNames;
        }

        public List<string> GetDestinationsNames(int id)
        {
            var plantDestinations = new List<PlantDestinationsVm>();
            plantDestinations = _plantRepo.GetPlantDestinations(id).ProjectTo<PlantDestinationsVm>(_mapper.ConfigurationProvider).ToList();
            var destinations = _plantRepo.GetDestinations().ProjectTo<DestinationsVm>(_mapper.ConfigurationProvider).ToList();
           
            var propertyNames = new List<string>();

            if (plantDestinations != null)
            {
                var destinationsForPlants = new List<DestinationsVm>();
                var propertyIds = new List<int>();

                foreach (var items in plantDestinations)
                {
                    propertyIds.Add(items.DestinationId);
                }

                foreach (var items in propertyIds)
                {
                    foreach (var item in destinations)
                    {
                        if (item.Id == items)
                        {
                            destinationsForPlants.Add(destinations.FirstOrDefault(p => p.Id == items));


                        }
                    }
                }
                foreach (var i in destinationsForPlants)
                {
                    propertyNames.Add(i.Name);
                    propertyNames.Add(", ");
                }

                propertyNames = propertyNames.Take(propertyNames.Count - 1).ToList();
            }

            return propertyNames;
        }
        public List<string> GetGrowingSeaznosNames(int id)
        {
            var plantGrowingSeazons = new List<PlantGrowingSeazonsVm>();
            plantGrowingSeazons = _plantRepo.GetPlantGrowingSeazons(id).ProjectTo<PlantGrowingSeazonsVm>(_mapper.ConfigurationProvider).ToList();
            var growingSeazons = _plantRepo.GetGrowingSeazons().ProjectTo<GrowingSeazonVm>(_mapper.ConfigurationProvider).ToList();

            var propertyNames = new List<string>();

            if (plantGrowingSeazons != null)
            {
                var growingSeaznosForPlant = new List<GrowingSeazonVm>();
                var propertyIds = new List<int>();

                foreach (var items in plantGrowingSeazons)
                {
                    propertyIds.Add(items.GrowingSeazonId);
                }

                foreach (var items in propertyIds)
                {
                    foreach (var item in growingSeazons)
                    {
                        if (item.Id == items)
                        {
                            growingSeaznosForPlant.Add(growingSeazons.FirstOrDefault(p => p.Id == items));

                        }
                    }
                }
                foreach (var i in growingSeaznosForPlant)
                {
                    propertyNames.Add(i.Name);
                    propertyNames.Add(", ");
                }

                propertyNames = propertyNames.Take(propertyNames.Count - 1).ToList();
            }

            return propertyNames;
        }
        public List<PlantGroupsVm> GetPlantGroups(int? typeId)
        {
            var groups = _plantRepo.GetAllGroups().Where(e => e.PlantTypeId == typeId).ProjectTo<PlantGroupsVm>(_mapper.ConfigurationProvider).ToList();

            return groups;
        }

        public List<PlantTypesVm> GetPlantTypes()
        {
            var types = _plantRepo.GetAllTypes().OrderBy(p=>p.Id).ProjectTo<PlantTypesVm>(_mapper.ConfigurationProvider).ToList();

            return types;
        }

        public List<PlantSectionsVm> GetPlantSections(int? groupId)
        {
            var sections = _plantRepo.GetAllSections().Where(e => e.PlantGroupId == groupId).ProjectTo<PlantSectionsVm>(_mapper.ConfigurationProvider).ToList();

            return sections;
        }

        public List<GrowthTypeVm> GetGrowthTypes(int typeId, int groupId, int? sectionId)
        {
            List<GrowthTypeVm> growthTyes = new List<GrowthTypeVm>();

            if (typeId == 1)
            {
                growthTyes = _plantRepo.GetGrowthTypes().Where(e => e.PlantTypeId == typeId && e.PlantGroupId == groupId && e.PlantSectionId == sectionId).OrderBy(e=>e.PlantTypeId).ProjectTo<GrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
            }
            else if (typeId == 2 || typeId == 3)
            {
                growthTyes = _plantRepo.GetGrowthTypes().Where(e => e.PlantTypeId == typeId).OrderBy(e => e.PlantTypeId).ProjectTo<GrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
            }

            return growthTyes;
        }

        public List<DestinationsVm> GetDestinations()
        {
            List<DestinationsVm> destinationsList = new List<DestinationsVm>();

            destinationsList = _plantRepo.GetDestinations().OrderBy(p => p.Id).ProjectTo<DestinationsVm>(_mapper.ConfigurationProvider).ToList();

            return destinationsList;
        }

        public List<ColorsVm> GetColors()
        {
            List<ColorsVm> colorsList = new List<ColorsVm>();

            colorsList = _plantRepo.GetColors().OrderBy(p => p.Id).ProjectTo<ColorsVm>(_mapper.ConfigurationProvider).ToList();

            return colorsList;
        }

        public List<GrowingSeazonVm> GetGrowingSeazons()
        {
            List<GrowingSeazonVm> growingSeazonList = new List<GrowingSeazonVm>();

            growingSeazonList = _plantRepo.GetGrowingSeazons().OrderBy(p => p.Id).ProjectTo<GrowingSeazonVm>(_mapper.ConfigurationProvider).ToList();

            return growingSeazonList;
        }

        public List<FruitSizeVm> GetFruitSize(int typeId, int groupId, int? sectionId)
        {
            List<FruitSizeVm> fruitSizeList = new List<FruitSizeVm>();
            if (!sectionId.HasValue)
            {
                fruitSizeList = _plantRepo.GetFruitSizes().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId).OrderBy(p => p.Id).ProjectTo<FruitSizeVm>(_mapper.ConfigurationProvider).ToList();

            }
            if (sectionId.HasValue)
            {
                fruitSizeList = _plantRepo.GetFruitSizes().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId).OrderBy(p => p.Id).ProjectTo<FruitSizeVm>(_mapper.ConfigurationProvider).ToList();
            }

            
            return fruitSizeList;
        }

        public List<FruitTypeVm> GetFruitType(int typeId, int groupId, int? sectionId)
        {
            List<FruitTypeVm> fruitTypeList = new List<FruitTypeVm>();

            if (!sectionId.HasValue)
            {
                fruitTypeList = _plantRepo.GetFruitTypes().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId).OrderBy(p => p.Id).ProjectTo<FruitTypeVm>(_mapper.ConfigurationProvider).ToList();

            }
            if (sectionId.HasValue)
            {
                fruitTypeList = _plantRepo.GetFruitTypes().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId).OrderBy(p => p.Id).ProjectTo<FruitTypeVm>(_mapper.ConfigurationProvider).ToList();
            }

            return fruitTypeList;
        }

        public NewPlantVm GetPlantToEdit(int id)
        {
            
            var plant = _plantRepo.GetPlantById(id);
            var plantVm = _mapper.Map<NewPlantVm>(plant);
            var plantDetails = _plantRepo.GetPlantDetails(id);
            var plantDetailsVm = _mapper.Map<PlantDetailsVm>(plantDetails);
            plantVm.PlantDetails = plantDetailsVm;
            if (plantDetails != null)
            {
                var plantDetailsImages = _plantRepo.GetPlantDetailsImages(plantDetailsVm.Id).ProjectTo<PlantDetailsImagesVm>(_mapper.ConfigurationProvider).ToList();

                if (plantDetailsImages != null)
                {
                    foreach (var image in plantDetailsImages)
                    {
                        plantVm.PlantDetails.PlantDetailsImages.Add(image);
                    }
                }

                var growthTypes = _plantRepo.GetPlantGrowthTypes(plantDetails.Id).ProjectTo<PlantGrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
                if (growthTypes != null)
                {
                    plantDetailsVm.ListGrowthTypes = new ListGrowthTypesVm();
                    plantDetailsVm.ListGrowthTypes.GrowthTypesIds = new int[growthTypes.Count];
                    for (int i = 0; i < growthTypes.Count; i++)
                    {
                        plantDetailsVm.ListGrowthTypes.GrowthTypesIds[i] = growthTypes[i].GrowthTypeId;
                    }
                }

                var growingSeazons = _plantRepo.GetPlantGrowingSeazons(plantDetails.Id).ProjectTo<PlantGrowingSeazonsVm>(_mapper.ConfigurationProvider).ToList();
                if (growingSeazons != null)
                {
                    plantDetailsVm.ListGrowingSeazons = new ListGrowingSeazonsVm();
                    plantDetailsVm.ListGrowingSeazons.GrowingSeaznosIds = new int[growingSeazons.Count];
                    for (int i = 0; i < growingSeazons.Count; i++)
                    {
                        plantDetailsVm.ListGrowingSeazons.GrowingSeaznosIds[i] = growingSeazons[i].GrowingSeazonId;
                    }
                }

                var destinations = _plantRepo.GetPlantDestinations(plantDetails.Id).ProjectTo<PlantDestinationsVm>(_mapper.ConfigurationProvider).ToList();
                if (destinations != null)
                {
                    plantDetailsVm.ListPlantDestinations = new ListPlantDestinationsVm();
                    plantDetailsVm.ListPlantDestinations.DestinationsIds = new int[destinations.Count];
                    for (int i = 0; i < destinations.Count; i++)
                    {
                        plantDetailsVm.ListPlantDestinations.DestinationsIds[i] = destinations[i].DestinationId;
                    }
                }
            }

            return plantVm;
        }

        public void UpdatePlant(NewPlantVm model)
        {
            var getPhotoName = _plantRepo.GetPlantById(model.Id);
            var PhotoName = getPhotoName.Photo;
            
            string direction = null;
            if (model.Photo != null)
            {

                direction = "plantGallery/searchPhoto";
                var fileName = _imageService.UploadImage(model.Photo, model.FullName, direction);
                model.PhotoFileName = fileName;

                string imagePath = "plantGallery/searchPhoto/" + PhotoName;

                _imageService.DeleteImage(imagePath);

            }
            else
            {
                model.PhotoFileName = getPhotoName.Photo;
            }

            if (model.PlantDetails.Images != null)
            {
                if (model.PlantDetails.Images.Count > 0)
                {

                    foreach (var item in model.PlantDetails.Images)
                    {
                        direction = "plantGallery/plantDetailsGallery";
                        string fileName = _imageService.UploadImage(item, model.FullName, direction);
                        _plantRepo.AddPlantDetailsImages(fileName, model.PlantDetails.Id);
                    }
                }
            }

            if (model.PlantDetails.PlantDetailsImages != null)
            {
                if (model.PlantDetails.PlantDetailsImages.Count > 0)
                {
                    foreach (var image in model.PlantDetails.PlantDetailsImages)
                    {
                        if (image.IsChecked == true)
                        {
                            string imagePath = direction + "/" + image.ImageURL;
                            _imageService.DeleteImage(imagePath);
                            _plantRepo.DeleteImageFromGallery(image.Id);
                        }
                    }
                }
            }

            var plant = _mapper.Map<Plant>(model);

            if (model.PlantDetails.ColorId == 0)
                model.PlantDetails.ColorId = null;
            if(model.PlantDetails.FruitSizeId==0)
                model.PlantDetails.FruitSizeId= null;
            if(model.PlantDetails.FruitTypeId==0)
                model.PlantDetails.FruitTypeId= null;

            var plantDetails = _mapper.Map<PlantDetail>(model.PlantDetails);
            _plantRepo.UpdatePlant(plant);
            _plantRepo.UpdatePlantDetails(plantDetails);

            //Update Destinations
            if (model.PlantDetails.ListPlantDestinations != null)        
                UpdatePlantDestinations(model);

            //Update GrowingSeazons
            if (model.PlantDetails.ListGrowingSeazons != null)
                UpdatePlantGrowingSeazons(model);

            //UpdateGrowthTypes
            if(model.PlantDetails.ListGrowthTypes!=null)
            UpdatePlantGrowthTypes(model);
            
        }

        private void UpdatePlantDestinations(NewPlantVm model)
        {
            var plantDestinations = _plantRepo.GetPlantDestinations(model.PlantDetails.Id);

            if (plantDestinations != null)
            {
                _plantRepo.DeletePlantDestinations(model.PlantDetails.Id);
                _plantRepo.AddPlantDestinations(model.PlantDetails.ListPlantDestinations.DestinationsIds, model.PlantDetails.Id);
            }
            else
            {
                _plantRepo.AddPlantDestinations(model.PlantDetails.ListPlantDestinations.DestinationsIds, model.PlantDetails.Id);
            }
        }

        private void UpdatePlantGrowingSeazons(NewPlantVm model)
        {
            var growingS = _plantRepo.GetPlantGrowingSeazons(model.PlantDetails.Id);

            if (growingS != null)
            {
                _plantRepo.DeletePlantGrowingSeazons(model.PlantDetails.Id);
                _plantRepo.AddPlantGrowingSeazons(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, model.PlantDetails.Id);
            }
            else
            {
                _plantRepo.AddPlantGrowingSeazons(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, model.PlantDetails.Id);
            }
        }

        private void UpdatePlantGrowthTypes(NewPlantVm model)
        {
            var plantGrowthTypes= _plantRepo.GetPlantGrowthTypes(model.PlantDetails.Id);

            if (plantGrowthTypes!=null)
            {
                _plantRepo.DeletePlantGrowthTypes(model.PlantDetails.Id);
                _plantRepo.AddPlantGrowthTypes(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, model.PlantDetails.Id);
            }
            else
            {
                _plantRepo.AddPlantGrowthTypes(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, model.PlantDetails.Id);
            }

        }

        public PlantForListVm DeletePlant(int id)
        {
            var getPlantToDelete = _plantRepo.GetPlantById(id);
            var plantVm = _mapper.Map<PlantForListVm>(getPlantToDelete);
            plantVm.isActive = false;
            //var plantVm = _mapper.Map<PlantForListVm>(getPlantToDelete);

            if (getPlantToDelete != null)
            {
                getPlantToDelete.isActive = false;
                //plantVm.isActive = false;
                //var plantToDelete = _mapper.Map<Plant>(plantVm);
                _plantRepo.DeletePlant(getPlantToDelete);
            }
            
            return plantVm;
        }

        public List<SelectListItem> FillPropertyList(List<PlantTypesVm> list, List<ColorsVm> colorList, List<GrowingSeazonVm> seazonList)
        {
            List<SelectListItem> propertyList = new List<SelectListItem>();

            if (list != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in list)
                {
                    propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                }
            }
            if (colorList != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in colorList)
                {
                    propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                }
            }
            if (seazonList != null)
            {
                //propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in seazonList)
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

        public PlantSeedVm FillProperties(int id,string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            var plantSedd = new PlantSeedVm() { PlantId = id,UserId = user.Result.Id};
            return plantSedd;
        }

        public void AddPlantSeed(PlantSeedVm seed)
        {
         
            if (seed != null)
            {
                seed.DateAdded = DateTime.Now;
                var plantSeed = _mapper.Map<PlantSeed>(seed);
                var seedId = _plantRepo.AddPlantSeed(plantSeed);

                //if (seed.ContactDetail != null)
                if(seed.Link!=null)
                {
                    seed.ContactDetail.ContactDetailTypeID = 1;
                    seed.ContactDetail.UserId = seed.UserId;
                    seed.ContactDetail.ContactDetailInformation = seed.Link;
                    var contactDetails = _mapper.Map<ContactDetail>(seed.ContactDetail);
                    var contactId = _plantRepo.AddContactDetail(contactDetails);

                    ContactDetailForSeedVm contactSeed = new ContactDetailForSeedVm();
                    contactSeed.PlantSeedId= seedId;
                    contactSeed.ContactDetailId= contactId;

                    var contactSeddToSave = _mapper.Map<ContactDetailForSeed>(contactSeed);

                    _plantRepo.AddContactDetailForSeed(contactSeddToSave);
                }             
            }
            else
            {
                throw new NullReferenceException(); 
            }
        }

        public void AddPlantSeedling(PlantSeedlingVm seedling)
        {
            if (seedling != null)
            {
                seedling.DateAdded = DateTime.Now;
                var plantSeedling = _mapper.Map<PlantSeedling>(seedling);
                var seedlingId = _plantRepo.AddPlantSeedling(plantSeedling);

                if (seedling.ContactDetail != null)
                {
                    seedling.ContactDetail.ContactDetailTypeID = 1;
                    seedling.ContactDetail.UserId = seedling.UserId;
                    var contactDetails = _mapper.Map<ContactDetail>(seedling.ContactDetail);
                    var contactId = _plantRepo.AddContactDetail(contactDetails);

                    ContactDetailForSeedlingVm contactSeedling =new ContactDetailForSeedlingVm();
                    contactSeedling.PlantSeedlingId = seedlingId;
                    contactSeedling.ContactDetailId = contactId;

                    var contactSeedlingToSave = _mapper.Map<ContactDetailForSeedling>(contactSeedling);
                    _plantRepo.AddContactDetailForSeedling(contactSeedlingToSave);

                }

            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public PlantSeedlingVm FillPropertiesSeedling(int id, string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            var plantSeddling = new PlantSeedlingVm() { PlantId = id, UserId = user.Result.Id };
            return plantSeddling;
        }

        public void AddPlantOpinion(PlantOpinionsVm opinion)
        {
            if (opinion != null)
            {
                opinion.DateAdded = DateTime.Now;
                var plantOpinion = _mapper.Map<PlantOpinion>(opinion);
                _plantRepo.AddPlantOpinion(plantOpinion);
            }
            else { throw new NullReferenceException(); }
        }

        public PlantOpinionsVm FillPropertyOpinion(int id, string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            var plantOpinion = new PlantOpinionsVm() { PlantDetailId = id, UserId = user.Result.Id };
            return plantOpinion;
        }

        public List<PlantSeedVm> FilterSeedsList(List<PlantSeedVm> seeds, List<string> filteredUsersList)
        {
            var filteredSeeds = new List<PlantSeedVm>();
            var seedsList = new List<PlantSeedVm>();

            foreach (var seed in seeds)
            {
                foreach (var items in filteredUsersList)
                {
                    if (seed.UserId == items)
                    {
                        filteredSeeds.Add(seed);
                    }
                }
            }

            if (filteredSeeds.Count > 1)
            {
                for (int i = 0; i < filteredSeeds.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        seedsList.Add(filteredSeeds[i]);
                    }
                    if (filteredSeeds[i] != filteredSeeds[i + 1])
                    {
                        seedsList.Add(filteredSeeds[i + 1]);
                    }
                }
                return seedsList;
            }
            else
            {
                return filteredSeeds;
            }

        }
        public List<PlantSeedlingVm> FilterSeedlingsList(List<PlantSeedlingVm> seedlings, List<string> filteredUsersList)
        {
            var filteredSeedlings = new List<PlantSeedlingVm>();
            var seedlingsList = new List<PlantSeedlingVm>();

            foreach (var seedling in seedlings)
            {
                foreach (var items in filteredUsersList)
                {
                    if (seedling.UserId == items)
                    {
                        filteredSeedlings.Add(seedling);
                    }
                }
            }

            if (filteredSeedlings.Count > 1)
            {
                for (int i = 0; i < filteredSeedlings.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        seedlingsList.Add(filteredSeedlings[i]);
                    }
                    if (filteredSeedlings[i] != filteredSeedlings[i + 1])
                    {
                        seedlingsList.Add(filteredSeedlings[i + 1]);
                    }
                }
                return seedlingsList;
            }
            else
            {
                return filteredSeedlings;
            }
            
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

        public void ActivatePlant(int id)
        {
            var plant = _plantRepo.GetPlantToActivate(id);
            var plantVm = _mapper.Map<NewPlantVm>(plant);

            plantVm.isActive= true;
            plantVm.isNew = false;

            var plantToSave = _mapper.Map<Plant>(plantVm);

            _plantRepo.ActivatePlant(plantToSave);
        }
    }
}
