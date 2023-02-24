using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.Api.Controllers;
using X.BusinessService.Interfaces;
using X.Domain;
using X.UnitTest.Infrastructures;
using Xunit;

namespace X.UnitTest.Api
{
    public class LocationControllerTest
    {
        private readonly LocationController _controller;
        private readonly SQLLiteDBConnections _sqLiteDBConnections = new SQLLiteDBConnections();
        private readonly DbContextHelper _dbContextHelper;
        private readonly MockHelper _mockHelper = new MockHelper();
        private readonly IServiceProvider _serviceProvider;
        private readonly DataContextTestHelper _dataContext;

        public LocationControllerTest()
        {
            _dbContextHelper = new DbContextHelper(_sqLiteDBConnections);
            _dbContextHelper.InitializeDB();
            _serviceProvider = new ServiceProviderTestHelper().CreateServiceProvider(_sqLiteDBConnections, _mockHelper);
            _dataContext = new DataContextTestHelper(_dbContextHelper);

            var countryService = _serviceProvider.GetService<ICountryService>();
            var cityService = _serviceProvider.GetService<ICityService>();

            _controller = new LocationController(countryService, cityService);
        }

        [Fact]
        public async Task Get_ReturnsOkObjectResult_WithListOfCountries()
        {
            // Arrange
            _dataContext.Add(new Country()
            {
                Id = 4,
                Name = "Singapore"
            });

            // Act
            var result = await _controller.GetCountry();

            // Assert
            Assert.Equal(4, result.Count());
            Assert.Equal("Australia", result.ElementAt(0).Name);
            Assert.Equal("New Zealand", result.ElementAt(1).Name);
            Assert.Equal("Indonesia", result.ElementAt(2).Name);
            Assert.Equal("Singapore", result.ElementAt(3).Name);
        }

        [Fact]
        public async Task Get_ReturnsOkObjectResult_WithListOfCities()
        {
            // Arrange
            _dataContext.Add(new City()
            {
                Name = "Perth",
                CountryId = 1
            });

            // Act
            var result = await _controller.GetCitiesByCountry(1);

            // Assert
            Assert.Equal(4, result.Count());
            Assert.Equal("Sydney", result.ElementAt(0).Name);
            Assert.Equal("Melbourne", result.ElementAt(1).Name);
            Assert.Equal("Brisbane", result.ElementAt(2).Name);
            Assert.Equal("Perth", result.ElementAt(3).Name);
        }
    }
}
