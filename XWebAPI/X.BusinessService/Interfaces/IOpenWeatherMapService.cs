using System.Threading.Tasks;
using X.Model;

namespace X.BusinessService.Interfaces
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherResponseDto> GetWeatherByCityNameAsync(string cityName);
    }
}
