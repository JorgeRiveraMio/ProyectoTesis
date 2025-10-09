using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace ProyectoTesis.Services
{
    public class PythonApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public PythonApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<dynamic> ObtenerRecomendacionesAsync(int[] riasec, int[] ocean)
        {
            //Determinar entorno y URL base
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            string baseUrl;

            if (env.Equals("Development", StringComparison.OrdinalIgnoreCase))
            {
                // Modo local
                baseUrl = "http://127.0.0.1:8000";
            }
            else
            {
                // Modo producción (Render)
                baseUrl = _config["PythonApi:BaseUrl"] ?? "https://proyecto-ml-3.onrender.com";
            }

            string endpoint = $"{baseUrl}/predict";

            //Preparar JSON
            var payload = new { riasec, ocean };
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Cabeceras
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //Enviar solicitud
            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            //Leer respuesta UTF-8 segura
            var responseBytes = await response.Content.ReadAsByteArrayAsync();
            var resultJson = Encoding.UTF8.GetString(responseBytes);

            return JsonConvert.DeserializeObject<dynamic>(resultJson);
        }
    }
}
