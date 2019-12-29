using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiagnosticSourceDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private static readonly DiagnosticSource testDiagnosticListener = new DiagnosticListener("TestDiagnosticListener");

        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("http://www.mingdao.com");
            var result = await response.Content.ReadAsStringAsync();

            if (testDiagnosticListener.IsEnabled("RequestStart"))
            {
                testDiagnosticListener.Write("RequestStart", "hello world");
            }

            return "OK";
        }
    }
}
