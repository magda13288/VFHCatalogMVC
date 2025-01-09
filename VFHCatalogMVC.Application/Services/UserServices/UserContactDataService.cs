using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using AutoMapper.QueryableExtensions;
using System.Linq;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Domain.Model;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.ViewModels.User.Common;
using System.Reflection.Metadata.Ecma335;


namespace VFHCatalogMVC.Application.Services.UserServices
{
    public class UserContactDataService : IUserContactDataService
    {
        private readonly IUserPlantService _userService;
        private readonly IMessageRepository _messageRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserContactDataService(IUserPlantService userService, IMessageRepository messageRepository, IMapper mapper, IUserRepository userRepository)
        {
            _userService = userService;
            _messageRepo = messageRepository;
            _mapper = mapper;
            _userRepo = userRepository;

        }
        public List<SelectListItem> GetSelectListItem<T>(IEnumerable<T> entity) where T : SelectListItemVm
        {

            if (!entity.Any()) return new List<SelectListItem>();

            return entity
                           .OrderBy(p => p.Id)
                           .Where(e => e.Id != 0 && !string.IsNullOrEmpty(e.Name))
                           .Select(e => new SelectListItem
                           {
                               Value = e.Id.ToString(),
                               Text = e.Name
                           })
                           .Prepend(new SelectListItem { Text = "-Select-", Value = "0", Disabled = true })
                           .ToList();
        }
        public List<SelectListItem> Cities(int regionId)
        {
            var cities = _userRepo.GetAllEntities<City>().Where(p=>p.RegionId == regionId).ProjectTo<CityVm>(_mapper.ConfigurationProvider).ToList();
            return GetSelectListItem(cities);
        }
        public List<SelectListItem> Countries()
        {
            var countries = _userRepo.GetAllEntities<Country>().OrderBy(p=>p.Id).ProjectTo<CountryVm>(_mapper.ConfigurationProvider).ToList();
            return GetSelectListItem(countries);
        }
        public List<SelectListItem> Regions(int countryId)
        {
            var regions = _userRepo.GetAllEntities<Region>().Where(p=>p.CountryId == countryId).ProjectTo<RegionVm>(_mapper.ConfigurationProvider).ToList();
            return GetSelectListItem(regions);
        }       
        public AddressVm GetAddress(string userId)
        {
            var address = _userRepo.GetAddressInfo(userId);
            var addressVm = _mapper.Map<AddressVm>(address);

            return addressVm;
        }
        public void AddAddress(AddressVm address)
        {
            var addressToSave = _mapper.Map<Address>(address);
            _userRepo.AddEntity<Address>(addressToSave);
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
