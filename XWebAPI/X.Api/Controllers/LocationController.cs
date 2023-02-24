using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.BusinessService.Interfaces;
using X.Model;

namespace X.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public LocationController(ICountryService countryService, ICityService cityService)
        {
            _countryService = countryService;
            _cityService = cityService;
        }

        [HttpGet("countries")]
        public async Task<IEnumerable<CountryDto>> GetCountry()
        {
            return await _countryService.GetCountries();
        }

        [HttpGet("cities/{countryid}")]
        public async Task<IEnumerable<CityDto>> GetCitiesByCountry(int countryId)
        {
            return await _cityService.GetCities(countryId);
        }
    }
}
