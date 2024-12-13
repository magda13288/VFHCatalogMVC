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
using System.Text;
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


        public UserSeedsForListVm GetUserSeeds(
            int pageSize,
            int? pageNo,
            string searchString,
            int typeId,
            int groupId,
            int? sectionId,
            string userName)
        {

            return GetUserPlantItems<UserSeedVm, UserSeedsForListVm, PlantSeed>(
               pageSize,
               pageNo,
               searchString,
               typeId,
               groupId,
               sectionId,
               userName,
               _userRepo.GetUserPlantEntity<PlantSeed>,
               _userRepo.GetContactDetailForSeed
           );
        }
        public UserSeedlingsForListVm GetUserSeedlings(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName)
        {

            return GetUserPlantItems<UserSeedlingVm, UserSeedlingsForListVm, PlantSeedling>(
               pageSize,
               pageNo,
               searchString,
               typeId,
               groupId,
               sectionId,
               userName,
               _userRepo.GetUserPlantEntity<PlantSeedling>,
               _userRepo.GetContactDetailForSeed
           );
        }

        public List<string> FilterUsers(int countryId, int regionId, int cityId, List<PlantItemVm> items)
        {
            if (countryId == 0)
                return new List<string>(); 

            var usersList = new List<string>();

            foreach (var item in items)
            {
                var address = _userRepo.GetAddressInfo(item.UserId);

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
        public void UpdateSeedling(UserSeedlingVm seedling)
        {
            UpdateEntity<PlantSeedling,UserSeedlingVm, ContactDetailForSeedlingVm, ContactDetailForSeedling>(
                seedling,
                id => _userRepo.GetContactDetailForSeedling(id),
                id => new ContactDetailForSeedlingVm { ContactDetailId = id, PlantSeedlingId = seedling.Id },
                vm => _mapper.Map<ContactDetailForSeedling>(vm)
            );
        }

        public void UpdateSeed(UserSeedVm seed)
        {
            UpdateEntity<PlantSeed, UserSeedVm, ContactDetailForSeedVm, ContactDetailForSeed>(
                seed,
                id => _userRepo.GetContactDetailForSeed(id),
                id => new ContactDetailForSeedVm { ContactDetailId = id, PlantSeedId = seed.Id },
                vm => _mapper.Map<ContactDetailForSeed>(vm)
            );
        }

        public void DeleteItem<T>(int id)
            where T : BaseEntityProperty
        {
            var item = _userRepo.GetUserEntity<T>(id);
            _userRepo.DeleteEntity<T>(item);
        }

        public UserSeedVm GetUserSeedToEdit(int id)
        {
            return GetUserPlantSeeOrSeedlingToEdit<UserSeedVm, PlantSeed> (id, _userRepo.GetContactDetailForSeed);

        }
        public UserSeedlingVm GetUserSeedlingToEdit(int id)
        {

            return GetUserPlantSeeOrSeedlingToEdit<UserSeedlingVm, PlantSeedling>(id, _userRepo.GetContactDetailForSeedling);
        }

        public ContactDetail GetContactDetail(int? id)
        {
            var contactDetails = _userRepo.GetContactDetail(id);
            return contactDetails;
        }

        private int? GetContactDetailForPlant(int id, Func<int,int?> getContact)
        {
            return getContact(id);
           
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

        public void AddNewUserPlant(int plantId, string userId)
        {
            var newPlant = new NewUserPlantVm { PlantId = plantId, UserId = userId };
            var newUserPlant = _mapper.Map<NewUserPlant>(newPlant);

            _userRepo.AddEntity<NewUserPlant>(newUserPlant);

        }

        public NewUserPlantsForListVm GetNewUserPlants(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, bool viewAll, string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            var userRole = _userManager.GetRolesAsync(user.Result);
            var plants = new List<NewUserPlantVm>();

            if (userRole.Result.Count > 0 && userRole.Result.Contains(UserRoles.ADMIN) == true)
            {
                plants = _userRepo.GetEntity<NewUserPlant>().ProjectTo<NewUserPlantVm>(_mapper.ConfigurationProvider).ToList();

                foreach (var plant in plants)
                {
                    var userInfo = _userManager.FindByIdAsync(plant.UserId);
                    plant.UserName = userInfo.Result.AccountName;
                }
            }
            else
            {
                plants = _userRepo.GetUserPlantEntity<NewUserPlant>(user.Result.Id).ProjectTo<NewUserPlantVm>(_mapper.ConfigurationProvider).ToList();
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
                plantsToShow = plants.Skip(pageSize * ((int)pageNo - 1)).Take(pageSize).ToList();
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
                            plantsToShow = plantsToList.Skip(pageSize * ((int)pageNo - 1)).Take(pageSize).ToList();
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
                            plantsToShow = plantsToList.Skip(pageSize * ((int)pageNo - 1)).Take(pageSize).ToList();
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
                        plantsToShow = plantsToList.Skip(pageSize * ((int)pageNo - 1)).Take(pageSize).ToList();
                    }
                }

            }

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
     private TListVm GetUserPlantItems<TVm, TListVm, T>(
            int pageSize,
            int? pageNo,
            string searchString,
            int typeId,
            int groupId,
            int? sectionId,
            string userName,
            Func<string, IQueryable<T>> getItems,
            Func<int, int?> getContactDetailId
            )
            where TVm : PlantItemVm
            where TListVm : UserPlantItemListVm<TVm>, new()

        {
            var user = _userManager.FindByNameAsync(userName);
            var items = getItems(user.Result.Id).ProjectTo<TVm>(_mapper.ConfigurationProvider).ToList();
            var userRole = _userManager.IsInRoleAsync(user.Result, UserRoles.COMPANY);

            if (items == null) return null;

            if (userRole.Result is true)
            {
                PopulateItemProperties(items);
            }

            var filteredItems = FilterItems(items, searchString, typeId, groupId, sectionId);
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

        private List<TVm> FilterItems<TVm>(List<TVm> items, string searchString, int typeId, int groupId, int? sectionId)
             where TVm : PlantItemVm
        {

            return items
             .Select(item =>
             {
                 var plant = _plantRepo.GetPlantById(item.PlantId);
                 return new { Plant = plant, Item = item }; // create new temporary struct which contain Plant and Item
             })
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
        private List<TVm> PopulateItemProperties<TVm>(List<TVm> entity) where TVm : PlantItemVm
        {

            foreach (var item in entity)
            {
                item.Date = item.DateAdded.ToShortDateString();

                var contactId = _userRepo.GetContactDetailForSeed(item.Id);

                if (contactId != null)
                {
                    var contactDetails = _userRepo.GetContactDetail(contactId);
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
        private void UpdateEntity<T, TVm, TDetailForEntityVm, TDetailForEntity>(
               TVm entityVm,
               Func<int, int?> getExistingDetail,
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
                _userRepo.EditEntity<T>(entityToEdit);

                if (entityVm.ContactDetail != null)
                {
                    var existingDetail = getExistingDetail(entityVm.Id);
                    if (existingDetail != null)
                    {
                        var contact = _userRepo.GetContactDetail(existingDetail);
                        entityVm.ContactDetail.Id = contact.Id;
                        entityVm.ContactDetail.ContactDetailTypeID = contact.ContactDetailTypeID;
                        entityVm.ContactDetail.UserId = contact.UserId;
                        var contactDetails = _mapper.Map<ContactDetail>(entityVm.ContactDetail);
                        _userRepo.EditContactDetails(contactDetails);
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
                        var id = _plantRepo.AddContactDetail(contactDetails);

                        var contactDetailsForEntityVm = createDetailForEntityVm(id);
                        var contactDetailsForEntity = mapDetailForEntity(contactDetailsForEntityVm);
                        _plantRepo.AddContactDetailsEntity(contactDetailsForEntity);
                    }
                }
            }
        }
        private TVm GetUserPlantSeeOrSeedlingToEdit<TVm, TPlant>
            (int id,
             Func<int, int?> getContactDetailId
            )
            where TVm : PlantItemVm
            where TPlant : BasePlantSeedSeedlingProperty
        {
            var item = _userRepo.GetUserEntity<TPlant>(id);
            var userItem = _mapper.Map<TVm>(item);

            var user = _userManager.FindByIdAsync(userItem.UserId);
            var userRole = _userManager.IsInRoleAsync(user.Result, UserRoles.COMPANY);

            if (userRole.Result is true)
            {
                var itemContactDetails = getContactDetailId(item.Id);
                if (itemContactDetails != 0)
                {
                    var contactDetails = _userRepo.GetContactDetail(itemContactDetails);
                    var contacDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                    userItem.ContactDetail = contacDetailsVm;

                }
            }

            return userItem;
        }

    }
}
