using System.Threading.Tasks;

namespace X.BusinessService.Utilities
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string url);
    }
}
