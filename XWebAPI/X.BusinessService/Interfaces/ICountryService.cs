using System.Collections.Generic;
using System.Threading.Tasks;
using X.Model;

namespace X.BusinessService.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetCountries();
    }
}
