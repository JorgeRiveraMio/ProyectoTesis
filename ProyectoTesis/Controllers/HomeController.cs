using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTesis.Data;
using ProyectoTesis.Models;

namespace ProyectoTesis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // --- Vista de administración con botón de exportación ---
        public IActionResult Admin()
        {
            return View();
        }

        /// <summary>
        /// Exporta resultados completos (para análisis de hipótesis HE1, HE2, HE3).
        /// Incluye tiempos, perfiles, recomendaciones y satisfacción.
        /// </summary>
        public IActionResult ExportarResultadosDetallado()
        {
            var datos = (from s in _context.TBM_SESIONES
                         join r in _context.TBM_RESULTADOS on s.IDD_SESION equals r.IDD_SESION
                         join sa in _context.TBM_SATISFACCIONES on s.IDD_SESION equals sa.IDD_SESION into satisfaccion
                         from sa in satisfaccion.DefaultIfEmpty()
                         select new
                         {
                             SesionId = s.IDD_SESION,
                             FechaInicio = s.FEC_INICIO,
                             FechaFin = s.FEC_FIN,
                             DuracionMinutos = s.FEC_FIN.HasValue && s.FEC_INICIO.HasValue
                                 ? Math.Round((s.FEC_FIN.Value - s.FEC_INICIO.Value).TotalMinutes, 2)
                                 : 0,
                             PerfilRIASEC = r.NOM_PERFIL_TX ?? "",
                             NumeroRecomendaciones = r.NUM_RECOMENDACIONES,
                             FacilidadUso = sa != null ? sa.FACILIDAD_USO : 0,
                             ClaridadResultados = sa != null ? sa.CLARIDAD_RESULTADOS : 0,
                             UtilidadRecomendaciones = sa != null ? sa.UTILIDAD_RECOMENDACIONES : 0,
                             SatisfaccionGlobal = sa != null ? sa.SATISFACCION_GLOBAL : 0,
                             FechaRegistro = sa != null ? sa.FEC_REGISTRO : DateTime.MinValue
                         }).ToList();

            var csv = new StringBuilder();

            // --- Cabecera ---
            csv.AppendLine("SesionId;FechaInicio;FechaFin;DuracionMin;PerfilRIASEC;Recomendaciones;FacilidadUso;ClaridadResultados;UtilidadRecomendaciones;SatisfaccionGlobal;FechaRegistro");

            // --- Contenido ---
            foreach (var d in datos)
            {
                csv.AppendLine($"{d.SesionId};{d.FechaInicio};{d.FechaFin};{d.DuracionMinutos};{d.PerfilRIASEC};{d.NumeroRecomendaciones};{d.FacilidadUso};{d.ClaridadResultados};{d.UtilidadRecomendaciones};{d.SatisfaccionGlobal};{d.FechaRegistro}");
            }

            return File(
                Encoding.UTF8.GetBytes(csv.ToString()),
                "text/csv",
                "ResultadosVocacionalesDetallado.csv"
            );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult LimpiarSesion()
        {
            HttpContext.Session.Clear();
            return Ok();
        }

    }
}
