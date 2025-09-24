using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTesis.Data;
using ProyectoTesis.Models;
using ProyectoTesis.Models.ViewModels;
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
                    return RedirectToAction("ResultadoCombinado");

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
                // Marcar intento como completado
                var intento = await _context.TBM_INTENTOS.FindAsync(intentoId);
                if (intento != null)
                {
                    intento.FEC_COMPLETADO = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }

                if (moduloId == 1) // RIASEC
                {
                    return RedirectToAction("MostrarPregunta", new { moduloId = 2, numero = 1 });
                }

                if (moduloId == 2) // MBTI
                {
                    return RedirectToAction("ResultadoCombinado");
                }
            }
            return RedirectToAction("MostrarPregunta", new { moduloId, numero = numeroActual + 1 });
        }

        // 4) Mostrar resultado combinado
        public async Task<IActionResult> ResultadoCombinado()
        {
            var sesionIdStr = HttpContext.Session.GetString("SesionId");
            if (string.IsNullOrEmpty(sesionIdStr)) return RedirectToAction("Iniciar");

            var sesionId = Guid.Parse(sesionIdStr);

            // Obtener intentos de RIASEC (1) y MBTI (2)
            var intentoRiasec = await _context.TBM_INTENTOS
                .FirstOrDefaultAsync(i => i.IDD_SESION == sesionId && i.IDD_MODULO == 1);
            var intentoMbti = await _context.TBM_INTENTOS
                .FirstOrDefaultAsync(i => i.IDD_SESION == sesionId && i.IDD_MODULO == 2);

            if (intentoRiasec == null || intentoMbti == null)
                return RedirectToAction("Iniciar");

            // ---- Calcular RIASEC ----
            var respuestasRiasec = await _context.TBD_RESPUESTAS
                .Where(r => r.IDD_INTENTO == intentoRiasec.IDD_INTENTO)
                .Join(_context.TBT_PREGUNTAS,
                    resp => resp.IDD_PREGUNTA,
                    preg => preg.IDD_PREGUNTA,
                    (resp, preg) => new { preg.COD_CATEGORIA, resp.VAL_RESPUESTA_TX })
                .ToListAsync();

            var categorias = new Dictionary<string, int>
            {
                { "R", 0 }, { "I", 0 }, { "A", 0 },
                { "S", 0 }, { "E", 0 }, { "C", 0 }
            };

            foreach (var item in respuestasRiasec)
            {
                if (int.TryParse(item.VAL_RESPUESTA_TX, out int valor) &&
                    !string.IsNullOrEmpty(item.COD_CATEGORIA) &&
                    categorias.ContainsKey(item.COD_CATEGORIA))
                {
                    categorias[item.COD_CATEGORIA] += valor;
                }
            }

            var categoriaGanadora = categorias.OrderByDescending(c => c.Value).First();
            var perfilRiasec = categoriaGanadora.Key;

            // ---- Calcular MBTI ----
            var respuestasMbti = await _context.TBD_RESPUESTAS
                .Where(r => r.IDD_INTENTO == intentoMbti.IDD_INTENTO)
                .ToListAsync();

            string perfilMbti = CalcularMbti(respuestasMbti);
            string descripcionMbti = GetDescripcionMbti(perfilMbti);

            // ---- Generar perfil combinado ----
            string perfilFinal = $"Tu perfil es {perfilRiasec} con un estilo {perfilMbti}";
            string recomendacionFinal = $"Carreras sugeridas combinando RIASEC ({perfilRiasec}) y MBTI ({perfilMbti}).";

            // Obtener carreras sugeridas
            var carreras = GetCarrerasDetalladas(perfilRiasec, perfilMbti);

            // Guardar resultado combinado
            var resultado = new TBM_RESULTADO
            {
                IDD_RESULTADO = Guid.NewGuid(),
                IDD_SESION = sesionId,
                FEC_CREADO = DateTime.UtcNow,
                NOM_PERFIL_TX = perfilFinal,
                DES_RECOMENDACION_TX = recomendacionFinal
            };

            _context.TBM_RESULTADOS.Add(resultado);
            await _context.SaveChangesAsync();

            // ViewModel con ambos resultados
            var vm = new ResultadoViewModel
            {
                IDD_RESULTADO = resultado.IDD_RESULTADO,
                NOM_PERFIL_TX = perfilFinal,
                DES_RECOMENDACION_TX = recomendacionFinal,
                Puntajes = categorias,
                Total = categorias.Values.Sum(),
                PerfilRiasec = perfilRiasec,
                PerfilMbti = perfilMbti,
                DescripcionMbti = descripcionMbti,
                Carreras = carreras
            };

            return View("Resultado", vm);

        }

        private List<CarreraSugerida> GetCarrerasDetalladas(string riasec, string mbti)
        {
            var carreras = new List<CarreraSugerida>();

            switch (riasec)
            {
                case "R": // Realista
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Ingeniería Mecánica",
                        Descripcion = "Diseña, construye y mantiene sistemas mecánicos aplicados a la industria.",
                        Icono = "engineering",
                        Universidades = new List<string> { "UNI", "UTEC" }
                    });
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Ingeniería Civil",
                        Descripcion = "Construye infraestructura como carreteras, puentes y edificios.",
                        Icono = "construction",
                        Universidades = new List<string> { "UNI", "PUCP" }
                    });
                    break;

                case "I": // Investigador
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Ciencias de Datos",
                        Descripcion = "Analiza grandes volúmenes de información para resolver problemas complejos.",
                        Icono = "query_stats",
                        Universidades = new List<string> { "UP", "PUCP" }
                    });
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Biotecnología",
                        Descripcion = "Aplica conocimientos de biología y tecnología en salud, agricultura e industria.",
                        Icono = "science",
                        Universidades = new List<string> { "UNMSM", "UTEC" }
                    });
                    break;

                case "A": // Artístico
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Diseño Gráfico",
                        Descripcion = "Crea soluciones visuales para marcas, productos y comunicación digital.",
                        Icono = "brush",
                        Universidades = new List<string> { "UPC", "Toulouse Lautrec" }
                    });
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Artes Escénicas",
                        Descripcion = "Explora el teatro, la danza o la actuación como medio de expresión artística.",
                        Icono = "theater_comedy",
                        Universidades = new List<string> { "PUCP", "USIL" }
                    });
                    break;

                case "S": // Social
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Psicología",
                        Descripcion = "Estudia la mente humana y el comportamiento. Aplica en salud, educación o RRHH.",
                        Icono = "psychology",
                        Universidades = new List<string> { "PUCP", "UNMSM" }
                    });
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Trabajo Social",
                        Descripcion = "Interviene en comunidades y organizaciones para mejorar la calidad de vida.",
                        Icono = "diversity_3",
                        Universidades = new List<string> { "UNMSM", "USMP" }
                    });
                    break;

                case "E": // Emprendedor
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Administración de Empresas",
                        Descripcion = "Lidera organizaciones, gestiona recursos y diseña estrategias de negocio.",
                        Icono = "business_center",
                        Universidades = new List<string> { "UP", "ULima" }
                    });
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Marketing",
                        Descripcion = "Crea estrategias para posicionar productos, servicios y marcas.",
                        Icono = "campaign",
                        Universidades = new List<string> { "UP", "USIL" }
                    });
                    break;

                case "C": // Convencional
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Contabilidad",
                        Descripcion = "Gestiona la información financiera de empresas y organizaciones.",
                        Icono = "monitoring",
                        Universidades = new List<string> { "UPC", "ULima" }
                    });
                    carreras.Add(new CarreraSugerida
                    {
                        Nombre = "Derecho",
                        Descripcion = "Defiende derechos, interpreta normas y asesora legalmente.",
                        Icono = "gavel",
                        Universidades = new List<string> { "PUCP", "USMP" }
                    });
                    break;
            }

            // 🔹 Ajuste extra con MBTI (para afinar la recomendación)
            if (mbti.StartsWith("E"))
            {
                carreras.Add(new CarreraSugerida
                {
                    Nombre = "Comunicación",
                    Descripcion = "Desarrolla habilidades en medios, periodismo o relaciones públicas.",
                    Icono = "mic",
                    Universidades = new List<string> { "ULima", "UPC" }
                });
            }
            else if (mbti.StartsWith("I"))
            {
                carreras.Add(new CarreraSugerida
                {
                    Nombre = "Investigación Académica",
                    Descripcion = "Enfócate en análisis profundo, proyectos de ciencia o docencia universitaria.",
                    Icono = "menu_book",
                    Universidades = new List<string> { "PUCP", "UNMSM" }
                });
            }

            // Fallback por si algo falla
            if (!carreras.Any())
            {
                carreras.Add(new CarreraSugerida
                {
                    Nombre = "Explora tu vocación",
                    Descripcion = "Tu perfil es único, explora distintas áreas relacionadas a tus intereses.",
                    Icono = "school",
                    Universidades = new List<string> { "Consulta diversas opciones académicas" }
                });
            }

            return carreras;
        }




        // Método para calcular MBTI
        private string CalcularMbti(List<TBD_RESPUESTA> respuestasMbti)
        {
            // Inicializamos los puntajes de las 8 dimensiones
            var dimensiones = new Dictionary<string, int>
            {
                { "E", 0 }, { "I", 0 },
                { "S", 0 }, { "N", 0 },
                { "T", 0 }, { "F", 0 },
                { "J", 0 }, { "P", 0 }
            };

            // Traemos cada respuesta con su COD_CATEGORIA de la tabla de preguntas
            var respuestasConPreguntas = respuestasMbti
                .Join(_context.TBT_PREGUNTAS,
                    r => r.IDD_PREGUNTA,
                    p => p.IDD_PREGUNTA,
                    (r, p) => new { p.COD_CATEGORIA, r.VAL_RESPUESTA_TX })
                .ToList();

            // Sumamos los valores por dimensión
            foreach (var item in respuestasConPreguntas)
            {
                if (int.TryParse(item.VAL_RESPUESTA_TX, out int valor) &&
                    !string.IsNullOrEmpty(item.COD_CATEGORIA) &&
                    dimensiones.ContainsKey(item.COD_CATEGORIA))
                {
                    dimensiones[item.COD_CATEGORIA] += valor;
                }
            }

            // Construimos el tipo MBTI tomando el mayor de cada par
            string mbti = "";
            mbti += dimensiones["E"] >= dimensiones["I"] ? "E" : "I";
            mbti += dimensiones["S"] >= dimensiones["N"] ? "S" : "N";
            mbti += dimensiones["T"] >= dimensiones["F"] ? "T" : "F";
            mbti += dimensiones["J"] >= dimensiones["P"] ? "J" : "P";

            return mbti;
        }

        // Método para obtener descripción del MBTI
        private string GetDescripcionMbti(string tipo)
        {
            var descripciones = new Dictionary<string, string>
            {
                { "ESTJ", "Práctico, organizado, buen líder. Se enfoca en resultados concretos." },
                { "ENTJ", "Estratega nato, decidido y visionario, con habilidades de liderazgo." },
                { "ESFJ", "Colaborador, leal y sociable. Busca armonía y ayudar a los demás." },
                { "ENFJ", "Carismático, inspira a otros y se centra en el bienestar grupal." },
                { "ISTJ", "Responsable, confiable, estructurado y orientado al deber." },
                { "ISFJ", "Protector, servicial y detallista. Busca apoyar y cuidar a otros." },
                { "INTJ", "Analítico, innovador y con visión de futuro. Le gustan los retos complejos." },
                { "INFJ", "Idealista, empático y creativo. Busca un propósito profundo." },
                { "ESTP", "Dinámico, activo y enfocado en la acción inmediata." },
                { "ESFP", "Extrovertido, alegre y entusiasta. Disfruta del presente." },
                { "ENTP", "Ingenioso, creativo y hábil para debatir. Le gustan los retos intelectuales." },
                { "ENFP", "Soñador, innovador y empático. Le atraen las posibilidades futuras." },
                { "ISTP", "Analítico, independiente y hábil con herramientas o sistemas." },
                { "ISFP", "Artístico, sensible y tranquilo. Le gusta la libertad personal." },
                { "INTP", "Pensador lógico, curioso y analítico. Le gusta explorar ideas." },
                { "INFP", "Idealista, reflexivo y compasivo. Busca coherencia con sus valores." }
            };

            return descripciones.ContainsKey(tipo)
                ? descripciones[tipo]
                : "Tipo MBTI no reconocido.";
        }

        public async Task<IActionResult> Recomendaciones(Guid resultadoId)
        {
            var resultado = await _context.TBM_RESULTADOS
                .FirstOrDefaultAsync(r => r.IDD_RESULTADO == resultadoId);

            if (resultado == null)
                return RedirectToAction("ResultadoCombinado");

            // Recuperar RIASEC y MBTI de la sesión actual
            var sesionIdStr = HttpContext.Session.GetString("SesionId");
            if (string.IsNullOrEmpty(sesionIdStr)) return RedirectToAction("Iniciar");
            var sesionId = Guid.Parse(sesionIdStr);

            var intentoRiasec = await _context.TBM_INTENTOS
                .FirstOrDefaultAsync(i => i.IDD_SESION == sesionId && i.IDD_MODULO == 1);
            var intentoMbti = await _context.TBM_INTENTOS
                .FirstOrDefaultAsync(i => i.IDD_SESION == sesionId && i.IDD_MODULO == 2);

            // Calculamos otra vez puntajes y perfiles (puedes optimizar guardando estos en TBM_RESULTADO si prefieres)
            var respuestasRiasec = await _context.TBD_RESPUESTAS
                .Where(r => r.IDD_INTENTO == intentoRiasec.IDD_INTENTO)
                .Join(_context.TBT_PREGUNTAS,
                    resp => resp.IDD_PREGUNTA,
                    preg => preg.IDD_PREGUNTA,
                    (resp, preg) => new { preg.COD_CATEGORIA, resp.VAL_RESPUESTA_TX })
                .ToListAsync();

            var categorias = new Dictionary<string, int>
            {
                { "R", 0 }, { "I", 0 }, { "A", 0 },
                { "S", 0 }, { "E", 0 }, { "C", 0 }
            };

            foreach (var item in respuestasRiasec)
            {
                if (int.TryParse(item.VAL_RESPUESTA_TX, out int valor) &&
                    !string.IsNullOrEmpty(item.COD_CATEGORIA) &&
                    categorias.ContainsKey(item.COD_CATEGORIA))
                {
                    categorias[item.COD_CATEGORIA] += valor;
                }
            }

            var perfilRiasec = categorias.OrderByDescending(c => c.Value).First().Key;

            var respuestasMbti = await _context.TBD_RESPUESTAS
                .Where(r => r.IDD_INTENTO == intentoMbti.IDD_INTENTO)
                .ToListAsync();

            string perfilMbti = CalcularMbti(respuestasMbti);
            string descripcionMbti = GetDescripcionMbti(perfilMbti);

            // Traemos carreras sugeridas
            var carreras = GetCarrerasDetalladas(perfilRiasec, perfilMbti);

            var vm = new ResultadoViewModel
            {
                IDD_RESULTADO = resultadoId,
                NOM_PERFIL_TX = resultado.NOM_PERFIL_TX,
                DES_RECOMENDACION_TX = resultado.DES_RECOMENDACION_TX,
                Puntajes = categorias,
                Total = categorias.Values.Sum(),
                PerfilRiasec = perfilRiasec,
                PerfilMbti = perfilMbti,
                DescripcionMbti = descripcionMbti,
                Carreras = carreras
            };

            return View(vm);
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

            return RedirectToAction("ResultadoCombinado");
        }
    }
}
