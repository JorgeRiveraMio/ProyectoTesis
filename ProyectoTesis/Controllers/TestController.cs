using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTesis.Data;
using ProyectoTesis.Models;
using ProyectoTesis.Models.ViewModels;
using ProyectoTesis.Services;
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
        private readonly PdfService _pdfService;
        private readonly EmailService _emailService;
        private readonly PythonApiService _pythonApiService;

        public TestController(
            ApplicationDbContext context,
            PdfService pdfService,
            EmailService emailService,
            PythonApiService pythonApiService)
        {
            _context = context;
            _pdfService = pdfService;
            _emailService = emailService;
            _pythonApiService = pythonApiService;
        }

        // 1) Iniciar sesión
        public async Task<IActionResult> Iniciar(byte moduloId = 1)
        {
            var sesion = new TBM_SESION
            {
                NOM_ESTAD_SES = "activa",
                FEC_CREADO = DateTime.UtcNow,
                FEC_INICIO = DateTime.UtcNow 
            };

            _context.TBM_SESIONES.Add(sesion);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("SesionId", sesion.IDD_SESION.ToString());

            return RedirectToAction("MostrarPregunta", new { moduloId, numero = 1 });
        }


        // 2) Mostrar pregunta
        public async Task<IActionResult> MostrarPregunta(byte moduloId, int numero)
        {
            var sesionIdStr = HttpContext.Session.GetString("SesionId");
            if (string.IsNullOrEmpty(sesionIdStr))
                return RedirectToAction("Iniciar", new { moduloId });

            var sesionId = Guid.Parse(sesionIdStr);

            var intento = await _context.TBM_INTENTOS
                .FirstOrDefaultAsync(i => i.IDD_SESION == sesionId && i.IDD_MODULO == moduloId);

            if (intento == null)
            {
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

            var preguntas = await _context.TBT_PREGUNTAS
                .Where(p => p.IDD_MODULO == moduloId)
                .OrderBy(p => p.IDD_PREGUNTA)
                .ToListAsync();

            if (numero < 1 || numero > preguntas.Count)
                return RedirectToAction("ResultadoCombinado");

            var preguntaActual = preguntas[numero - 1];

            ViewBag.SesionId = sesionId;
            ViewBag.ModuloId = moduloId;
            ViewBag.IntentoId = intento.IDD_INTENTO;
            ViewBag.NumeroActual = numero;
            ViewBag.TotalPreguntas = preguntas.Count;

            return View("Preguntas", preguntaActual);
        }

        // 3) Guardar respuesta
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

            var totalPreguntas = await _context.TBT_PREGUNTAS
                .CountAsync(p => p.IDD_MODULO == moduloId);

            if (numeroActual >= totalPreguntas)
            {
                var intento = await _context.TBM_INTENTOS.FindAsync(intentoId);
                if (intento != null)
                {
                    intento.FEC_COMPLETADO = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }

                if (moduloId == 1)
                    return RedirectToAction("MostrarPregunta", new { moduloId = 2, numero = 1 });

                if (moduloId == 2)
                    return RedirectToAction("ResultadoCombinado");
            }

            return RedirectToAction("MostrarPregunta", new { moduloId, numero = numeroActual + 1 });
        }

        // 4) Resultado combinado con API Python
        public async Task<IActionResult> ResultadoCombinado()
        {
            var sesionIdStr = HttpContext.Session.GetString("SesionId");
            if (string.IsNullOrEmpty(sesionIdStr))
                return RedirectToAction("Iniciar");

            var sesionId = Guid.Parse(sesionIdStr);

            // --- Bloque RIASEC ---
            var intentoRiasec = await _context.TBM_INTENTOS
                .FirstOrDefaultAsync(i => i.IDD_SESION == sesionId && i.IDD_MODULO == 1);

            if (intentoRiasec == null)
            {
                TempData["Mensaje"] = "No se encontraron respuestas del test RIASEC.";
                return RedirectToAction("Iniciar");
            }

            var respuestasRiasec = await _context.TBD_RESPUESTAS
                .Where(r => r.IDD_INTENTO == intentoRiasec.IDD_INTENTO)
                .Join(_context.TBT_PREGUNTAS,
                    resp => resp.IDD_PREGUNTA,
                    preg => preg.IDD_PREGUNTA,
                    (resp, preg) => new { preg.COD_CATEGORIA, resp.VAL_RESPUESTA_TX })
                .ToListAsync();

            int[] riasecVector = CalcularVectorRiasec(respuestasRiasec);

            var categoriasRiasec = new Dictionary<string, int>
            {
                { "R", riasecVector[0] },
                { "I", riasecVector[1] },
                { "A", riasecVector[2] },
                { "S", riasecVector[3] },
                { "E", riasecVector[4] },
                { "C", riasecVector[5] }
            };

            // --- Bloque OCEAN ---
            var intentoOcean = await _context.TBM_INTENTOS
                .FirstOrDefaultAsync(i => i.IDD_SESION == sesionId && i.IDD_MODULO == 2);

            if (intentoOcean == null)
            {
                TempData["Mensaje"] = "No se encontraron respuestas del test OCEAN.";
                return RedirectToAction("Iniciar");
            }

            var respuestasOcean = await _context.TBD_RESPUESTAS
                .Where(r => r.IDD_INTENTO == intentoOcean.IDD_INTENTO)
                .ToListAsync();

            int[] oceanVector = CalcularVectorOcean(respuestasOcean);

            // ===============================
            // Llamada a la API Python
            // ===============================
            var resultadoApi = await _pythonApiService.ObtenerRecomendacionesAsync(riasecVector, oceanVector);

            if (resultadoApi == null)
            {
                TempData["Mensaje"] = "❌ Error: no se recibieron datos del modelo ML.";
                return RedirectToAction("Iniciar");
            }

            Console.WriteLine("=== JSON devuelto por API Python ===");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(resultadoApi, Newtonsoft.Json.Formatting.Indented));

            // --- Guardar resultado base ---
            var resultado = new TBM_RESULTADO
            {
                IDD_RESULTADO = Guid.NewGuid(),
                IDD_SESION = sesionId,
                FEC_CREADO = DateTime.UtcNow,
                NOM_PERFIL_TX = resultadoApi.PerfilRiasec ?? "N/A",
                DES_RECOMENDACION_TX = "Generadas por modelo ML",
                NUM_RECOMENDACIONES = resultadoApi.Carreras.Count,
                LISTA_RECOMENDACIONES_JSON = System.Text.Json.JsonSerializer.Serialize(resultadoApi.Carreras)
            };

            _context.TBM_RESULTADOS.Add(resultado);
            await _context.SaveChangesAsync();

            // --- Procesar OCEAN resumen ---
            string perfilOceanResumen = string.Join(", ",
                resultadoApi.PuntajesOcean.Select(o =>
                    $"{MapOceanNombre(o.Trait)}: {MapNivelOcean(o.Value)} ({o.Value:F1})"
                ));

            // --- ViewModel final ---
            resultadoApi.IDD_RESULTADO = resultado.IDD_RESULTADO;
            resultadoApi.PuntajesRiasec = categoriasRiasec;
            resultadoApi.TotalRiasec = categoriasRiasec.Values.Sum();
            resultadoApi.PerfilOceanResumen = perfilOceanResumen;

            // --- Finalizar sesión ---
            var sesion = await _context.TBM_SESIONES.FindAsync(sesionId);
            if (sesion != null)
            {
                sesion.FEC_FIN = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return View("Resultado", resultadoApi);
        }





        // ---  Helpers ---
        private int[] CalcularVectorRiasec(IEnumerable<dynamic> respuestas)
        {
            var categorias = new Dictionary<string, int>
            {
                { "R", 0 }, { "I", 0 }, { "A", 0 },
                { "S", 0 }, { "E", 0 }, { "C", 0 }
            };

            foreach (var item in respuestas)
            {
                if (int.TryParse(item.VAL_RESPUESTA_TX, out int valor) &&
                    categorias.ContainsKey(item.COD_CATEGORIA))
                {
                    categorias[item.COD_CATEGORIA] += valor;
                }
            }

            return new int[]
            {
                categorias["R"],
                categorias["I"],
                categorias["A"],
                categorias["S"],
                categorias["E"],
                categorias["C"]
            };
        }

        private int[] CalcularVectorOcean(IEnumerable<TBD_RESPUESTA> respuestas)
        {
            var vector = new List<int>();
            foreach (var r in respuestas.OrderBy(x => x.IDD_PREGUNTA))
            {
                if (int.TryParse(r.VAL_RESPUESTA_TX, out int valor))
                    vector.Add(valor);
            }

            return vector.ToArray();
        }

        private string MapOceanNombre(string key)
        {
            return key switch
            {
                "O" => "Apertura",
                "C" => "Responsabilidad",
                "E" => "Extraversión",
                "A" => "Amabilidad",
                "N" => "Neuroticismo",
                _ => key
            };
        }

        private string MapNivelOcean(double valor)
        {
            if (valor >= 4) return "Alta";
            if (valor >= 2.5) return "Media";
            return "Baja";
        }

        // 5) Ver recomendaciones
        public async Task<IActionResult> Recomendaciones(Guid resultadoId)
        {
            var resultado = await _context.TBM_RESULTADOS.FindAsync(resultadoId);
            if (resultado == null)
            {
                TempData["Mensaje"] = "No se encontró el resultado.";
                return RedirectToAction("ResultadoCombinado");
            }

            List<CarreraSugerida> carreras = new();

            try
            {
                if (!string.IsNullOrEmpty(resultado.LISTA_RECOMENDACIONES_JSON))
                {
                    using var doc = System.Text.Json.JsonDocument.Parse(resultado.LISTA_RECOMENDACIONES_JSON);
                    foreach (var elem in doc.RootElement.EnumerateArray())
                    {
                        
                        string nombre = "";
                        if (elem.TryGetProperty("carrera", out var nomLower))
                            nombre = nomLower.GetString() ?? "";
                        else if (elem.TryGetProperty("Carrera", out var nomUpper))
                            nombre = nomUpper.GetString() ?? "";

                        string desc = elem.TryGetProperty("score", out var scLower)
                            ? $"Sugerida automáticamente (afinidad: {scLower.GetDouble():F2}%)"
                            : elem.TryGetProperty("Score", out var scUpper)
                                ? $"Sugerida automáticamente (afinidad: {scUpper.GetDouble():F2}%)"
                                : "Sugerida automáticamente";

                        List<string> universidades = new();
                        if (elem.TryGetProperty("universidades", out var unisLower) && unisLower.ValueKind == System.Text.Json.JsonValueKind.Array)
                            universidades = unisLower.EnumerateArray().Select(u => u.GetString() ?? "").ToList();
                        else if (elem.TryGetProperty("Universidades", out var unisUpper) && unisUpper.ValueKind == System.Text.Json.JsonValueKind.Array)
                            universidades = unisUpper.EnumerateArray().Select(u => u.GetString() ?? "").ToList();

                        carreras.Add(new CarreraSugerida
                        {
                            Nombre = nombre,
                            Descripcion = desc,
                            Icono = "school",
                            Universidades = universidades.Count > 0
                                ? universidades
                                : new List<string> { "Consulta diversas opciones académicas" }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al deserializar carreras: {ex.Message}");
            }

            // --- Construir el ViewModel final ---
            var vm = new ResultadoViewModel
            {
                IDD_RESULTADO = resultado.IDD_RESULTADO,
                NOM_PERFIL_TX = resultado.NOM_PERFIL_TX,
                DES_RECOMENDACION_TX = resultado.DES_RECOMENDACION_TX,
                PerfilRiasec = resultado.NOM_PERFIL_TX,
                Carreras = carreras,
                PuntajesOcean = new List<OceanTrait>() // <- evita nulos
            };

            if (!carreras.Any())
                TempData["Mensaje"] = "No se encontraron recomendaciones válidas en el registro.";

            return View(vm);
        }





        // GET: /Test/Enviar/{resultadoId}
        public async Task<IActionResult> Enviar(Guid resultadoId)
        {
            var resultado = await _context.TBM_RESULTADOS.FindAsync(resultadoId);
            if (resultado == null)
            {
                TempData["Mensaje"] = "No se encontró el resultado para enviar.";
                return RedirectToAction("ResultadoCombinado");
            }

            return View(resultado); 
        }

        [HttpPost]
        public async Task<IActionResult> EnviarYGuardarSatisfaccion(
            Guid resultadoId,
            Guid sesionId,
            string correo,
            int facilidadUso,
            int claridadResultados,
            int utilidadRecomendaciones,
            int satisfaccionGlobal)
        {
            // 1. Validar sesión y resultado
            var sesion = await _context.TBM_SESIONES.FindAsync(sesionId);
            var resultado = await _context.TBM_RESULTADOS.FindAsync(resultadoId);

            if (sesion == null || resultado == null)
            {
                TempData["Mensaje"] = "No se encontró la sesión o el resultado.";
                return RedirectToAction("ResultadoCombinado");
            }

            // 2. Guardar satisfacción
            var satisfaccion = new TBM_SATISFACCION
            {
                IDD_SESION = sesionId,
                FACILIDAD_USO = facilidadUso,
                CLARIDAD_RESULTADOS = claridadResultados,
                UTILIDAD_RECOMENDACIONES = utilidadRecomendaciones,
                SATISFACCION_GLOBAL = satisfaccionGlobal,
                FEC_REGISTRO = DateTime.UtcNow
            };
            _context.TBM_SATISFACCIONES.Add(satisfaccion);
            await _context.SaveChangesAsync();

            // 3. Generar PDF y enviar correo
            var vm = new ResultadoViewModel
            {
                IDD_RESULTADO = resultado.IDD_RESULTADO,
                NOM_PERFIL_TX = resultado.NOM_PERFIL_TX,
                DES_RECOMENDACION_TX = resultado.DES_RECOMENDACION_TX,
                PerfilRiasec = resultado.NOM_PERFIL_TX,
                Carreras = new List<CarreraSugerida>
                {
                    new CarreraSugerida { Nombre = "Ingeniería de Sistemas", Descripcion = "Diseño y gestión de software" },
                    new CarreraSugerida { Nombre = "Psicología", Descripcion = "Estudio del comportamiento humano" }
                }
            };

            var pdfBytes = _pdfService.GenerarPdf(vm);

            await _emailService.EnviarCorreoConPdfAsync(
                correo,
                "Tus resultados vocacionales",
                "Gracias por tu evaluación. Adjunto encontrarás tu reporte vocacional.",
                pdfBytes
            );

            TempData["Mensaje"] = $"✅ Evaluación guardada y resultados enviados correctamente a {correo}.";
            return RedirectToAction("Recomendaciones", new { resultadoId });
        }


    
    }
}
