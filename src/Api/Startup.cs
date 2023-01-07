using Api.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMemoryCache();

            services.AddHttpContextAccessor();

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwaggerSetup();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }

    }
}
