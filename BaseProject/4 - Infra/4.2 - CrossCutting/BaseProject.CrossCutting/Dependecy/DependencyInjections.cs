using AutoMapper;
using BaseProject.Data.Context;
using BaseProject.Data.Interface;
using BaseProject.Data.Repository;
using BaseProject.Domain.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.CrossCutting.Dependecy
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DataBase");
            service.AddDbContext<BaseProjectContext>(x => x.UseSqlServer(connectionString));

            return service;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            return services;
        }
    }
}
