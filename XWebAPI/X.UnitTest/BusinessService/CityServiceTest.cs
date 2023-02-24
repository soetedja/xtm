using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.BusinessService;
using X.Domain;
using X.Repository;
using X.UnitTest.Infrastructures;
using Xunit;

namespace X.UnitTest.BusinessService
{
    public class CityServiceTest
    {
        private readonly CityService _service;
        private readonly SQLLiteDBConnections _sqLiteDBConnections = new SQLLiteDBConnections();
        private readonly DbContextHelper _dbContextHelper;
        private readonly MockHelper _mockHelper = new MockHelper();
        private readonly IServiceProvider _serviceProvider;
        private readonly DataContextTestHelper _dataContext;

        public CityServiceTest()
        {
            _dbContextHelper = new DbContextHelper(_sqLiteDBConnections);
            _dbContextHelper.InitializeDB();
            _serviceProvider = new ServiceProviderTestHelper().CreateServiceProvider(_sqLiteDBConnections, _mockHelper);
            _dataContext = new DataContextTestHelper(_dbContextHelper);

            var dataContext = _serviceProvider.GetService<IDataContext>();
            var mapper = _serviceProvider.GetService<IMapper>();

            _service = new CityService(dataContext, mapper);
        }

        [Fact]
        public async Task Get_ReturnsOkObjectResult_WithListOfCities()
        {
            // Arrange
            _dataContext.AddRange(new List<City>() {
                new City()
                {
                    Name = "Solo",
                    CountryId = 3
                },
                new City()
                {
                    Name = "Surabaya",
                    CountryId = 3
                }
            });

            // Act
            var result = await _service.GetCities(3);

            // Assert
            Assert.Equal(5, result.Count());
            Assert.Equal("Jakarta", result.ElementAt(0).Name);
            Assert.Equal("Yogyakarta", result.ElementAt(1).Name);
            Assert.Equal("Bandung", result.ElementAt(2).Name);
            Assert.Equal("Solo", result.ElementAt(3).Name);
            Assert.Equal("Surabaya", result.ElementAt(4).Name);
        }
    }
}
