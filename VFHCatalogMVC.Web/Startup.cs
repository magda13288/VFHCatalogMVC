using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VFHCatalogMVC.Application;
using VFHCatalogMVC.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using FluentValidation.Internal;
using FluentValidation.Resources;
using FluentValidation.Results;
using VFHCatalogMVC.Application.ViewModels.Plant;
using static VFHCatalogMVC.Application.ViewModels.Plant.NewPlantVm;
using static VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails.PlantDetailsVm;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using static VFHCatalogMVC.Application.ViewModels.Adresses.AddressVm;
using System.Reflection;
using static VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds.PlantSeedVm;
using static VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings.PlantSeedlingVm;
using static VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails.PlantOpinionsVm;
using VFHCatalogMVC.Application.ViewModels.User;
using static VFHCatalogMVC.Application.ViewModels.User.UserSeedVm;
using static VFHCatalogMVC.Application.ViewModels.User.UserSeedlingVm;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Infrastructure.Common;

namespace VFHCatalogMVC.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options => {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));
   
                /* options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)*/
                
              
            });
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Context>();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true, //waliduje cykl ¿ycia tokenu, za kazdym razem jak bedzie tworzony token to bede wskazywac na jak dlugo bedzie on wystarczal
            //        ValidateIssuerSigningKey = true, //podpis elektroniczny 
            //        //sprawdza czy klient pochodzi z odpowiedniej strony 
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        ValidAudience = Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //tworzy podpis elektroniczny klienta
            //    };

            //});

            services.AddApplication();
            services.AddInfrastructure();
            services.AddHttpContextAccessor();


            services.AddControllersWithViews().AddFluentValidation(options =>
            {
                // Validate child properties and root collection elements
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;
                options.AutomaticValidationEnabled = true;
                // Automatic registration of validators in assembly
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            services.AddTransient<IValidator<NewPlantVm>, NewPlantValidation>();
            services.AddTransient<IValidator<PlantDetailsVm>, PlantDetailsValidation>();
            services.AddTransient<IValidator<AddressVm>, AddressValidation>();
            services.AddTransient<IValidator<PlantSeedVm>,PlantSeedValidation>();
            services.AddTransient<IValidator<PlantSeedlingVm>, PlantSeedlingValidation>();
            services.AddTransient<IValidator<PlantOpinionsVm>, PlantOpinionValidation>();
            services.AddTransient<IValidator<UserSeedVm>, UserSeedValidation>();
            services.AddTransient<IValidator<UserSeedlingVm>, UserSeedlingValidation>();
            services.AddScoped<ICurrentSessionProvider, CurrentSessionProvider>();

            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            {
               
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;

            });

            services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
