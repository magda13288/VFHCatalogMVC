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
                           .Prepend(new SelectListItem { Text = "-Select-", Value = "0" })
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
        //public List<SelectListItem> Cities(int regionId)
        //{
        //    var cities = GetCities(regionId);
        //    var list = FillList(cities);
        //    return list;
        //}
        //public List<SelectListItem> Countries()
        //{
        //    var countries = GetCountries();
        //    var list = FillList(countries);
        //    return list;
        //}
        //public List<SelectListItem> Regions(int countryId)
        //{
        //    var regions = GetRegions(countryId);
        //    var list = FillList(regions);
        //    return list;
        //}
        //public List<CityVm> GetCities(int regionId)
        //{
        //    var cities = _userRepo.GetCities(regionId).ProjectTo<CityVm>(_mapper.ConfigurationProvider).ToList();
        //    return cities;
        //}

        //public List<CountryVm> GetCountries()
        //{
        //    var countries = _userRepo.GetEntity<Country>().ProjectTo<CountryVm>(_mapper.ConfigurationProvider).ToList();
        //    return countries;
        //}

        //public List<RegionVm> GetRegions(int countryId)
        //{
        //    var regions = _userRepo.GetRegions(countryId).ProjectTo<RegionVm>(_mapper.ConfigurationProvider).ToList();
        //    return regions;
        //}

        //public List<SelectListItem> FillList<TVm>(List<TVm> items)
        //    where TVm: SelectListItemVm
        //{
        //    List<SelectListItem> propertyList = new List<SelectListItem>();

        //    if (items != null)
        //    {
        //        propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

        //        foreach (var type in items)
        //        {
        //            propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
        //        }
        //    }
        //    else
        //    {
        //        propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });
        //    }
        //    return propertyList;
        //}
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
