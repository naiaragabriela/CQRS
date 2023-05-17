using CQRS.Domain.Commands.CreatePerson;
using CQRS.Domain.Contracts;
using CQRS.Domain.Queries.ListPerson;
using CQRS.Repository;
using CQRS.Repository.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MongoDB.Driver;

namespace CQRS.Api
{
    public static class Bootstrap
    {
        public static void AddInjections( this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddCommands();
            services.AddQueries();
            services.AddMappers();
            services.AddValidators();
            
        }

        private static void AddMappers(this IServiceCollection services) => 
            services.AddAutoMapper(
            typeof(CreatePersonCommandProfile),
            typeof(ListPersonQueryProfile));

        private static void AddCommands( this IServiceCollection services)
        {
            services.AddTransient<CreatePersonCommandHandler>();
        }

        private static void AddQueries(this IServiceCollection services) 
        {
            services.AddTransient<ListPersonQueryHandler>();
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
        }

        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoSettings = configuration.GetSection(nameof(MongoRepositorySettings));
            var clientSettings = MongoClientSettings.FromConnectionString(mongoSettings.Get<MongoRepositorySettings>().ConnectionString);

            services.Configure<MongoRepositorySettings>(mongoSettings);
            services.AddSingleton<IMongoClient>(new MongoClient(clientSettings));
            services.AddSingleton<IPersonRepository, PersonRepository>();
        }
    }
}
