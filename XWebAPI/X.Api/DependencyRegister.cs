using Microsoft.Extensions.DependencyInjection;
using X.BusinessService;
using X.BusinessService.Interfaces;
using X.BusinessService.Mapper;
using X.BusinessService.Utilities;

namespace X.Api
{
    public static class DependencyRegister
    {
        public static void RegisterInternalServiceDependencies(IServiceCollection services)
        {
            // AutoMapper
            var mapper = AutoMapperConfig.Configure();
            services.AddSingleton(mapper);

            // Register services
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();

        }

        public static void RegisterExternalServiceDependencies(IServiceCollection services)
        {
            // Register Utilities
            services.AddScoped<IHttpClientService, HttpClientService>();
        }
    }
}
