using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using ProyectoTesis.Models.ViewModels;
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

        public async Task<ResultadoViewModel?> ObtenerRecomendacionesAsync(int[] riasec, int[] ocean)
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            string baseUrl;

            if (env.Equals("Development", StringComparison.OrdinalIgnoreCase))
                baseUrl = "http://127.0.0.1:8000";
            else
                baseUrl = _config["PythonApi:BaseUrl"] ?? "https://proyecto-ml-3.onrender.com";

            string endpoint = $"{baseUrl}/predict";

            var payload = new { riasec, ocean };
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var resultJson = await response.Content.ReadAsStringAsync();
            dynamic apiResponse = JsonConvert.DeserializeObject(resultJson);

            // ==========================
            // Mapear campos del JSON
            // ==========================
            var result = apiResponse.result;

            var model = new ResultadoViewModel
            {
                PerfilRiasec = result.riasec,
                Subperfil = result.subperfil,
                PuntajesOcean = JsonConvert.DeserializeObject<List<OceanTrait>>(
                    Convert.ToString(result.ocean_vector)
                ),
                Carreras = JsonConvert.DeserializeObject<List<CarreraSugerida>>(
                    Convert.ToString(result.recomendaciones)
                )
            };

            return model;
        }
    }
}
