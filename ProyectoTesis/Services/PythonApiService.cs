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
            string baseUrl = env.Equals("Development", StringComparison.OrdinalIgnoreCase)
                ? "http://127.0.0.1:8000"
                : _config["PythonApi:BaseUrl"] ?? "https://proyecto-ml-3.onrender.com";

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

            var result = apiResponse.result;

            var oceanVectorJson = Convert.ToString(result.ocean_vector);
            var oceanVector = JsonConvert.DeserializeObject<List<OceanTrait>>(oceanVectorJson);

            var recomendacionesJson = Convert.ToString(result.recomendaciones);
            var recomendacionesParsed = JsonConvert.DeserializeObject<List<dynamic>>(recomendacionesJson);

            var carreras = new List<CarreraSugerida>();
            if (recomendacionesParsed != null)
            {
                foreach (var rec in recomendacionesParsed)
                {
                    string nombre = rec.carrera != null ? (string)rec.carrera : "";
                    double score = rec.score != null ? (double)rec.score : 0;

                    List<string> universidades = new();
                    if (rec.universidades != null)
                    {
                        universidades = ((Newtonsoft.Json.Linq.JArray)rec.universidades)
                            .ToObject<List<string>>() ?? new List<string>();
                    }

                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = nombre,
                        Descripcion = $"Sugerida automáticamente (afinidad: {score:F2})",
                        Universidades = universidades,
                        Icono = "school",
                        Score = score
                    });
                }
            }

            var model = new ResultadoViewModel
            {
                PerfilRiasec = result.riasec,
                Subperfil = result.subperfil,
                PuntajesOcean = oceanVector,
                Carreras = carreras
            };

            return model;
        }
    }
}
