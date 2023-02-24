using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using X.Api;
using X.Repository;

namespace X.UnitTest.Infrastructures
{
    public class ServiceProviderTestHelper
    {
        public ServiceProvider CreateServiceProvider(SQLLiteDBConnections sqLiteDBConnections, MockHelper mockHelper)
        {
            var serviceCollection = new ServiceCollection();

            //IConfiguration configuration = serviceCollection!.BuildServiceProvider().GetService<IConfiguration>()!;
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"OpenWeatherMapApiKey", "my-key"}
            });
            var configuration = configBuilder.Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            // Unit Test DI Injenction
            DependencyRegister.RegisterInternalServiceDependencies(serviceCollection);
            RegisterMockedExternalServiceDependencies(serviceCollection, mockHelper);

            serviceCollection.AddDbContext<DataContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlite(sqLiteDBConnections.xMemoryDbConnection);
            });
            serviceCollection!.AddScoped<IDataContext>(s => s.GetService<DataContext>()!);

            return serviceCollection.BuildServiceProvider();
        }

        public void RegisterMockedExternalServiceDependencies(IServiceCollection serviceCollection, MockHelper mockHelper)
        {
            serviceCollection.AddScoped(sp => mockHelper.GetMockHttpClientService().Object);
        }
    }
}
