using Core;
using Core.Entities;
using Core.Entities.Interfaces;
using Core.Infrastructure.Repositories;
using Core.Infrastructure.Repositories.Interfaces;
using Core.MongoDB;
using Core.Services;
using Core.Services.Interfaces;
using Core.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            services
                .AddServices()
                .AddRepositories()
                .AddMongo()
                .AddMongoRepository<Expense>("expenses")
                .AddMongoRepository<ExpenseTotal>("expensesTotal");

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IExpenseService, ExpenseService>()
                .AddScoped<IExpenseTotalService, ExpenseTotalService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IExpenseRepository, ExpenseRepository>()
                .AddScoped<IExpenseTotalRepository, ExpenseTotalRepository>();

            return services;
        }

        private static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<ExpenseRecorrence>(BsonType.String));
            var pack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register(nameof(CamelCaseElementNameConvention), pack, _ => true);

            services
                .AddSingleton(sp =>
                {
                    var configuration = sp.GetService<IConfiguration>();
                    var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                    var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                    return mongoClient.GetDatabase(serviceSettings.ServiceName);
                });

            return services;
        }

        private static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
        {
            services.AddSingleton<IRepository<T>>(sp =>
            {
                var database = sp.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database, collectionName);
            });

            return services;
        }

    }
}
