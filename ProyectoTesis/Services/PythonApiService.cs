using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProyectoTesis.Services
{
    public class PythonApiService
    {
        private readonly HttpClient _httpClient;

        public PythonApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> ObtenerRecomendacionesAsync(int[] riasec, int[] ocean)
        {
            var payload = new
            {
                riasec = riasec,
                ocean = ocean
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://127.0.0.1:8000/predict", content);
            response.EnsureSuccessStatusCode();

            var resultJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(resultJson);
        }
    }
}
