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
            services.AddTransient<IPlantHelperService, PlantHelperService>();
            services.AddTransient<IPlantDetailsService, PlantDetailsService>();
            services.AddTransient<IUserPlantService, UserPlantService>();
            services.AddTransient<IUserContactDataService, UserContactDataService>();
            services.AddTransient<IImageService, ImageService>();           
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient(typeof(IPlantItemProcessor<>), typeof(PlantItemProcessor<>));
            services.AddTransient<IListService, ListService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
