using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using X.Domain;
using X.Repository;

namespace X.UnitTest.Infrastructures
{
    public class DbContextHelper
    {
        private readonly SQLLiteDBConnections _sqLiteDBConnections;

        public DbContextHelper(SQLLiteDBConnections sqLiteDBConnections)
        {
            _sqLiteDBConnections = sqLiteDBConnections;
        }

        public DataContext DataContext { get; set; }

        public DbContextOptions<TContext> ContextOptions<TContext>(DbConnection dbConnection)
                where TContext : DbContext
        {
            return new DbContextOptionsBuilder<TContext>()
                    .UseSqlite(dbConnection)
                    .EnableSensitiveDataLogging()
                    .Options;
        }

        public DataContext CreateDataContext()
        {
            return new DataContext(ContextOptions<DataContext>(_sqLiteDBConnections.xMemoryDbConnection));
        }


        public void InitializeDB()
        {
            _sqLiteDBConnections.xMemoryDbConnection.Open();
            DataContext = CreateDataContext();
            DataContext.Database.EnsureCreated();

            InitializeDataGlobally();
        }

        void InitializeDataGlobally()
        {

            // Add the countries
            var countries = new List<Country>() {
                new Country() { Id = 1, Name = "Australia" },
                new Country() { Id = 2, Name = "New Zealand" },
                new Country() { Id = 3, Name = "Indonesia" }
            };

            // Add the cities
            var cities = new List<City>
            {
                new City { Name = "Sydney", CountryId = countries[0].Id },
                new City { Name = "Melbourne", CountryId = countries[0].Id },
                new City { Name = "Brisbane", CountryId = countries[0].Id },
                new City { Name = "Auckland", CountryId = countries[1].Id },
                new City { Name = "Wellington", CountryId = countries[1].Id },
                new City { Name = "Christchurch", CountryId = countries[1].Id },
                new City { Name = "Jakarta", CountryId = countries[2].Id },
                new City { Name = "Yogyakarta", CountryId = countries[2].Id },
                new City { Name = "Bandung", CountryId = countries[2].Id },
            };


            DataContext.AddRange(countries);
            DataContext.AddRange(cities);
            DataContext.SaveChanges();
        }

    }
}
