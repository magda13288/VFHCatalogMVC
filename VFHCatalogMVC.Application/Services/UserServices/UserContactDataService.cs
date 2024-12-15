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
using VFHCatalogMVC.Application.ViewModels.Plant.Common;


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
        public List<SelectListItem> Cities(int regionId)
        {
            var cities = GetCities(regionId);
            var list = FillList(cities);
            return list;
        }
        public List<SelectListItem> Countries()
        {
            var countries = GetCountries();
            var list = FillList(countries);
            return list;
        }
        public List<SelectListItem> Regions(int countryId)
        {
            var regions = GetRegions(countryId);
            var list = FillList(regions);
            return list;
        }
        public List<CityVm> GetCities(int regionId)
        {
            var cities = _userRepo.GetCities(regionId).ProjectTo<CityVm>(_mapper.ConfigurationProvider).ToList();
            return cities;
        }

        public List<CountryVm> GetCountries()
        {
            var countries = _userRepo.GetEntity<Country>().ProjectTo<CountryVm>(_mapper.ConfigurationProvider).ToList();
            return countries;
        }

        public List<RegionVm> GetRegions(int countryId)
        {
            var regions = _userRepo.GetRegions(countryId).ProjectTo<RegionVm>(_mapper.ConfigurationProvider).ToList();
            return regions;
        }

        public List<SelectListItem> FillList<TVm>(List<TVm> items)
            where TVm: SelectListItemVm
        {
            List<SelectListItem> propertyList = new List<SelectListItem>();

            if (items != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in items)
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
