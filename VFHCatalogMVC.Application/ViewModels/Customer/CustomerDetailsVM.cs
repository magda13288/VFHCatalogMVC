using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Application.ViewModels.Customer
{
    public class CustomerDetailsVM
    {//wyswietla tylko aktywnych uzytkownikow. Serwis ma zwrocic do listy tylko aktywnych 
        public int Id { get; set; }
        public string Name { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string CEOFullName { get; set; } // na etapie serwisu dwie wlasciwoscie CEO beda łączone w jedną
        public string FirstLineOfContactInformation { get; set; } //nie towrzy osobnego vm, ponieważ w tym widoku chce tylko wyswietlic imie,nazwisko kotre beda sklejone w stringu
        public byte[] LogoPic { get; set; }
        public List<AddressForListVM> Address { get; set; } // będzie mało adresów wiec nie trzeba tworzyc oddzielnego vm tak jak z customerem, żeby odpwoeidnio paginacje ustawić
        public List<ContactDetailVm> Emails { get; set; }
        public List<ContactDetailVm> PhoneNumbers { get; set; }


    }
}
