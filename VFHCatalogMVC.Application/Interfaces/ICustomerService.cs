using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Customer;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface ICustomerService
    {
        CustomerVm GetCustomer(string id);
    }
}
