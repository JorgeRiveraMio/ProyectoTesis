using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ProyectoTesis.Controllers
{
    public class AdminController : Controller
    {
        private readonly string _adminKey;

        public AdminController(IConfiguration config)
        {
            _adminKey = config["AdminSettings:AccessKey"] ?? "";
        }

        // Muestra vista con campo de clave (sin exponer datos)
        [HttpGet]
        public IActionResult Panel()
        {
            return View("PanelLogin");
        }

        // Verifica la clave enviada por POST
        [HttpPost]
        public IActionResult Panel(string password)
        {
            if (password == _adminKey)
            {
                TempData["AdminAccess"] = true;
                return RedirectToAction("Dashboard");
            }

            TempData["Error"] = "Clave incorrecta.";
            return RedirectToAction("Panel");
        }

        public IActionResult Dashboard()
        {
            if (TempData["AdminAccess"] == null)
                return NotFound(); // No autorizado

            TempData.Keep("AdminAccess"); // Mantiene la sesión temporal
            return View();
        }
    }
}
