﻿using Microsoft.Extensions.DependencyInjection;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Infrastructure.Repositories;

namespace VFHCatalogMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPlantRepository, PlantRepository>();
            services.AddTransient<IPrivateUserRepository, PrivateUserRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            return services;
        }


    }
}
