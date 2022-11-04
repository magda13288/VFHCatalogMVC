using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(string id);
    }
}
