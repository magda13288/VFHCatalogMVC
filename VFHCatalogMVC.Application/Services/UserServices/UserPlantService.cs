using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPOI.OpenXmlFormats.Dml;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.Constants;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Application.ViewModels.User;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;


namespace VFHCatalogMVC.Application.Services.UserServices
{
    public class UserPlantService : IUserPlantService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPlantRepository _plantRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserPlantService(
            IUserRepository userRepo,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IPlantRepository plantRepository
            )
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _userManager = userManager;
            _plantRepo = plantRepository;
        }


        public async Task<UserSeedsForListVm> GetUserSeedsAsync(
            int pageSize,
            int? pageNo,
            string searchString,
            int typeId,
            int groupId,
            int? sectionId,
            string userName)
        {

            return await GetUserPlantItemsAsync<UserSeedVm, UserSeedsForListVm, PlantSeed>(
               pageSize,
               pageNo,
               searchString,
               typeId,
               groupId,
               sectionId,
               userName,
               _userRepo.GetUserPlantEntity<PlantSeed>
               //_userRepo.GetContactDetailForSeed
           );
        }
        public async Task<UserSeedlingsForListVm> GetUserSeedlingsAsync(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName)
        {

            return await GetUserPlantItemsAsync<UserSeedlingVm, UserSeedlingsForListVm, PlantSeedling>(
               pageSize,
               pageNo,
               searchString,
               typeId,
               groupId,
               sectionId,
               userName,
               _userRepo.GetUserPlantEntity<PlantSeedling>
               //_userRepo.GetContactDetailForSeedAsync
           );
        }

        public async Task<List<string>> FilterUsersAsync(int countryId, int regionId, int cityId, List<PlantItemVm> items)
        {
            if (countryId == 0)
                return new List<string>(); 

            var usersList = new List<string>();

            foreach (var item in items)
            {
                var address = await _userRepo.GetAddressInfoAsync(item.UserId);

                if (address.CountryId != countryId)
                    continue; // If countryId isn't match,skip

                if (regionId != 0 && address.RegionId != regionId)
                    continue; 

                if (cityId != 0 && address.CityId != cityId)
                    continue; 

                usersList.Add(item.UserId);
            }

            return usersList;
        }      
        public async Task UpdateSeedlingAsync(UserSeedlingVm seedling)
        {
           await UpdateEntityAsync<PlantSeedling,UserSeedlingVm, ContactDetailForSeedlingVm, ContactDetailForSeedling>(
                seedling,
                id => _userRepo.GetContactDetailForSeedlingAsync(id),
                id => new ContactDetailForSeedlingVm { ContactDetailId = id, PlantSeedlingId = seedling.Id },
                vm => _mapper.Map<ContactDetailForSeedling>(vm)
            );
        }

        public async Task UpdateSeedAsync(UserSeedVm seed)
        {
            await UpdateEntityAsync<PlantSeed, UserSeedVm, ContactDetailForSeedVm, ContactDetailForSeed>(
                seed,
                id => _userRepo.GetContactDetailForSeedAsync(id),
                id => new ContactDetailForSeedVm { ContactDetailId = id, PlantSeedId = seed.Id },
                vm => _mapper.Map<ContactDetailForSeed>(vm)
            );
        }

        public async Task DeleteItemAsync<T>(int id)
            where T : BaseEntityProperty
        {
            var item = await _userRepo.GetUserEntityAsync<T>(id);
            await _userRepo.DeleteEntityAsync<T>(item);
        }

        public async Task<UserSeedVm> GetUserSeedToEditAsync(int id)
        {
            return await GetUserPlantSeeOrSeedlingToEditAsync<UserSeedVm, PlantSeed> (id, _userRepo.GetContactDetailForSeedAsync);

        }
        public async Task<UserSeedlingVm> GetUserSeedlingToEditAsync(int id)
        {

            return await GetUserPlantSeeOrSeedlingToEditAsync<UserSeedlingVm, PlantSeedling>(id, _userRepo.GetContactDetailForSeedlingAsync);
        }

        public async Task<ContactDetail> GetContactDetailAsync(int? id)
        {
            var contactDetails = await _userRepo.GetContactDetailAsync(id);
            return contactDetails;
        }

        public async Task<int?> GetContactDetailForPlantAsync(int id, Func<int,Task<int?>> getContact)
        {
            return await getContact(id);
           
        }
        public async Task AddNewUserPlantAsync(int plantId, string userId)
        {
            var newPlant = new NewUserPlantVm { PlantId = plantId, UserId = userId };
            var newUserPlant = _mapper.Map<NewUserPlant>(newPlant);

            await _userRepo.AddEntityAsync<NewUserPlant>(newUserPlant);

        }
        private async Task<List<NewUserPlantVm>> GetPlantsByUserRoleAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userRole = await _userManager.GetRolesAsync(user);

            var plants = new List<NewUserPlantVm>();

            if (userRole.Count > 0 && userRole.Contains(UserRoles.ADMIN) == true)
            {
                plants = _userRepo.GetEntity<NewUserPlant>().ProjectTo<NewUserPlantVm>(_mapper.ConfigurationProvider).ToList();

                //foreach (var plant in plants)
                //{
                //    var userInfo = await _userManager.FindByIdAsync(plant.UserId);
                //    plant.UserName = userInfo.AccountName;

                // Creating collection of async tasks 
                var tasks = plants.Select(async plant =>
                {
                    var userInfo = await _userManager.FindByIdAsync(plant.UserId);
                    plant.UserName = userInfo.AccountName;
                });

                // Waiting for ending of all tasks
                await Task.WhenAll(tasks);
            }
            else
            {
                plants = _userRepo.GetUserPlantEntity<NewUserPlant>(user.Id).ProjectTo<NewUserPlantVm>(_mapper.ConfigurationProvider).ToList();
            }

            return plants;
        }

        private List<NewUserPlantVm> FilterPlants(List<NewUserPlantVm> plants, int typeId, int groupId, int? sectionId, bool viewAll)
        {
            if(viewAll)
            return plants;

            return plants.Where(plant => MatchPlantCriteria(plant, typeId, groupId, sectionId)).ToList();
        }
        private bool MatchPlantCriteria(NewUserPlantVm plant, int typeId, int groupId, int? sectionId)
        {
                     
            if (typeId != 0 && plant.PlantForList.TypeId != typeId)
            {
                return false;
            }

            if (groupId != 0 && plant.PlantForList.GroupId != groupId)
            {
                return false;
            }

            if (sectionId.HasValue && sectionId != 0 && plant.PlantForList.SectionId != sectionId)
            {
                return false;
            }

            return true;

        }
        public async Task<NewUserPlantsForListVm> GetNewUserPlantsAsync(int pageSize, int? pageNo, int typeId, int groupId, int? sectionId, bool viewAll, string userName)
        {
            var plants = await GetPlantsByUserRoleAsync(userName);

            //foreach (var plant in plants)
            //{
            //    var details = _plantRepo.GetPlantByIdAsync(plant.PlantId);
            //    plant.PlantForList = _mapper.Map<PlantForListVm>(details);
            //}

            var tasks = plants.Select(async plant =>
            {
                var details = await _plantRepo.GetPlantByIdAsync(plant.PlantId);
                plant.PlantForList = _mapper.Map<PlantForListVm>(details);
            });

            await Task.WhenAll(tasks);

            var filteredPlants = FilterPlants(plants,typeId,groupId,sectionId,viewAll);
            var plantsToShow = Paginate(filteredPlants,pageSize,pageNo);

            var newUserPlantsForList = new NewUserPlantsForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                NewUserPlants = plantsToShow,
                Count = plants.Count,
                ViewAll = viewAll,
            };

            return newUserPlantsForList;
        }
        private async Task<List<TVm>> FilterItemsAsync<TVm>(List<TVm> items, string searchString, int typeId, int groupId, int? sectionId)
          where TVm : PlantItemVm
        {
            var tasks = items.Select(async item =>
            {
                var plant = await _plantRepo.GetPlantByIdAsync(item.PlantId);
                return new { Plant = plant, Item = item };
            });

            var results = await Task.WhenAll(tasks);

            return results
                .Where(x => MatchesCriteria(x.Plant, searchString, typeId, groupId, sectionId))
                .Select(x => GetEntityList<TVm>(x.Plant, x.Item))
                .ToList();

        }
        private bool MatchesCriteria(Plant plant, string searchString, int typeId, int groupId, int? sectionId)
        {
            //Separation of Concerns (SoC)
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                return plant.FullName.StartsWith(searchString);
            }

            if (typeId != 0 && plant.PlantTypeId != typeId)
            {
                return false;
            }

            if (groupId != 0 && plant.PlantGroupId != groupId)
            {
                return false;
            }

            if (sectionId.HasValue && sectionId != 0 && plant.PlantSectionId != sectionId)
            {
                return false;
            }

            return true;
        }
        private async Task<TListVm> GetUserPlantItemsAsync<TVm, TListVm, T>(
            int pageSize,
            int? pageNo,
            string searchString,
            int typeId,
            int groupId,
            int? sectionId,
            string userName,
            Func<string, IQueryable<T>> getItems
            //Func<int, Task<int?>> getContactDetailId
            )
            where TVm : PlantItemVm
            where TListVm : UserPlantItemListVm<TVm>, new()

        {
            var user = await _userManager.FindByNameAsync(userName);
            var items = getItems(user.Id).ProjectTo<TVm>(_mapper.ConfigurationProvider).ToList();

            if (items == null) return null;

            var userRole = await _userManager.IsInRoleAsync(user, UserRoles.COMPANY);

            if (userRole is true)
            {
               await PopulateItemPropertiesAsync(items);
            }

            var filteredItems = await FilterItemsAsync(items, searchString, typeId, groupId, sectionId);
            var paginatedItems = Paginate(filteredItems, pageSize, pageNo);

            return new TListVm
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                PlantItems = paginatedItems,
                SearchString = searchString,
                Count = items.Count
            };

        }      
        private async Task<List<TVm>> PopulateItemPropertiesAsync<TVm>(List<TVm> entity) where TVm : PlantItemVm
        {

            foreach (var item in entity)
            {
                item.Date = item.DateAdded.ToShortDateString();

                var contactId = await _userRepo.GetContactDetailForSeedAsync(item.Id);

                if (contactId != null)
                {
                    var contactDetails = await _userRepo.GetContactDetailAsync(contactId);
                    var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                    item.ContactDetail = new ContactDetailVm();
                    item.ContactDetail = contactDetailsVm;
                }
                else
                {
                    item.ContactDetail = new ContactDetailVm();
                    item.ContactDetail.ContactDetailInformation = string.Empty;
                }
            }

            return entity;
        }

        private List<T> Paginate<T>(IEnumerable<T> items, int pageSize, int? pageNo)
        {
            if (!pageNo.HasValue || pageNo <= 0)
            {
                pageNo = 1; // default first page
            }

            return items.Skip(pageSize * (pageNo.Value - 1)).Take(pageSize).ToList();
        }
        private T GetEntityList<T>(Plant plant, T item)
           where T : PlantItemVm
        {
            item.PlantForList = new PlantForListVm()
            {
                FullName = plant.FullName,
                Photo = plant.Photo,
            };

            item.Date = item.DateAdded.ToShortDateString();

            return item;

        }
        private async Task UpdateEntityAsync<T, TVm, TDetailForEntityVm, TDetailForEntity>(
               TVm entityVm,
               Func<int, Task<int?>> getExistingDetail,
               Func<int, TDetailForEntityVm> createDetailForEntityVm,
               Func<TDetailForEntityVm, TDetailForEntity> mapDetailForEntity)
               where T : BasePlantSeedSeedlingProperty
               where TVm : PlantItemVm
               where TDetailForEntityVm : class
               where TDetailForEntity : class
        {
            if (entityVm != null)
            {
                var entityToEdit = _mapper.Map<T>(entityVm);
                await _userRepo.EditEntityAsync<T>(entityToEdit);

                if (entityVm.ContactDetail != null)
                {
                    var existingDetail = await getExistingDetail(entityVm.Id);
                    if (existingDetail != null)
                    {
                        var contact = await _userRepo.GetContactDetailAsync(existingDetail);
                        entityVm.ContactDetail.Id = contact.Id;
                        entityVm.ContactDetail.ContactDetailTypeID = contact.ContactDetailTypeID;
                        entityVm.ContactDetail.UserId = contact.UserId;
                        var contactDetails = _mapper.Map<ContactDetail>(entityVm.ContactDetail);
                        await _userRepo.EditContactDetailsAsync(contactDetails);
                    }
                    else
                    {
                        var contact = new ContactDetailVm()
                        {
                            ContactDetailTypeID = 1,
                            UserId = entityVm.UserId,
                            ContactDetailInformation = entityVm.ContactDetail.ContactDetailInformation
                        };

                        var contactDetails = _mapper.Map<ContactDetail>(contact);
                        var id = await _plantRepo.AddContactDetailAsync(contactDetails);

                        var contactDetailsForEntityVm = createDetailForEntityVm(id);
                        var contactDetailsForEntity = mapDetailForEntity(contactDetailsForEntityVm);
                        await _plantRepo.AddContactDetailsEntityAsync<TDetailForEntity>(contactDetailsForEntity);
                    }
                }
            }
        }
        private async Task<TVm> GetUserPlantSeeOrSeedlingToEditAsync<TVm, TPlant>
            (int id,
             Func<int, Task<int?>> getContactDetailId
            )
            where TVm : PlantItemVm
            where TPlant : BasePlantSeedSeedlingProperty
        {
            var item = await _userRepo.GetUserEntityAsync<TPlant>(id);
            var userItem = _mapper.Map<TVm>(item);

            var user = await _userManager.FindByIdAsync(userItem.UserId);
            var userRole = await _userManager.IsInRoleAsync(user, UserRoles.COMPANY);


            if (userRole is true)
            {
                var itemContactDetails = await getContactDetailId(item.Id);
                if (itemContactDetails != 0)
                {
                    var contactDetails = await _userRepo.GetContactDetailAsync(itemContactDetails);
                    var contacDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                    userItem.ContactDetail = contacDetailsVm;

                }
            }

            return userItem;
        }

    }
}
