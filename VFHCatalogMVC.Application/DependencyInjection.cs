using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.Services;
using VFHCatalogMVC.Application.Services.PlantServices;
using VFHCatalogMVC.Application.Services.UserServices;

namespace VFHCatalogMVC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IPlantService, PlantService>();
            services.AddTransient<IUserPlantService, UserPlantService>();
            services.AddTransient<IUserContactDataService, UserContactDataService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IPlantHelperService, PlantHelperService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IPlantDetailsSerrvice, PlantDetailsService>();
            services.AddTransient<IPlantHelperService, PlantHelperService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
