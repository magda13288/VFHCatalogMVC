using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Application.ViewModels.PrivateUser;

namespace VFHCatalogMVC.Application.Services
{
    public class PrivateUserService : IPrivateUserService
    {
        private readonly IPrivateUserRepository _userRepo;
        private readonly IMapper _mapper;

        public PrivateUserService(IPrivateUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public PrivateUserVm GetPrivateUser(string id)
        {
           var user = _userRepo.GetPrivateUser(id);
           var userVm = _mapper.Map<PrivateUserVm>(user);

           return userVm;
        }
    }
}
