using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTesis.Data;
using ProyectoTesis.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ProyectoTesis.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1) Iniciar una sesión anónima y redirigir a la primera pregunta
        public async Task<IActionResult> Iniciar(byte moduloId = 1)
        {
            var sesion = new TBM_SESION
            {
                NOM_ESTAD_SES = "activa",
                FEC_CREADO = DateTime.UtcNow
            };

            _context.TBM_SESIONES.Add(sesion);
            await _context.SaveChangesAsync();

            // Guardar en HttpContext.Session
            HttpContext.Session.SetString("SesionId", sesion.IDD_SESION.ToString());

            return RedirectToAction("MostrarPregunta", new { moduloId, numero = 1 });
        }

        // 2) Mostrar una pregunta por número
        public async Task<IActionResult> MostrarPregunta(byte moduloId, int numero)
        {
            try
            {
                var sesionIdStr = HttpContext.Session.GetString("SesionId");
                if (string.IsNullOrEmpty(sesionIdStr))
                    return RedirectToAction("Iniciar", new { moduloId });

                var sesionId = Guid.Parse(sesionIdStr);

                // Buscar intento existente
                var intento = await _context.TBM_INTENTOS
                    .FirstOrDefaultAsync(i => i.IDD_SESION == sesionId && i.IDD_MODULO == moduloId);

                if (intento == null)
                {
                    if (!await _context.TBT_MODULOS.AnyAsync(m => m.IDD_MODULO == moduloId))
                        throw new Exception($"El moduloId={moduloId} no existe.");

                    intento = new TBM_INTENTO
                    {
                        IDD_INTENTO = Guid.NewGuid(),
                        IDD_SESION = sesionId,
                        IDD_MODULO = moduloId,
                        FEC_INICIADO = DateTime.UtcNow
                    };

                    _context.TBM_INTENTOS.Add(intento);
                    await _context.SaveChangesAsync();
                }

                // Obtener todas las preguntas del módulo
                var preguntas = await _context.TBT_PREGUNTAS
                    .Where(p => p.IDD_MODULO == moduloId)
                    .OrderBy(p => p.IDD_PREGUNTA)
                    .ToListAsync();

                if (numero < 1 || numero > preguntas.Count)
                    return RedirectToAction("Resultado");

                var preguntaActual = preguntas[numero - 1];

                ViewBag.SesionId = sesionId;
                ViewBag.ModuloId = moduloId;
                ViewBag.IntentoId = intento.IDD_INTENTO;
                ViewBag.NumeroActual = numero;
                ViewBag.TotalPreguntas = preguntas.Count;

                return View("Preguntas", preguntaActual);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR en MostrarPregunta: {ex.Message}");
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // 3) Guardar una respuesta y avanzar
        [HttpPost]
        public async Task<IActionResult> GuardarRespuesta(Guid intentoId, int IDD_PREGUNTA, string VAL_RESPUESTA_TX, int numeroActual, byte moduloId)
        {
            if (!string.IsNullOrEmpty(VAL_RESPUESTA_TX))
            {
                var respuesta = new TBD_RESPUESTA
                {
                    IDD_INTENTO = intentoId,
                    IDD_PREGUNTA = IDD_PREGUNTA,
                    VAL_RESPUESTA_TX = VAL_RESPUESTA_TX,
                    FEC_GUARDADO = DateTime.UtcNow
                };

                _context.TBD_RESPUESTAS.Add(respuesta);
                await _context.SaveChangesAsync();
            }

            // Obtener total de preguntas
            var totalPreguntas = await _context.TBT_PREGUNTAS
                .CountAsync(p => p.IDD_MODULO == moduloId);

            if (numeroActual >= totalPreguntas)
            {
                // Marcar el intento como completado
                var intento = await _context.TBM_INTENTOS.FindAsync(intentoId);
                if (intento != null)
                {
                    intento.FEC_COMPLETADO = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Resultado");
            }

            return RedirectToAction("MostrarPregunta", new { moduloId, numero = numeroActual + 1 });
        }

        // 4) Mostrar resultado
        public async Task<IActionResult> Resultado()
        {
            var sesionIdStr = HttpContext.Session.GetString("SesionId");
            if (string.IsNullOrEmpty(sesionIdStr)) return RedirectToAction("Iniciar");

            var sesionId = Guid.Parse(sesionIdStr);

            var resultado = await _context.TBM_RESULTADOS
                                          .FirstOrDefaultAsync(r => r.IDD_SESION == sesionId);

            if (resultado == null)
            {
                resultado = new TBM_RESULTADO
                {
                    IDD_RESULTADO = Guid.NewGuid(),
                    IDD_SESION = sesionId,
                    FEC_CREADO = DateTime.UtcNow,
                    NOM_PERFIL_TX = "Artístico",
                    DES_RECOMENDACION_TX = "Carreras en diseño, música, artes escénicas..."
                };

                _context.TBM_RESULTADOS.Add(resultado);
                await _context.SaveChangesAsync();
            }

            return View(resultado);
        }

        // 5) Guardar envío de resultado por correo
        [HttpPost]
        public async Task<IActionResult> EnviarResultado(Guid resultadoId, string correo)
        {
            var envio = new TBD_ENVIO
            {
                IDD_RESULTADO = resultadoId,
                DES_CORREO_TX = correo,
                FEC_ENVIADO = DateTime.UtcNow
            };

            _context.TBD_ENVIOS.Add(envio);
            await _context.SaveChangesAsync();

            return RedirectToAction("Resultado");
        }
    }
}
