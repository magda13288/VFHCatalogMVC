using System.ComponentModel.DataAnnotations;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using System.Web.Mvc;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;

namespace VFHCatalogMVC.Web.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserContactDataService _userHelperService;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<ApplicationUser> userManager,
            ILogger<PersonalDataModel> logger,
            SignInManager<ApplicationUser> signInManager, IUserContactDataService userHelperService)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _userHelperService = userHelperService;
        }
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nazwa konta")]
            public string AccountName { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Imię")]
            public string FirstName { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nazwisko")]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nazwa firmy")]
            public string CompanyName { get; set; }
            public string NIP { get; set; }
            public string REGON { get; set; }
            public string CEOName { get; set; }
            public string CEOLastName { get; set; }
           // public byte[] LogoPic { get; set; }

            //[Required]
            //[Display(Name = "Birth Date")]
            //[DataType(DataType.Date)]
            //public DateTime DOB { get; set; }

            [Phone]
            [Display(Name = "Numer telefonu")]
            public string PhoneNumber { get; set; }
            public AddressVm Address { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var address = await _userHelperService.GetAddressAsync(user.Id);

            Username = userName;

            Input = new InputModel
            {
                AccountName = user.AccountName,
                CompanyName = user.CompanyName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NIP = user.NIP,
                REGON = user.REGON,
                CEOName = user.CEOName,
                CEOLastName = user.CEOLastName,
                PhoneNumber = phoneNumber,
                Address = address
            };
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            var countries = await _userHelperService.Countries();
            ViewData["Country"] = countries;
            var regions = await _userHelperService.Regions(Input.Address.CountryId);
            ViewData["Region"] = regions;
            var cities = await _userHelperService.Cities(Input.Address.RegionId);
            ViewData["City"] = cities;

            if (Input.Address.CountryId != 0)
            {
                ViewData["CountryId"] = Input.Address.CountryId;
            }
            if (Input.Address.RegionId != 0)
            {
                ViewData["RegionId"] = Input.Address.RegionId;
            }
            if (Input.Address.CityId != 0)
            {
                ViewData["CityId"] = Input.Address.CityId;
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            //if (Input.Name != user.Name)
            //{
            //    user.Name = Input.Name;
            //}

            //if (Input.DOB != user.DOB)
            //{
            //    user.DOB = Input.DOB;
            //}

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}