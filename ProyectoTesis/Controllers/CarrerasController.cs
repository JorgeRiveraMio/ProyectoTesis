using Microsoft.AspNetCore.Mvc;
using ProyectoTesis.Models;
using System.IO;
using System.Text.Json;

namespace ProyectoTesis.Controllers
{
    public class CarrerasController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public CarrerasController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "riasec_affinity.json");

            if (!System.IO.File.Exists(path))
                return Content("Archivo JSON no encontrado.");

            var json = System.IO.File.ReadAllText(path);
            var perfiles = JsonSerializer.Deserialize<PerfilRIASEC>(json);

            return View(perfiles);
        }
    }
}
