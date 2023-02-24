using Microsoft.EntityFrameworkCore;
using X.Domain;

namespace X.Repository
{
    public interface IDataContext
    {
        DbSet<AppSetting> AppSettings { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<City> Cities { get; set; }
    }
}
