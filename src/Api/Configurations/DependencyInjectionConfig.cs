using Core.Interfaces;
using Core.Services;
using Core.Services.Interfaces;
using Infrastructure.Repositories;

namespace Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services
                .AddServices()
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IExpanseService, ExpanseService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IExpanseRepository, ExpanseRepository>();

            return services;
        }
    }
}
