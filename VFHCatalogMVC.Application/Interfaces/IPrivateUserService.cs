using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.PrivateUser;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IPrivateUserService
    {
        PrivateUserVm GetPrivateUser(string id);
    }
}
