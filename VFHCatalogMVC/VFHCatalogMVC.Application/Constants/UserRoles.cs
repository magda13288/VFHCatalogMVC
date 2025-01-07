using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace VFHCatalogMVC.Application.Constants
{
    public class UserRoles
    {
        public const string ADMIN = "Admin";
        public const string PRIVATE_USER = "PrivateUser";
        public const string COMPANY  = "Company";
        public const string ALL_ROLES = ADMIN + "," + PRIVATE_USER + "," + COMPANY;
        public const string PRIVATEUSER_COMPANY = PRIVATE_USER + "," + COMPANY;
    }
}
