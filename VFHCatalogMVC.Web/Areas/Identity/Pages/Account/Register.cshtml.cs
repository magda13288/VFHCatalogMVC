using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

using VFHCatalogMVC.Application.ViewModels.Adresses;
using Microsoft.AspNetCore.Mvc.Rendering;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;

namespace VFHCatalogMVC.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserContactDataService _userContactDataService;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, IUserContactDataService userContactDataService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _userContactDataService = userContactDataService;
            
        }

        [BindProperty/*(SupportsGet = true)*/]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
     
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = /*"Password"*/"Hasło")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = /*"Confirm password"*/ "Potwierdź hasło")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

            public string ConfirmPassword { get; set; }
            public bool IsPrivateUser { get; set; }
            public bool IsCompany { get; set; }

            public int CountryId { get; set; }
            public int RegionId { get; set; }
            public int CityId { get; set; }
            public string AccountName { get; set; }
            public string CompanyName { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var countries =  _userContactDataService.GetCountries();
            var countriesList = _userContactDataService.FillList(countries);
            ViewData["Country"] = countriesList;     
        }   
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                var result = new IdentityResult();
                var roleResult = new IdentityResult();
                var address = new AddressVm();

                if (Input.IsPrivateUser == true)
                {
                    user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, AccountName = Input.AccountName, isActive = true };
                    result = await _userManager.CreateAsync(user, Input.Password);
                    roleResult = await _userManager.AddToRoleAsync(user, "PRIVATE_USER");
                    address = new AddressVm() { CountryId = Input.CountryId, RegionId = Input.RegionId, CityId = Input.CityId, UserId = user.Id };
                    _userContactDataService.AddAddress(address);
                }

                if (Input.IsCompany == true)
                {
                    user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, CompanyName = Input.CompanyName, isActive = true };
                    result = await _userManager.CreateAsync(user, Input.Password);
                    roleResult = await _userManager.AddToRoleAsync(user, "Company");
                    address = new AddressVm() { CountryId = Input.CountryId, RegionId = Input.RegionId, CityId = Input.CityId, UserId = user.Id };
                    _userContactDataService.AddAddress(address);
                }


                if (result.Succeeded && roleResult.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        }

    }

