using AutoMapper;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RentMyTools.Api.Infrastructure.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            Initialize();
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(serviceProvider =>
                new Mapper(serviceProvider.GetRequiredService<IConfigurationProvider>(),
                    serviceProvider.GetService));
        }

        public static void Initialize()
        {
            var profileType = typeof(Profile);
            var profiles = typeof(AutoMapperConfiguration).Assembly
                .GetExportedTypes()
                .Where(x => profileType.IsAssignableFrom(x)
                            && !x.IsAbstract)
                .ToArray();

            Mapper.Initialize(config =>
            {
                foreach (var profile in profiles)
                {
                    config.AddProfile(profile);
                }
            });
        }
    }
}
