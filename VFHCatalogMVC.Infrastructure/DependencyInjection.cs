using Microsoft.Extensions.DependencyInjection;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Interface.PlantDetailsRepositories;
using VFHCatalogMVC.Domain.Interface.PlantRepositories;
using VFHCatalogMVC.Infrastructure.Common;
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
            services.AddTransient<IPlantDetailRepository, PlantDetailsRepository>();
            services.AddTransient<IPlantGrowthTypeRepository, PlantGrowthTypesRepository>();
            services.AddTransient<IPlantGrowingSeazonRepository, PlantGrowingSeazonRepository>();
            services.AddTransient<IPlantDestinationRepository, PlantDestinationRepository>();    
            services.AddTransient<IPlantDetailsImagesRepository, PlantDetailsImagesRepository>();
            services.AddTransient<IPlantOpinionRepository, PlantOpinionRepository>();
			services.AddScoped<ICurrentSessionProvider, CurrentSessionProvider>();

            return services;
        }


    }
}
