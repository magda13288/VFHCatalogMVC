using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Application.ViewModels.PrivateUser;

namespace VFHCatalogMVC.Application.Services
{
    public class PrivateUserServie : IPrivateUserService
    {
        private readonly IPrivateUserRepository _userRepo;
        private readonly IMapper _mapper;

        public PrivateUserServie(IPrivateUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public PrivateUserVm GetPrivateUser(int id)
        {
           var user = _userRepo.GetPrivateUser(id);
           var userVm = _mapper.Map<PrivateUserVm>(user);

           return userVm;
        }
    }
}
