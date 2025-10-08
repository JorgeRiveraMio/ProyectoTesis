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
            // --- Preparar JSON ---
            var payload = new { riasec, ocean };
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // --- Cabeceras ---
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // --- Enviar solicitud ---
            var response = await _httpClient.PostAsync("http://127.0.0.1:8000/predict", content);
            response.EnsureSuccessStatusCode();

            // --- Leer respuesta en bytes (garantiza UTF-8) ---
            var responseBytes = await response.Content.ReadAsByteArrayAsync();
            var resultJson = Encoding.UTF8.GetString(responseBytes);

            // --- Deserializar dinámicamente ---
            return JsonConvert.DeserializeObject<dynamic>(resultJson);
        }


    }
}
