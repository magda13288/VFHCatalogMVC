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
using static VFHCatalogMVC.Application.ViewModels.Plant.PlantDetailsVm;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using static VFHCatalogMVC.Application.ViewModels.Adresses.AddressVm;

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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options => {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));
   
                /* options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)*/
                
              
            });
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Context>();


            services.AddApplication();
            services.AddInfrastructure();

            services.AddControllersWithViews().AddFluentValidation();
            services.AddTransient<IValidator<NewPlantVm>, NewPlantValidation>();
            services.AddTransient<IValidator<PlantDetailsVm>, PlantDetailsValidation>();
            services.AddTransient<IValidator<AddressVm>, AddressValidation>();
            services.AddControllersWithViews().AddFluentValidation(fv => { });
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

            //services.AddAuthentication().AddGoogle(options =>
            //{
            //    IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
            //    options.ClientId = googleAuthNSection["ClientId"];
            //    options.ClientSecret = googleAuthNSection["ClientSecret"];
            //});
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
