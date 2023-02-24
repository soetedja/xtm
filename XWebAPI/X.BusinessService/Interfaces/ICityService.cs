using System.Collections.Generic;
using System.Threading.Tasks;
using X.Model;

namespace X.BusinessService.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetCities(int countryId);
    }
}
