using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using X.Common;
using X.Repository;

namespace X.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            DependencyRegister.RegisterInternalServiceDependencies(services);
            DependencyRegister.RegisterExternalServiceDependencies(services);

            if (!UnitTestDetector.IsRunningFromXUnit())
            {
                services.AddDbContext<DataContext>(options =>
                   options.UseSqlite(Configuration.GetConnectionString("XDBConnection")));

                // Create database file if it doesn't exist
                using (var dbContext = services.BuildServiceProvider().GetRequiredService<DataContext>())
                {
                    dbContext.Database.EnsureCreated();
                }

                services.AddScoped<IDataContext>(s => s.GetService<DataContext>());
            }

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dbContext)
        {
            bool dataInitialized = dbContext.AppSettings.Any(s => s.Name == "DataInitialized" && s.Value == "true");
            if (!dataInitialized)
            {
                // Initialize data
                dbContext.InitializeData();

                // Update flag to indicate that data has been initialized
                var flag = dbContext.AppSettings.FirstOrDefault(s => s.Name == "DataInitialized");
                if (flag != null)
                {
                    flag.Value = "true";
                    dbContext.SaveChanges();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
