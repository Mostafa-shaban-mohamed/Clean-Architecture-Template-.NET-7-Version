using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wuzzfny.Application;
using Wuzzfny.Application.Users;
using Wuzzfny.Domain.Interfaces;
using Wuzzfny.Infrastructure.Context;
using Wuzzfny.Infrastructure.Repositories;

namespace Wuzzfny.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //Add Repository
            services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            services.AddScoped<RepositoryWrapper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Services
            services.AddScoped<IUserService, UserService>();

            // AutoMapper Configuration
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
