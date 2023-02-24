using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System.Text;
using X.BusinessService.Utilities;

namespace X.UnitTest.Infrastructures
{
    public class MockHelper
    {
        private readonly Mock<IHttpClientService> mockHttpClientService = new Mock<IHttpClientService>();

        public Mock<IHttpClientService> GetMockHttpClientService()
        {
            return mockHttpClientService;
        }
    }
}
