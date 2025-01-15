using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using AutoMapper.QueryableExtensions;
using System.Linq;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Domain.Model;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.Interfaces;


namespace VFHCatalogMVC.Application.Services.UserServices
{
    public class UserContactDataService : IUserContactDataService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IListService _listService;

        public UserContactDataService(IMapper mapper, IUserRepository userRepository, IListService listService)
        {
            _mapper = mapper;
            _userRepo = userRepository;
            _listService = listService;

        }
        public List<SelectListItem> Cities(int regionId)
        {
            var cities = _userRepo.GetAllEntities<City>().Where(p=>p.RegionId == regionId).ProjectTo<CityVm>(_mapper.ConfigurationProvider).ToList();
            return _listService.GetSelectListItem(cities);
        }
        public List<SelectListItem> Countries()
        {
            var countries = _userRepo.GetAllEntities<Country>().OrderBy(p=>p.Id).ProjectTo<CountryVm>(_mapper.ConfigurationProvider).ToList();
            return _listService.GetSelectListItem(countries);
        }
        public List<SelectListItem> Regions(int countryId)
        {
            var regions = _userRepo.GetAllEntities<Region>().Where(p=>p.CountryId == countryId).ProjectTo<RegionVm>(_mapper.ConfigurationProvider).ToList();
            return _listService.GetSelectListItem(regions);
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
