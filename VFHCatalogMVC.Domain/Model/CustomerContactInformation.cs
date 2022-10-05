using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class CustomerContactInformation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Possition { get; set; }

        //jeden do jednego - jedne customer ma jendego przedstawiciela i informacje o nim są w tej tabeli 
        public int CustomerRef { get; set; } // referencja do customera
        public Customer Customer { get; set; }

    }
}
