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
using Microsoft.EntityFrameworkCore;


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
                           .Prepend(new SelectListItem { Text = "-Select-", Value = "0", Disabled=true })
                           .ToList();

            return await Task.FromResult(result);
        }
        public async Task<List<SelectListItem>> Cities(int regionId)
        {
            var citiesEntity = _userRepo.GetEntity<City>().Where(p => p.RegionId == regionId).ProjectTo<CityVm>(_mapper.ConfigurationProvider);

            var cities = await citiesEntity.ToListAsync();

            return await GetSelectListItemAsync(cities);
        }
        public async Task<List<SelectListItem>> Countries()
        {
            var countriesEntity = _userRepo.GetEntity<Country>().OrderBy(p => p.Id).ProjectTo<CountryVm>(_mapper.ConfigurationProvider);

            var countries = await countriesEntity.ToListAsync();
            return await GetSelectListItemAsync(countries);
        }
        public async Task<List<SelectListItem>> Regions(int countryId)
        {
            var regionsEntity = _userRepo.GetEntity<Region>().Where(p => p.CountryId == countryId).ProjectTo<RegionVm>(_mapper.ConfigurationProvider);

            var regions = await regionsEntity.ToListAsync();

            return await GetSelectListItemAsync(regions);
        }     
        public async Task<AddressVm> GetAddressAsync(string userId)
        {
            var address = await _userRepo.GetAddressInfoAsync(userId);
            var addressVm = _mapper.Map<AddressVm>(address);

            return addressVm;
        }
        public async Task AddAddressAsync(AddressVm address)
        {
            var addressToSave = _mapper.Map<Address>(address);
            await _userRepo.AddEntityAsync<Address>(addressToSave);
        }
        public string UserAccountName(ApplicationUser user)
        {
            string userAccountName = null;

            if (user.AccountName != null)
                userAccountName = user.AccountName;
            if (user.CompanyName != null)
                userAccountName = user.CompanyName;
            return userAccountName;
        }
    }
}
