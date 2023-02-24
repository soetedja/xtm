using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.Api.Controllers;
using X.BusinessService;
using X.BusinessService.Interfaces;
using X.Domain;
using X.Repository;
using X.UnitTest.Infrastructures;
using Xunit;

namespace X.UnitTest.BusinessService
{
    public class CountryServiceTest
    {
        private readonly CountryService _service;
        private readonly SQLLiteDBConnections _sqLiteDBConnections = new SQLLiteDBConnections();
        private readonly DbContextHelper _dbContextHelper;
        private readonly MockHelper _mockHelper = new MockHelper();
        private readonly IServiceProvider _serviceProvider;
        private readonly DataContextTestHelper _dataContext;

        public CountryServiceTest()
        {
            _dbContextHelper = new DbContextHelper(_sqLiteDBConnections);
            _dbContextHelper.InitializeDB();
            _serviceProvider = new ServiceProviderTestHelper().CreateServiceProvider(_sqLiteDBConnections, _mockHelper);
            _dataContext = new DataContextTestHelper(_dbContextHelper);

            var dataContext = _serviceProvider.GetService<IDataContext>();
            var mapper = _serviceProvider.GetService<IMapper>();

            _service = new CountryService(dataContext, mapper);
        }

        [Fact]
        public async Task Get_ReturnsOkObjectResult_WithListOfCountries()
        {
            // Arrange
            _dataContext.Add(new Country()
            {
                Id = 4,
                Name = "Thailand"
            });

            // Act
            var result = await _service.GetCountries();

            // Assert
            Assert.Equal(4, result.Count());
            Assert.Equal("Australia", result.ElementAt(0).Name);
            Assert.Equal("New Zealand", result.ElementAt(1).Name);
            Assert.Equal("Indonesia", result.ElementAt(2).Name);
            Assert.Equal("Thailand", result.ElementAt(3).Name);
        }
    }
}
