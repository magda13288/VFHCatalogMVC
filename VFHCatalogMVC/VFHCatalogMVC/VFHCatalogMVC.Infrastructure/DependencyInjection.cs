using Microsoft.Extensions.DependencyInjection;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Infrastructure.Repositories;

namespace VFHCatalogMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPlantRepository, PlantRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            return services;
        }


    }
}
