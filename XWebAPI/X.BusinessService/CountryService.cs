using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.BusinessService.Interfaces;
using X.Domain;
using X.Model;
using X.Repository;

namespace X.BusinessService
{
    public class CountryService : ICountryService
    {
        private readonly IMapper _mapper;
        private readonly IDataContext _dataContext;

        public CountryService(IDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDto>> GetCountries()
        {
            var countries = await _dataContext.Countries.ToListAsync();
            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }
    }
}
