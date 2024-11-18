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
            var list = FillCityList(cities);
            return list;
        }
        public List<SelectListItem> Countries()
        {
            var countries = GetCountries();
            var list = FillCountryList(countries);
            return list;
        }
        public List<SelectListItem> Regions(int countryId)
        {
            var regions = GetRegions(countryId);
            var list = FillRegionList(regions);
            return list;
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
        public void AddAddress(AddressVm address)
        {
            var addressToSave = _mapper.Map<Address>(address);
            _userRepo.AddAddress(addressToSave);
        }
    }
}
