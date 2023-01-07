using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace Api.Configurations
{
    [ExcludeFromCodeCoverage]
    internal static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Gasteei Expanses",
                    Description = "Gasteei Expanses",
                    Contact = new OpenApiContact { Name = "Workaholic", Email = "renanortega82@gmail.com" }
                });

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}
